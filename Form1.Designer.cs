namespace ManagementAssistantTester
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.boxMessages = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.btnAddValue = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClose = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLow = new System.Windows.Forms.TextBox();
            this.txtHigh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOpen = new System.Windows.Forms.TextBox();
            this.listChart = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmCode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.boxMessages);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.listChart);
            this.splitContainer1.Panel1MinSize = 357;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(1077, 704);
            this.splitContainer1.SplitterDistance = 364;
            this.splitContainer1.TabIndex = 0;
            // 
            // boxMessages
            // 
            this.boxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMessages.Location = new System.Drawing.Point(3, 580);
            this.boxMessages.MaximumSize = new System.Drawing.Size(355, 120);
            this.boxMessages.MinimumSize = new System.Drawing.Size(355, 120);
            this.boxMessages.Name = "boxMessages";
            this.boxMessages.Size = new System.Drawing.Size(355, 120);
            this.boxMessages.TabIndex = 2;
            this.boxMessages.Text = "";
            this.boxMessages.TextChanged += new System.EventHandler(this.boxMessages_TextChanged_1);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cmCode);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.btnAddValue);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtClose);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtLow);
            this.panel1.Controls.Add(this.txtHigh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtOpen);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MaximumSize = new System.Drawing.Size(355, 170);
            this.panel1.MinimumSize = new System.Drawing.Size(355, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 170);
            this.panel1.TabIndex = 1;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(184, 132);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(157, 23);
            this.btnView.TabIndex = 10;
            this.btnView.Text = "View chart";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(9, 132);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(157, 23);
            this.btnAddValue.TabIndex = 1;
            this.btnAddValue.Text = "New value";
            this.btnAddValue.UseVisualStyleBackColor = true;
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Code";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(233, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "X - Value";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(73, 93);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(100, 22);
            this.txtDate.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Close";
            // 
            // txtClose
            // 
            this.txtClose.Location = new System.Drawing.Point(233, 37);
            this.txtClose.Name = "txtClose";
            this.txtClose.Size = new System.Drawing.Size(100, 22);
            this.txtClose.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Low";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "High";
            // 
            // txtLow
            // 
            this.txtLow.Location = new System.Drawing.Point(233, 65);
            this.txtLow.Name = "txtLow";
            this.txtLow.Size = new System.Drawing.Size(100, 22);
            this.txtLow.TabIndex = 2;
            // 
            // txtHigh
            // 
            this.txtHigh.Location = new System.Drawing.Point(73, 65);
            this.txtHigh.Name = "txtHigh";
            this.txtHigh.Size = new System.Drawing.Size(100, 22);
            this.txtHigh.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Open";
            // 
            // txtOpen
            // 
            this.txtOpen.Location = new System.Drawing.Point(73, 37);
            this.txtOpen.Name = "txtOpen";
            this.txtOpen.Size = new System.Drawing.Size(100, 22);
            this.txtOpen.TabIndex = 0;
            // 
            // listChart
            // 
            this.listChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listChart.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listChart.HideSelection = false;
            this.listChart.Location = new System.Drawing.Point(3, 178);
            this.listChart.MaximumSize = new System.Drawing.Size(355, 2000);
            this.listChart.MinimumSize = new System.Drawing.Size(355, 400);
            this.listChart.Name = "listChart";
            this.listChart.Size = new System.Drawing.Size(355, 400);
            this.listChart.TabIndex = 0;
            this.listChart.UseCompatibleStateImageBehavior = false;
            this.listChart.View = System.Windows.Forms.View.Details;
            this.listChart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listChart_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 33;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "X - Value";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Open";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "High";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Low";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Close";
            this.columnHeader6.Width = 62;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.Title = "X - Value";
            chartArea1.AxisY.Title = "Y - Value";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(3, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(706, 704);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // cmCode
            // 
            this.cmCode.FormattingEnabled = true;
            this.cmCode.Location = new System.Drawing.Point(73, 6);
            this.cmCode.Name = "cmCode";
            this.cmCode.Size = new System.Drawing.Size(100, 24);
            this.cmCode.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 704);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "MATester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnAddValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLow;
        private System.Windows.Forms.TextBox txtHigh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOpen;
        private System.Windows.Forms.ListView listChart;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox boxMessages;
        private System.Windows.Forms.ComboBox cmCode;
    }
}

