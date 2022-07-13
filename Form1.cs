using ManagementAssistant.API;
using ManagementAssistant.Data;
using ManagementAssistant.Data.DataModel;
using ManagementAssistant.Model;
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

namespace ManagementAssistant
{
    public partial class Form1 : Form
    {
        private delegate void AutoDelegate();
        private APIManager _apiManager = new APIManager();
        private Manager _manager = new Manager();
        private DbContext _dbContext = new DbContext();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _manager.GetMarketFromDbSuccess += Manager_GetMarketFromDbSuccess;
            _manager.GetMarketSymbolsSuccess += Manager_GetMarketStmbolsSuccess;
            _manager.PutMessageEvent += Manager_PutMessageEvent;
        }

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** EVENTI PER IL FORM ********************************
        private void Manager_PutMessageEvent(object sender, MessageDataModel e)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                boxMessages.ForeColor = e.isError ? Color.Red : Color.Black;
                boxMessages.Text += e.message;
            });
        }
        //Reindirizza l'output per le liste di scelta
        private void Manager_GetMarketStmbolsSuccess(object sender, MarketSymbolsDataModel marketSymbols)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                ListView listView = new ListView();
                switch (marketSymbols.whichMarket)
                {
                    case "stock":
                        listView = listViewStocks;
                        break;
                    case "crypto":
                        listView = listViewCrypto;
                        break;
                    case "forex":
                        listView = listViewForex;
                        break;
                    default:
                        break;
                }
                listView.Items.Clear();
                foreach(KeyValuePair<String, String> keyValue in marketSymbols.marketSymbols.OrderBy(x => x.Key))
                {
                    ListViewItem listViewItem = new ListViewItem(keyValue.Key);

                    ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem();
                    listViewSubItem.Text = keyValue.Value;
                    listViewItem.SubItems.Add(listViewSubItem);

                    listView.Items.Add(listViewItem);
                }
            });
        }
        //Reindirizza l'output del grafico con un nuovo DataTable per Stock Daily
        private void Manager_GetMarketFromDbSuccess(object sender, MarketDataModel dt)
        {
            this.BeginInvoke((AutoDelegate)delegate ()
            {
                ChartArea dailyCA = chart1.ChartAreas[0];
                Series series = chart1.Series["Daily"];

                dailyCA.AxisX.MajorGrid.LineWidth = 1;
                dailyCA.AxisY.MajorGrid.LineWidth = 1;

                Double minMax = Convert.ToDouble(dt.maxValue - dt.minValue);
                Double adjust = 10;
                Double factor = ((minMax * adjust) / 100);

                dailyCA.AxisY.Minimum = Math.Round(dt.minValue - factor, 3);
                dailyCA.AxisY.Maximum = Math.Round(dt.maxValue + factor, 3);
                dailyCA.AxisY.MajorGrid.Interval = factor;

                dailyCA.AxisY.MajorGrid.IntervalOffset = dailyCA.AxisY.Minimum;
                dailyCA.AxisY.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Number;
                dailyCA.AxisX.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;

                series.XValueMember = "Date";
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

                chart1.ChartAreas[0] = dailyCA;
                chart1.DataSource = dt.dataTable;
                chart1.DataBind();

                lblLastRefresh.Text = dt.informations;
                lblMinValue.Text = dt.minValue.ToString();
                lblMaxValue.Text = dt.maxValue.ToString();
                lblCurrentValue.Text = dt.currentValue.ToString();
            });
        }
        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** FORM ********************************

        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** EVENTI DA FORM ********************************
        #region ------------------ LISTVIEW ------------------
        private void listViewStocks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = listViewStocks.SelectedItems[0];
            String symbol = listViewItem.Text;
            _manager.PutMessage($" - - Getting view of: {symbol}");
            if (ckBoxIntraday.Checked)
            {
                if(cmBoxIntraday.SelectedItem == null)
                {
                    _manager.PutMessage(" - - Select a time frame.", true);
                    return;
                }
                String interval = cmBoxIntraday.SelectedItem.ToString();
                interval = interval.Remove(interval.Length - 7);
                _manager.GetStockIntraday(symbol, interval);
            }
            else
                _manager.GetStockDaily(symbol);
        }
        private void listViewStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            txBxSearchStock.Text = listViewStocks.FocusedItem.Text;
        }
        private void listViewCrypto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = listViewCrypto.SelectedItems[0];
            String digitalCurrencyCode = listViewItem.Text;
            _manager.PutMessage($" - - Getting view of: {digitalCurrencyCode}");
            if (ckBoxIntraday.Checked)
            {
                if (cmBoxIntraday.SelectedItem == null)
                {
                    _manager.PutMessage(" - - Select a time frame.", true);
                    return;
                }
                String interval = cmBoxIntraday.SelectedItem.ToString();
                interval = interval.Remove(interval.Length - 7);
                _manager.GetCryptoIntraday(digitalCurrencyCode, interval, "USD");
            }
            else
            {
                _manager.GetCryptoDaily(listViewItem.Text, "USD");
            }
        }
        private void listViewCrypto_SelectedIndexChanged(object sender, EventArgs e)
        {
            txBxSearchCrypto.Text = listViewCrypto.FocusedItem.Text;
        }
        private void listViewForex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = listViewForex.SelectedItems[0];
            String fromSymbol = listViewItem.Text.Split('-')[0].Remove(3, 1);
            String toSymbol = listViewItem.Text.Split('-')[1].Remove(0, 1);
            _manager.PutMessage($" - - Getting view of: {fromSymbol}-{toSymbol}");
            if (ckBoxIntraday.Checked)
            {
                if (cmBoxIntraday.SelectedItem == null)
                {
                    _manager.PutMessage(" - - Select a time frame.", true);
                    return;
                }
                String interval = cmBoxIntraday.SelectedItem.ToString();
                interval = interval.Remove(interval.Length - 7);
                _manager.GetForexIntraday(fromSymbol, toSymbol, interval);
            }
            _manager.GetForexDaily(fromSymbol, toSymbol);
        }
        private void listViewForex_SelectedIndexChanged(object sender, EventArgs e)
        {
            txBxSearchForex.Text = listViewForex.FocusedItem.Text;
        }
        private void tabPageStock_Enter(object sender, EventArgs e)
        {
            _manager.GetMarketSymbols("stock");
        }
        private void tabPageCrypto_Enter(object sender, EventArgs e)
        {
            _manager.GetMarketSymbols("crypto");
        }
        private void tabPageForex_Enter(object sender, EventArgs e)
        {
            _manager.GetMarketSymbols("forex");
        }
        #endregion
        #region ------------------ CONTROLS ------------------
        private void ckBoxIntraday_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBoxIntraday.Checked)
                cmBoxIntraday.Enabled = true;
            else
                cmBoxIntraday.Enabled = false;
        }
        private void btnAddMarket_Click(object sender, EventArgs e)
        {
            String inputToCheck = txBxSearchStock.Text + txBxSearchCrypto.Text + txBxSearchForex.Text;
            if (String.IsNullOrEmpty(inputToCheck))
            {
                _manager.PutMessage(" - - Insert something to search.", true);
                return;
            }
            String interval = String.Empty;
            if (cmBoxTimeFrame.SelectedItem != null)
            {
                interval = cmBoxTimeFrame.SelectedItem.ToString();
                interval = interval.Remove(interval.Length - 7);
            }  
            _manager.SetMarket(txBxSearchStock.Text,txBxSearchCrypto.Text,txBxSearchForex.Text,interval);
        }
        private void txBxSearchMarket_TextChanged(object sender, EventArgs e)
        {
            txBxSearchCrypto.Text = "";
            txBxSearchForex.Text = "";
        }
        private void txBxSearchCrypto_TextChanged(object sender, EventArgs e)
        {
            txBxSearchStock.Text = "";
            txBxSearchForex.Text = "";
        }
        private void txBxSearchForex_TextChanged(object sender, EventArgs e)
        {
            txBxSearchStock.Text = "";
            txBxSearchCrypto.Text = "";
        }
        #endregion

        private void boxMessages_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            boxMessages.SelectionStart = boxMessages.Text.Length;
            // scroll it automatically
            boxMessages.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _manager.TryProcess();
        }

        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/
    }
}
