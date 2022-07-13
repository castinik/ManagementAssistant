namespace ManagementAssistant
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listViewStocks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tab2 = new System.Windows.Forms.TabControl();
            this.tabPageStock = new System.Windows.Forms.TabPage();
            this.tabPageCrypto = new System.Windows.Forms.TabPage();
            this.listViewCrypto = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageForex = new System.Windows.Forms.TabPage();
            this.listViewForex = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmBoxIntraday = new System.Windows.Forms.ComboBox();
            this.ckBoxIntraday = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmBoxTimeFrame = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddMarket = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txBxSearchForex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txBxSearchCrypto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txBxSearchStock = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLastRefresh = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.boxMessages = new System.Windows.Forms.RichTextBox();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tab2.SuspendLayout();
            this.tabPageStock.SuspendLayout();
            this.tabPageCrypto.SuspendLayout();
            this.tabPageForex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series5.Legend = "MarketLegend";
            series5.Name = "Daily";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series5.YValuesPerPoint = 4;
            series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(557, 588);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // listViewStocks
            // 
            this.listViewStocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4});
            this.listViewStocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewStocks.GridLines = true;
            this.listViewStocks.HideSelection = false;
            this.listViewStocks.Location = new System.Drawing.Point(3, 3);
            this.listViewStocks.Name = "listViewStocks";
            this.listViewStocks.Size = new System.Drawing.Size(278, 395);
            this.listViewStocks.TabIndex = 1;
            this.listViewStocks.UseCompatibleStateImageBehavior = false;
            this.listViewStocks.View = System.Windows.Forms.View.Details;
            this.listViewStocks.SelectedIndexChanged += new System.EventHandler(this.listViewStocks_SelectedIndexChanged);
            this.listViewStocks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewStocks_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Stocks";
            this.columnHeader1.Width = 136;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.AutoScrollMinSize = new System.Drawing.Size(300, 0);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 700);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tab2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer4.Size = new System.Drawing.Size(300, 700);
            this.splitContainer4.SplitterDistance = 436;
            this.splitContainer4.TabIndex = 0;
            // 
            // tab2
            // 
            this.tab2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab2.Controls.Add(this.tabPageStock);
            this.tab2.Controls.Add(this.tabPageCrypto);
            this.tab2.Controls.Add(this.tabPageForex);
            this.tab2.Location = new System.Drawing.Point(6, 3);
            this.tab2.Name = "tab2";
            this.tab2.SelectedIndex = 0;
            this.tab2.Size = new System.Drawing.Size(292, 430);
            this.tab2.TabIndex = 3;
            // 
            // tabPageStock
            // 
            this.tabPageStock.Controls.Add(this.listViewStocks);
            this.tabPageStock.Location = new System.Drawing.Point(4, 25);
            this.tabPageStock.Name = "tabPageStock";
            this.tabPageStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStock.Size = new System.Drawing.Size(284, 401);
            this.tabPageStock.TabIndex = 0;
            this.tabPageStock.Text = "Stocks";
            this.tabPageStock.UseVisualStyleBackColor = true;
            this.tabPageStock.Enter += new System.EventHandler(this.tabPageStock_Enter);
            // 
            // tabPageCrypto
            // 
            this.tabPageCrypto.Controls.Add(this.listViewCrypto);
            this.tabPageCrypto.Location = new System.Drawing.Point(4, 25);
            this.tabPageCrypto.Name = "tabPageCrypto";
            this.tabPageCrypto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCrypto.Size = new System.Drawing.Size(284, 401);
            this.tabPageCrypto.TabIndex = 1;
            this.tabPageCrypto.Text = "Crypto";
            this.tabPageCrypto.UseVisualStyleBackColor = true;
            this.tabPageCrypto.Enter += new System.EventHandler(this.tabPageCrypto_Enter);
            // 
            // listViewCrypto
            // 
            this.listViewCrypto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader5});
            this.listViewCrypto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCrypto.GridLines = true;
            this.listViewCrypto.HideSelection = false;
            this.listViewCrypto.Location = new System.Drawing.Point(3, 3);
            this.listViewCrypto.Name = "listViewCrypto";
            this.listViewCrypto.Size = new System.Drawing.Size(278, 395);
            this.listViewCrypto.TabIndex = 2;
            this.listViewCrypto.UseCompatibleStateImageBehavior = false;
            this.listViewCrypto.View = System.Windows.Forms.View.Details;
            this.listViewCrypto.SelectedIndexChanged += new System.EventHandler(this.listViewCrypto_SelectedIndexChanged);
            this.listViewCrypto.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewCrypto_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Cryptocurrencies";
            this.columnHeader2.Width = 137;
            // 
            // tabPageForex
            // 
            this.tabPageForex.Controls.Add(this.listViewForex);
            this.tabPageForex.Location = new System.Drawing.Point(4, 25);
            this.tabPageForex.Name = "tabPageForex";
            this.tabPageForex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForex.Size = new System.Drawing.Size(284, 401);
            this.tabPageForex.TabIndex = 2;
            this.tabPageForex.Text = "Forex";
            this.tabPageForex.UseVisualStyleBackColor = true;
            this.tabPageForex.Enter += new System.EventHandler(this.tabPageForex_Enter);
            // 
            // listViewForex
            // 
            this.listViewForex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewForex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewForex.GridLines = true;
            this.listViewForex.HideSelection = false;
            this.listViewForex.Location = new System.Drawing.Point(3, 3);
            this.listViewForex.Name = "listViewForex";
            this.listViewForex.Size = new System.Drawing.Size(278, 395);
            this.listViewForex.TabIndex = 2;
            this.listViewForex.UseCompatibleStateImageBehavior = false;
            this.listViewForex.View = System.Windows.Forms.View.Details;
            this.listViewForex.SelectedIndexChanged += new System.EventHandler(this.listViewForex_SelectedIndexChanged);
            this.listViewForex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewForex_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Forex";
            this.columnHeader3.Width = 111;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 254);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 230);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cmBoxIntraday);
            this.tabPage1.Controls.Add(this.ckBoxIntraday);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(283, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tools";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmBoxIntraday
            // 
            this.cmBoxIntraday.Enabled = false;
            this.cmBoxIntraday.FormattingEnabled = true;
            this.cmBoxIntraday.Items.AddRange(new object[] {
            "1 minute",
            "5 minute",
            "15 minute",
            "30 minute",
            "60 minute"});
            this.cmBoxIntraday.Location = new System.Drawing.Point(24, 88);
            this.cmBoxIntraday.Name = "cmBoxIntraday";
            this.cmBoxIntraday.Size = new System.Drawing.Size(121, 24);
            this.cmBoxIntraday.TabIndex = 3;
            // 
            // ckBoxIntraday
            // 
            this.ckBoxIntraday.AutoSize = true;
            this.ckBoxIntraday.Location = new System.Drawing.Point(24, 62);
            this.ckBoxIntraday.Name = "ckBoxIntraday";
            this.ckBoxIntraday.Size = new System.Drawing.Size(111, 20);
            this.ckBoxIntraday.TabIndex = 2;
            this.ckBoxIntraday.Text = "Go In Intraday";
            this.ckBoxIntraday.UseVisualStyleBackColor = true;
            this.ckBoxIntraday.CheckedChanged += new System.EventHandler(this.ckBoxIntraday_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Search";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cmBoxTimeFrame);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.btnAddMarket);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txBxSearchForex);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txBxSearchCrypto);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txBxSearchStock);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(271, 209);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Serch in the market and download";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Time frame:";
            // 
            // cmBoxTimeFrame
            // 
            this.cmBoxTimeFrame.FormattingEnabled = true;
            this.cmBoxTimeFrame.Items.AddRange(new object[] {
            "1 minute",
            "5 minute",
            "15 minute",
            "30 minute",
            "60 minute"});
            this.cmBoxTimeFrame.Location = new System.Drawing.Point(86, 115);
            this.cmBoxTimeFrame.Name = "cmBoxTimeFrame";
            this.cmBoxTimeFrame.Size = new System.Drawing.Size(179, 24);
            this.cmBoxTimeFrame.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(153, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 26);
            this.label6.TabIndex = 7;
            this.label6.Text = "*Only chars - /\r\nex: EUR-USD";
            // 
            // btnAddMarket
            // 
            this.btnAddMarket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMarket.Location = new System.Drawing.Point(5, 148);
            this.btnAddMarket.Name = "btnAddMarket";
            this.btnAddMarket.Size = new System.Drawing.Size(260, 41);
            this.btnAddMarket.TabIndex = 0;
            this.btnAddMarket.Text = "Go";
            this.btnAddMarket.UseVisualStyleBackColor = true;
            this.btnAddMarket.Click += new System.EventHandler(this.btnAddMarket_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Forex*:";
            // 
            // txBxSearchForex
            // 
            this.txBxSearchForex.Location = new System.Drawing.Point(59, 78);
            this.txBxSearchForex.Name = "txBxSearchForex";
            this.txBxSearchForex.Size = new System.Drawing.Size(88, 22);
            this.txBxSearchForex.TabIndex = 5;
            this.txBxSearchForex.TextChanged += new System.EventHandler(this.txBxSearchForex_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Crypto:";
            // 
            // txBxSearchCrypto
            // 
            this.txBxSearchCrypto.Location = new System.Drawing.Point(59, 48);
            this.txBxSearchCrypto.Name = "txBxSearchCrypto";
            this.txBxSearchCrypto.Size = new System.Drawing.Size(88, 22);
            this.txBxSearchCrypto.TabIndex = 3;
            this.txBxSearchCrypto.TextChanged += new System.EventHandler(this.txBxSearchCrypto_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stock:";
            // 
            // txBxSearchStock
            // 
            this.txBxSearchStock.Location = new System.Drawing.Point(59, 20);
            this.txBxSearchStock.Name = "txBxSearchStock";
            this.txBxSearchStock.Size = new System.Drawing.Size(88, 22);
            this.txBxSearchStock.TabIndex = 1;
            this.txBxSearchStock.TextChanged += new System.EventHandler(this.txBxSearchMarket_TextChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(791, 700);
            this.splitContainer2.SplitterDistance = 594;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Size = new System.Drawing.Size(791, 594);
            this.splitContainer3.SplitterDistance = 563;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblCurrentValue);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.lblMaxValue);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.lblMinValue);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblLastRefresh);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 591);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informations";
            // 
            // lblCurrentValue
            // 
            this.lblCurrentValue.AutoSize = true;
            this.lblCurrentValue.Location = new System.Drawing.Point(34, 189);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(22, 16);
            this.lblCurrentValue.TabIndex = 11;
            this.lblCurrentValue.Text = ". . .";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 189);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "->";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 18);
            this.label14.TabIndex = 9;
            this.label14.Text = "Current value:";
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(34, 86);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(22, 16);
            this.lblMaxValue.TabIndex = 8;
            this.lblMaxValue.Text = ". . .";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "->";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 18);
            this.label13.TabIndex = 6;
            this.label13.Text = "Max value:";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new System.Drawing.Point(34, 136);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(22, 16);
            this.lblMinValue.TabIndex = 5;
            this.lblMinValue.Text = ". . .";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "->";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "Min value:";
            // 
            // lblLastRefresh
            // 
            this.lblLastRefresh.AutoSize = true;
            this.lblLastRefresh.Location = new System.Drawing.Point(34, 41);
            this.lblLastRefresh.Name = "lblLastRefresh";
            this.lblLastRefresh.Size = new System.Drawing.Size(22, 16);
            this.lblLastRefresh.TabIndex = 2;
            this.lblLastRefresh.Text = ". . .";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "->";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last refresh:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.boxMessages);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Messages";
            // 
            // boxMessages
            // 
            this.boxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxMessages.Location = new System.Drawing.Point(3, 18);
            this.boxMessages.Name = "boxMessages";
            this.boxMessages.Size = new System.Drawing.Size(779, 75);
            this.boxMessages.TabIndex = 3;
            this.boxMessages.Text = "";
            this.boxMessages.TextChanged += new System.EventHandler(this.boxMessages_TextChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Names";
            this.columnHeader4.Width = 177;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Names";
            this.columnHeader5.Width = 124;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(63, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "GetCandle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 700);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Management Assistant";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tabPageStock.ResumeLayout(false);
            this.tabPageCrypto.ResumeLayout(false);
            this.tabPageForex.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListView listViewStocks;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txBxSearchStock;
        private System.Windows.Forms.Button btnAddMarket;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tab2;
        private System.Windows.Forms.TabPage tabPageStock;
        private System.Windows.Forms.TabPage tabPageCrypto;
        private System.Windows.Forms.TabPage tabPageForex;
        private System.Windows.Forms.ListView listViewCrypto;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewForex;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox cmBoxIntraday;
        private System.Windows.Forms.CheckBox ckBoxIntraday;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblLastRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox boxMessages;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txBxSearchForex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txBxSearchCrypto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmBoxTimeFrame;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button1;
    }
}

