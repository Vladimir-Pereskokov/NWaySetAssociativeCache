namespace CacheTestClient
{
    partial class frmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.numLoadTimeMinutes = new System.Windows.Forms.NumericUpDown();
            this.numExpirationMinutes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.rbSizeOnly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numInitialCacheItems = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSetSize = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.rbLRU = new System.Windows.Forms.RadioButton();
            this.rbMRU = new System.Windows.Forms.RadioButton();
            this.rbSizesAndCustomStrategy = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.numMaxScanThreads = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCacheConfig = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDataFile = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileMessage = new System.Windows.Forms.Label();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoadTimeMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpirationMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInitialCacheItems)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxScanThreads)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.92308F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 583);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(14, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(801, 227);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test options:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.numLoadTimeMinutes, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.numExpirationMinutes, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbSizeOnly, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numInitialCacheItems, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbLRU, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbMRU, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.rbSizesAndCustomStrategy, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label12, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.numMaxScanThreads, 2, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(14, 19);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(14, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66708F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66542F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(784, 204);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // numLoadTimeMinutes
            // 
            this.numLoadTimeMinutes.Dock = System.Windows.Forms.DockStyle.Top;
            this.numLoadTimeMinutes.Location = new System.Drawing.Point(563, 139);
            this.numLoadTimeMinutes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numLoadTimeMinutes.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numLoadTimeMinutes.Name = "numLoadTimeMinutes";
            this.numLoadTimeMinutes.Size = new System.Drawing.Size(218, 22);
            this.numLoadTimeMinutes.TabIndex = 12;
            this.numLoadTimeMinutes.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numExpirationMinutes
            // 
            this.numExpirationMinutes.Dock = System.Windows.Forms.DockStyle.Top;
            this.numExpirationMinutes.Location = new System.Drawing.Point(563, 106);
            this.numExpirationMinutes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numExpirationMinutes.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numExpirationMinutes.Name = "numExpirationMinutes";
            this.numExpirationMinutes.Size = new System.Drawing.Size(218, 22);
            this.numExpirationMinutes.TabIndex = 10;
            this.numExpirationMinutes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Set Size (Power of 2):";
            // 
            // rbSizeOnly
            // 
            this.rbSizeOnly.AutoSize = true;
            this.rbSizeOnly.Location = new System.Drawing.Point(3, 7);
            this.rbSizeOnly.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.rbSizeOnly.Name = "rbSizeOnly";
            this.rbSizeOnly.Size = new System.Drawing.Size(193, 20);
            this.rbSizeOnly.TabIndex = 0;
            this.rbSizeOnly.Text = "Set bits = LOG2(Cache Bits)";
            this.rbSizeOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Size (Power of 2):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(379, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Initial number of cache items:";
            // 
            // numInitialCacheItems
            // 
            this.numInitialCacheItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.numInitialCacheItems.Location = new System.Drawing.Point(563, 72);
            this.numInitialCacheItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numInitialCacheItems.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numInitialCacheItems.Name = "numInitialCacheItems";
            this.numInitialCacheItems.Size = new System.Drawing.Size(218, 22);
            this.numInitialCacheItems.TabIndex = 8;
            this.numInitialCacheItems.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(379, 109);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Expiration interval (Min.):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(379, 142);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Load time limit (Min.):";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel6.Controls.Add(this.lblSetSize, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblSize, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.NumericUpDown1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.NumericUpDown2, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(560, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel6, 2);
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(224, 68);
            this.tableLayoutPanel6.TabIndex = 13;
            // 
            // lblSetSize
            // 
            this.lblSetSize.AutoSize = true;
            this.lblSetSize.Location = new System.Drawing.Point(81, 41);
            this.lblSetSize.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.lblSetSize.Name = "lblSetSize";
            this.lblSetSize.Size = new System.Drawing.Size(135, 16);
            this.lblSetSize.TabIndex = 8;
            this.lblSetSize.Text = "Set Size (Power of 2):";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(81, 7);
            this.lblSize.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(111, 16);
            this.lblSize.TabIndex = 7;
            this.lblSize.Text = "Size (Power of 2):";
            // 
            // NumericUpDown1
            // 
            this.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.NumericUpDown1.Location = new System.Drawing.Point(3, 4);
            this.NumericUpDown1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NumericUpDown1.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.NumericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumericUpDown1.Name = "NumericUpDown1";
            this.NumericUpDown1.Size = new System.Drawing.Size(72, 22);
            this.NumericUpDown1.TabIndex = 3;
            this.NumericUpDown1.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.NumericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // NumericUpDown2
            // 
            this.NumericUpDown2.Dock = System.Windows.Forms.DockStyle.Top;
            this.NumericUpDown2.Location = new System.Drawing.Point(3, 38);
            this.NumericUpDown2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NumericUpDown2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumericUpDown2.Name = "NumericUpDown2";
            this.NumericUpDown2.Size = new System.Drawing.Size(72, 22);
            this.NumericUpDown2.TabIndex = 6;
            this.NumericUpDown2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
            // 
            // rbLRU
            // 
            this.rbLRU.AutoSize = true;
            this.rbLRU.Checked = true;
            this.rbLRU.Location = new System.Drawing.Point(3, 41);
            this.rbLRU.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.rbLRU.Name = "rbLRU";
            this.rbLRU.Size = new System.Drawing.Size(341, 20);
            this.rbLRU.TabIndex = 14;
            this.rbLRU.TabStop = true;
            this.rbLRU.Text = "Specify Cache Size, Set Size + LRU Eviction Strategy";
            this.rbLRU.UseVisualStyleBackColor = true;
            // 
            // rbMRU
            // 
            this.rbMRU.AutoSize = true;
            this.rbMRU.Location = new System.Drawing.Point(3, 75);
            this.rbMRU.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.rbMRU.Name = "rbMRU";
            this.rbMRU.Size = new System.Drawing.Size(345, 20);
            this.rbMRU.TabIndex = 15;
            this.rbMRU.Text = "Specify Cache Size, Set Size + MRU Eviction Strategy";
            this.rbMRU.UseVisualStyleBackColor = true;
            // 
            // rbSizesAndCustomStrategy
            // 
            this.rbSizesAndCustomStrategy.AutoSize = true;
            this.rbSizesAndCustomStrategy.Location = new System.Drawing.Point(3, 109);
            this.rbSizesAndCustomStrategy.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.rbSizesAndCustomStrategy.Name = "rbSizesAndCustomStrategy";
            this.rbSizesAndCustomStrategy.Size = new System.Drawing.Size(370, 19);
            this.rbSizesAndCustomStrategy.TabIndex = 2;
            this.rbSizesAndCustomStrategy.Text = "Specify Cache Size, Set Size and custom Eviction strategy";
            this.rbSizesAndCustomStrategy.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(379, 176);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 16);
            this.label12.TabIndex = 16;
            this.label12.Text = "Max # of Scan Threads:";
            // 
            // numMaxScanThreads
            // 
            this.numMaxScanThreads.Dock = System.Windows.Forms.DockStyle.Top;
            this.numMaxScanThreads.Location = new System.Drawing.Point(563, 173);
            this.numMaxScanThreads.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMaxScanThreads.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numMaxScanThreads.Name = "numMaxScanThreads";
            this.numMaxScanThreads.Size = new System.Drawing.Size(218, 22);
            this.numMaxScanThreads.TabIndex = 17;
            this.numMaxScanThreads.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.60175F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.39825F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.ListBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 319);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(801, 260);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Select a Key below:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Value (JSON):";
            // 
            // ListBox1
            // 
            this.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 16;
            this.ListBox1.Location = new System.Drawing.Point(3, 34);
            this.ListBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(143, 222);
            this.ListBox1.TabIndex = 7;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.lblCacheConfig, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblTotalRows, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label10, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.lblTimeElapsed, 1, 3);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(149, 30);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(652, 227);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // lblCacheConfig
            // 
            this.lblCacheConfig.AutoSize = true;
            this.lblCacheConfig.Location = new System.Drawing.Point(162, 174);
            this.lblCacheConfig.Margin = new System.Windows.Forms.Padding(12, 7, 3, 7);
            this.lblCacheConfig.Name = "lblCacheConfig";
            this.lblCacheConfig.Size = new System.Drawing.Size(15, 16);
            this.lblCacheConfig.TabIndex = 14;
            this.lblCacheConfig.Text = "?";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 174);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Cache congifuration:";
            // 
            // textBox1
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(646, 129);
            this.textBox1.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Total # rows in data file:";
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Location = new System.Drawing.Point(162, 144);
            this.lblTotalRows.Margin = new System.Windows.Forms.Padding(12, 7, 3, 7);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(15, 16);
            this.lblTotalRows.TabIndex = 11;
            this.lblTotalRows.Text = "?";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 204);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Pull Time  ( μs ):";
            // 
            // lblTimeElapsed
            // 
            this.lblTimeElapsed.AutoSize = true;
            this.lblTimeElapsed.Location = new System.Drawing.Point(162, 204);
            this.lblTimeElapsed.Margin = new System.Windows.Forms.Padding(12, 7, 3, 7);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(15, 16);
            this.lblTimeElapsed.TabIndex = 12;
            this.lblTimeElapsed.Text = "?";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.27569F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.72431F));
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblDataFile, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(16, 238);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(788, 74);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Data File:";
            // 
            // lblDataFile
            // 
            this.lblDataFile.AutoSize = true;
            this.lblDataFile.Location = new System.Drawing.Point(83, 7);
            this.lblDataFile.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.lblDataFile.Name = "lblDataFile";
            this.lblDataFile.Size = new System.Drawing.Size(52, 16);
            this.lblDataFile.TabIndex = 6;
            this.lblDataFile.Text = "<none>";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.lblFileMessage, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.BtnLoad, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(80, 30);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(708, 44);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // lblFileMessage
            // 
            this.lblFileMessage.AutoSize = true;
            this.lblFileMessage.Location = new System.Drawing.Point(3, 7);
            this.lblFileMessage.Margin = new System.Windows.Forms.Padding(3, 7, 12, 7);
            this.lblFileMessage.Name = "lblFileMessage";
            this.lblFileMessage.Size = new System.Drawing.Size(166, 16);
            this.lblFileMessage.TabIndex = 7;
            this.lblFileMessage.Text = "<Data File does not exist >";
            // 
            // BtnLoad
            // 
            this.BtnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLoad.AutoSize = true;
            this.BtnLoad.Location = new System.Drawing.Point(609, 3);
            this.BtnLoad.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(93, 29);
            this.BtnLoad.TabIndex = 0;
            this.BtnLoad.Text = "Load Data ...";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 583);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmTest";
            this.ShowIcon = false;
            this.Text = "N-way Set Accociative Cache Test";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLoadTimeMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExpirationMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInitialCacheItems)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxScanThreads)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rbSizeOnly;
        private System.Windows.Forms.RadioButton rbSizesAndCustomStrategy;
        private System.Windows.Forms.NumericUpDown NumericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox ListBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDataFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.Label lblFileMessage;
        private System.Windows.Forms.NumericUpDown numLoadTimeMinutes;
        private System.Windows.Forms.NumericUpDown numExpirationMinutes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numInitialCacheItems;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblSetSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.RadioButton rbMRU;
        private System.Windows.Forms.RadioButton rbLRU;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.Label lblTimeElapsed;
        private System.Windows.Forms.Label lblCacheConfig;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numMaxScanThreads;
    }
}

