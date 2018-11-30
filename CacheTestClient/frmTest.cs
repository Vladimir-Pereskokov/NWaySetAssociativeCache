using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NSetACache;

namespace CacheTestClient
{
    public partial class frmTest : Form
    {

        private Cache<OrderDetail, int> m_cache = null;
        private int[] m_IDs = null;
        private int m_NumberOfSets;
        private int m_NumberOfRecordsPerSet;
        private string m_chacheAlgorithm;
        public frmTest()
        {
            InitializeComponent();
        }


        const string C_DefaultFileMSG = "Click Load Data button to populate cache >>";
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var rbHandler = new System.EventHandler(this.Rb_CheckedChanged);
            this.rbSizeOnly.CheckedChanged += rbHandler;
            this.rbLRU.CheckedChanged += rbHandler;
            this.rbMRU.CheckedChanged += rbHandler;
            this.rbSizesAndCustomStrategy.CheckedChanged += rbHandler;

            lblDataFile.Text = OrderDetailsReader.GetDefaultSourceFile();
            if (!File.Exists(lblDataFile.Text)) BtnLoad.Enabled = false;
            else lblFileMessage.Text = C_DefaultFileMSG;
            CalcSizes();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {            
            CalcSizes();
        }

        private void CalcSizes() {
            if (NumericUpDown2.Value > NumericUpDown1.Value) NumericUpDown2.Value = NumericUpDown1.Value;
            NumericUpDown2.Maximum = NumericUpDown1.Value;
            if (rbSizeOnly.Checked) {
                NumericUpDown2.Value = (decimal)Math.Log((int)(NumericUpDown1.Value), 2);
            }

            lblSize.Text = Math.Pow(2, (int)(NumericUpDown1.Value)).ToString("N0");            
            lblSetSize.Text = Math.Pow(2, (int)(NumericUpDown2.Value)).ToString("N0");
            NumericUpDown2.Enabled = !rbSizeOnly.Checked;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            lblFileMessage.Text = "<<<<<<<<<<< Loading KEYS from the data file. Please Wait ..........  >>>>";
            textBox1.Text = "";
            ListBox1.Items.Clear();
            lblCacheConfig.Text = "?";
            try
            {
                // Load file data asynchnously
                var t = LoadCacheAsync();
                var aw = t.GetAwaiter();
                aw.OnCompleted(() => 
                { if (m_IDs != null) //update UI
                    {                        
                        ListBox.ObjectCollection itms = ListBox1.Items;
                        itms.Clear();
                        ListBox1.DataSource = m_IDs;
                        lblTotalRows.Text = m_IDs.Length.ToString("N0");
                        lblCacheConfig.Text = $"Sets #: {m_NumberOfSets}, Items Per Set: {m_NumberOfRecordsPerSet},  Algorithm:  {m_chacheAlgorithm}";
                        /*
                        foreach (int key in m_IDs)
                        {
                            itms.Add(key);
                            if (key % 500 == 0) Application.DoEvents();
                        }
                        */
                        m_IDs = null;
                        UseWaitCursor = false;
                        lblFileMessage.Text = C_DefaultFileMSG;                        
                    }
                }
                );
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// calls <see cref="LoadCache"/>
        /// </summary>
        /// <returns></returns>
        private Task LoadCacheAsync() {
            ClearCache();
            lblTotalRows.Text = "?";
            lblCacheConfig.Text = "?";
            Task t = Task.Run(() => LoadCache());
            return t;
        }



        private void LoadCache() {
            CacheDataProvider CacheProv = null; //custom cache initial load provider
            try
            {
                Config<OrderDetail, int> cfg = null; //config object that will be used to instantiate the Cache
                //LRU, automatic Set Size calculation;
                if (rbSizeOnly.Checked) cfg = new Config<OrderDetail, int>((byte)(NumericUpDown1.Value));                 
                //use custom eviction strategy delegate. Explicit cache and set sizes
                else if (rbSizesAndCustomStrategy.Checked) cfg = new Config<OrderDetail, int>(
                     (byte)(NumericUpDown1.Value), (byte)(NumericUpDown2.Value), ProcessCustomEvictionRequest);
                //LRU or MRU eviction strategy. Explicit cache and set sizes
                else cfg = new Config<OrderDetail, int>((byte)(NumericUpDown1.Value), (byte)(NumericUpDown2.Value), rbMRU.Checked);

                cfg.LoadCacheMaxSeconds = (int)(numLoadTimeMinutes.Value) * 60;
                cfg.CacheExpirationMinutes = (int)(numExpirationMinutes.Value);
                //Setting LoadHandler delegate here simplifies cache use case scenario. See LoadDetail implementation
                cfg.LoadHandler = LoadDetail; 
                cfg.ThreadsMaxNumberOfScan = (byte)(numMaxScanThreads.Value);
                m_NumberOfSets = cfg.NumberOfSets;
                m_NumberOfRecordsPerSet = cfg.ItemsPerSet;
                m_chacheAlgorithm = cfg.CacheType.ToString();
                //Pass in initial loader implementation
                CacheProv = new CacheDataProvider(lblDataFile.Text, (int)(numInitialCacheItems.Value));
                
                //instantiate the cache object
                m_cache = new Cache<OrderDetail, int>(cfg, CacheProv);
                //pull the list of indexes to be used as cache keys.
                m_IDs = OrderDetail.GetRowIdxList(lblDataFile.Text); 
            }
            catch 
            {
               throw;
            }
            finally {
                if (CacheProv != null) CacheProv.Dispose();
            }
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ClearCache();
        }


        private void ClearCache() { m_IDs = null; if (m_cache != null) { m_cache.Dispose(); m_cache = null; } }

        private void Rb_CheckedChanged(object sender, EventArgs e) => NumericUpDown2.Enabled = !rbSizeOnly.Checked;
        

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CalcSizes();
        }


        /// <summary>
        /// Implements the following custom replacement strategy: Evict item mith lowest Unit Price
        /// </summary>
        /// <param name="args"></param>
        private void ProcessCustomEvictionRequest(ReplacementArgs<OrderDetail, int> args) {
            CacheItem<OrderDetail, int> item = args.CurrentCacheItem;
            List<int> lst = args.ListOfNextBestEvictionCandidateIDXs;
            if (lst.Count == 0) lst.Add(args.CurrentIndex);
            else {
                CacheItem<OrderDetail, int> lastItem = args.Fetcher(lst[0]);
                if (lastItem.Value.UnitPrice > item.Value.UnitPrice) {lst.Clear(); lst.Add(args.CurrentIndex); }
            }
            if (args.IsLastCallForThisScan && lst.Count > 0) args.CommitItemWithIndexForEvicton = lst[0];
        }

        /// <summary>
        /// Value (Detail) loader
        /// </summary>
        /// <param name="rowIDX">row index to read from storage file</param>
        /// <returns></returns>
        private OrderDetail LoadDetail(int rowIDX) {
            return OrderDetail.Get(lblDataFile.Text, rowIDX);
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Application.DoEvents();
            DateTime datStart = DateTime.UtcNow;
            if (m_IDs != null) return;
            if (ListBox1.SelectedIndex > -1 && m_cache != null) {                
                if (m_cache.PullItem((int)(ListBox1.SelectedItem), out OrderDetail orderDet)) {
                    textBox1.Text = orderDet.ToString();
                    lblTimeElapsed.Text = ((DateTime.UtcNow - datStart).Ticks /10).ToString("N0");
                } 
            }
        }        
    }
}
