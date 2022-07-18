using ManagementAssistantTester.Data;
using ManagementAssistantTester.Data.DataModel;
using ManagementAssistantTester.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ManagementAssistantTester
{
    public partial class Form1 : Form
    {
        private delegate void AutoDelegate();
        private Manager _manager = new Manager();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _manager.Message += Manager_Message;
            _manager.UpdateChart += Manager_UpdateChart;
            _manager.UpdateList += Manager_UpdateList;
            _manager.GetSymbolsDb += Manager_GetSymbolsDb;
            _manager.CleanData += Manager_CleanData;
            _manager.GetSymbols();
        }

        #region ******************************** EVENTI PER IL FORM ********************************
        private void Manager_Message(object sender, Data.DataModel.MessageDataModel e)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                boxMessages.ForeColor = e.IsError ? Color.Red : Color.Black;
                boxMessages.Text += $"{e.Message}\n";
            });
        }
        //Reindirizza l'output per le liste di scelta
        private void Manager_UpdateList(object sender, List<DataRowModel> list)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                listChart.Items.Clear();
                foreach (DataRowModel dataRow in list.OrderBy(x => x.Xvalue))
                {
                    ListViewItem listViewItem = new ListViewItem(dataRow.Id.ToString());

                    ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                    listViewSubItem.Text = dataRow.Xvalue.ToString();
                    listViewItem.SubItems.Add(listViewSubItem);

                    ListViewItem.ListViewSubItem listViewSubItem1 = new ListViewItem.ListViewSubItem();
                    listViewSubItem1.Text = dataRow.Open;
                    listViewItem.SubItems.Add(listViewSubItem1);

                    ListViewItem.ListViewSubItem listViewSubItem2 = new ListViewItem.ListViewSubItem();
                    listViewSubItem2.Text = dataRow.High;
                    listViewItem.SubItems.Add(listViewSubItem2);

                    ListViewItem.ListViewSubItem listViewSubItem3 = new ListViewItem.ListViewSubItem();
                    listViewSubItem3.Text = dataRow.Low;
                    listViewItem.SubItems.Add(listViewSubItem3);

                    ListViewItem.ListViewSubItem listViewSubItem4 = new ListViewItem.ListViewSubItem();
                    listViewSubItem4.Text = dataRow.Close;
                    listViewItem.SubItems.Add(listViewSubItem4);

                    listChart.Items.Add(listViewItem);
                }
                _manager.GetSymbols(list[0].Symbol);
            });
        }
        //Reindirizza l'output del grafico con un nuovo DataTable per Stock Daily
        private void Manager_UpdateChart(object sender, ChartModel e)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                ChartArea dailyCA = chart1.ChartAreas[0];
                Series series = new Series();

                dailyCA.AxisX.MajorGrid.LineWidth = 1;
                dailyCA.AxisY.MajorGrid.LineWidth = 1;

                dailyCA.AxisY.MajorGrid.IntervalOffset = dailyCA.AxisY.Minimum;
                dailyCA.AxisY.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Number;
                dailyCA.AxisX.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;

                series.XValueMember = "Xvalue";
                series.YValueMembers = "High,Low,Open,Close";
                series.ChartType = SeriesChartType.Candlestick;
                series.CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";
                series["ShowOpenClose"] = "Both";
                chart1.DataManipulator.IsStartFromFirst = true;

                dailyCA.AxisX.ScaleView.Zoomable = true;
                dailyCA.AxisX.ScrollBar.IsPositionedInside = true;
                dailyCA.CursorX.AutoScroll = true;
                dailyCA.CursorX.IsUserSelectionEnabled = true;

                dailyCA.AxisY.ScaleView.Zoomable = true;
                dailyCA.AxisY.ScrollBar.IsPositionedInside = true;
                dailyCA.CursorY.AutoScroll = true;
                dailyCA.CursorY.IsUserSelectionEnabled = true;

                chart1.Series.Add(series);
                chart1.ChartAreas[0] = dailyCA;
                chart1.DataSource = e.Chart;
                chart1.DataBind();
            });
        }
        private void Manager_GetSymbolsDb(object sender, List<string> e)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                cmCode.DataSource = e;
            });
        }
        private void Manager_CleanData(object sender, EventArgs e)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                listChart.Items.Clear();
                chart1.Series.Clear();
            });
        }
        #endregion

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            Int32 nXvalue = 0;
            if (String.IsNullOrEmpty(cmCode.Text)) { _manager.PutMessage(" - Missing CODE value"); return; }
            if (String.IsNullOrEmpty(txtOpen.Text)) { _manager.PutMessage(" - Missing OPEN value"); return; }
            if (String.IsNullOrEmpty(txtHigh.Text)) { _manager.PutMessage(" - Missing HIGH value"); return; }
            if (String.IsNullOrEmpty(txtLow.Text)) { _manager.PutMessage(" - Missing LOW value"); return; }
            if (String.IsNullOrEmpty(txtClose.Text)) { _manager.PutMessage(" - Missing CLOSE value"); return; }
            if (String.IsNullOrEmpty(txtDate.Text))
                try
                {
                    nXvalue = Int32.Parse(listChart.Items[listChart.Items.Count - 1].SubItems[1].Text) + 1;
                }
                catch
                {
                    nXvalue = 0;
                }

            else
                nXvalue = Int32.Parse(txtDate.Text);

            DataRowModel dataRow = new DataRowModel();
            dataRow.Symbol = cmCode.Text;
            dataRow.Open = txtOpen.Text;
            dataRow.High = txtHigh.Text;
            dataRow.Low = txtLow.Text;
            dataRow.Close = txtClose.Text;
            dataRow.Xvalue = nXvalue;
            _manager.AddValue(dataRow);
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmCode.Text)) { _manager.PutMessage(" - Missing CODE value"); return; }
            DataRowModel dataRow = new DataRowModel();
            dataRow.Symbol = cmCode.Text;
            _manager.ViewChart(dataRow);
        }

        private void boxMessages_TextChanged_1(object sender, EventArgs e)
        {
            // set the current caret position to the end
            boxMessages.SelectionStart = boxMessages.Text.Length;
            // scroll it automatically
            boxMessages.ScrollToCaret();
        }

        private void listChart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataRowModel dataRow = new DataRowModel();

                ListViewItem listViewItem = listChart.SelectedItems[0];
                String id = listViewItem.Text;
                dataRow.Id = Int32.Parse(id);
                dataRow.Symbol = cmCode.Text;
                _manager.PutMessage($" + Deleting {id}");
                _manager.RemoveValue(dataRow);
            }
            catch
            {

            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 357;
        }
    }
}
