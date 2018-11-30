using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace CacheTestClient
{
    /// <summary>
    /// Test POCO type to be used as a chache value
    /// </summary>
    internal class OrderDetail
    {
        public OrderDetail(): base() { }

        internal OrderDetail(string[] data) : base() {
            if (data != null && data.Length == 8) {
                ID = Guid.Parse(data[0]);
                UnitPrice = decimal.Parse(data[1]);
                TrackingNumber = data[2];
                RowIndex = int.Parse(data[3]);
                AccountNumber = data[4];
                OrderNumber = data[5];
                Customer = data[6];
                DateOfOrder = DateTime.Parse(data[7]);
            }            
        }

        /// <summary>
        /// Alternative object ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Unit Price.
        /// </summary>
        /// <remarks><see cref="frmTest.ProcessCustomEvictionRequest"/> for details of how it's
        /// used in that custom eviction strategy implementation</remarks>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Order Tracking number
        /// </summary>
        public string TrackingNumber { get; set; } = "";

        /// <summary>
        /// Order Index. This property is used as a cache Key
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// Customer account number
        /// </summary>
        public string AccountNumber { get; set; } = "";
        /// <summary>
        /// Order Number
        /// </summary>
        public string OrderNumber { get; set; } = "";
        /// <summary>
        /// Customer name
        /// </summary>
        public string Customer { get; set; } = "";
        /// <summary>
        /// Order date
        /// </summary>
        public DateTime DateOfOrder { get; set; }

        
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            using (var writer = new StringWriter(sb)) {
                var ser = new Newtonsoft.Json.JsonSerializer();
                ser.Serialize(writer, this);
            }
            return sb.ToString();            
        }

        public override int GetHashCode() => RowIndex;

        public override bool Equals(object obj) => (obj != null) && obj is OrderDetail && ((OrderDetail)obj).ID == ID;


        /// <summary>
        /// Returns order detail object for a given storage file and detail index
        /// </summary>
        /// <param name="sourceFile">Storage file</param>
        /// <param name="rowIndex">Order detail index</param>
        /// <returns></returns>
        public static OrderDetail Get(string sourceFile, int rowIndex) {
            if (rowIndex < 0) return null;
            using (var rdr = new OrderDetailsReader(sourceFile))
            {
                do
                {
                    var data = rdr.GetNextRow(out Guid id);
                    if (data != null && id != Guid.Empty) {
                        if (int.Parse(data[3]) == rowIndex) return new OrderDetail(data);
                    }
                } while (!rdr.EOF);
            }
            return null;
        }       

        /// <summary>
        /// Returns all order detail objects stored in a given file
        /// </summary>
        /// <param name="sourceFile">storage file</param>
        /// <returns>Readonly list of all oredr detail records deserialized from storage file</returns>
        public static ReadOnlyCollection<OrderDetail> GetList(string sourceFile) {
            var list = new List<OrderDetail>();            
            using (var rdr = new OrderDetailsReader(sourceFile))
            {                 
                do                {
                    var det = rdr.GetNextItem();
                    if (det != null) list.Add(det);                     
                } while (!rdr.EOF);
            }
            return list.AsReadOnly();
        }

        /// <summary>
        /// Returns all order detail object row indexes stored in a given file
        /// </summary>
        /// <param name="sourceFile">storage file</param>
        /// <returns>Array of all oredr detail record indexes deserialized from storage file</returns>
        public static int[] GetRowIdxList(string sourceFile) {
            var lst = new List<int>();
            using (var rdr = new OrderDetailsReader(sourceFile))
            {
                do                {
                    var data = rdr.GetNextRow(out Guid id);
                    if (data != null && id != Guid.Empty) lst.Add(int.Parse(data[3]));
                } while (!rdr.EOF);
            }
            return lst.ToArray();
        }


        

    }

    /// <summary>
    /// Provides CSV data file handling functionality.
    /// </summary>
    internal class OrderDetailsReader :IDisposable {
        private StreamReader m_Reader = null;
        private CsvHelper.CsvParser m_parser;
        internal OrderDetailsReader(string sourceFile) {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException($"Source file {sourceFile} does not exist.");
            m_Reader = new System.IO.StreamReader(sourceFile);
            m_parser = new CsvHelper.CsvParser(m_Reader);
        }


        /// <summary>
        /// Returns the name of the default test data storage file
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultSourceFile()
        {
            var pathS = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            var fileName = Path.Combine(pathS, "orderData.csv");
            while (pathS.Length > 3 && !File.Exists(fileName))
            {
                pathS = Path.GetDirectoryName(pathS);
                fileName = Path.Combine(pathS, "orderData.csv");
            }
            return fileName;
        }

        /// <summary>
        /// Reads next data row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] GetNextRow(out Guid id) {
            id = Guid.Empty;
            if (!EOF && m_parser != null) {
                string[] res = m_parser.Read();
                if (res == null)
                {
                    EOF = true;
                    Dispose();                    
                }
                else
                {
                    BOF = false;                    
                    if (!Guid.TryParse(res[0], out id)) {
                        res = GetNextRow(out id);
                    }
                }
                return res;
            }
            return null;
        }
        /// <summary>
        /// Reads next order detail item from the storage
        /// </summary>
        /// <returns></returns>
        public OrderDetail GetNextItem() {
            string[] data = GetNextRow(out Guid id);
            if (data != null && id != Guid.Empty) return new OrderDetail(data);
            return null;
        }

        /// <summary>
        /// End Of File flag
        /// </summary>
        public bool EOF { get; internal set; } = false;

        /// <summary>
        /// Beginning Of File flag
        /// </summary>
        public bool BOF { get; internal set; } = true;


        public void Dispose() {

            if (m_parser != null)
            {
                m_parser.Dispose();
                m_parser = null;
            }


            if (m_Reader != null) {
                m_Reader.Dispose();
                m_Reader = null;
            }
            
        }

    }
}

