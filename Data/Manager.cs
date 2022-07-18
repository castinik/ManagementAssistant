using ManagementAssistantTester.Data.DataModel;
using ManagementAssistantTester.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementAssistantTester.Data
{
    public class Manager
    {
        private DbContext _dbContext = new DbContext();
        private DataRowModel _dataRow;
        private String _symbol = String.Empty;
        private Thread _threadAddValue;
        private Thread _threadViewChart;
        private Thread _threadRemoveValue;
        private Thread _threadGetSymbols;

        public event EventHandler<MessageDataModel> Message;
        protected virtual void OnMessage(MessageDataModel e)
        {
            if (Message != null)
            {
                Message(this, e);
            }
        }
        public event EventHandler<ChartModel> UpdateChart;
        protected virtual void OnUpdateChart(ChartModel e)
        {
            if (UpdateChart != null)
            {
                UpdateChart(this, e);
            }
        }
        public event EventHandler<List<DataRowModel>> UpdateList;
        protected virtual void OnUpdateList(List<DataRowModel> e)
        {
            if (UpdateList != null)
            {
                UpdateList(this, e);
            }
        }
        public event EventHandler<List<String>> GetSymbolsDb;
        protected virtual void OnGetSymbolsDb(List<String> e)
        {
            if (GetSymbolsDb != null)
            {
                GetSymbolsDb(this, e);
            }
        }
        public event EventHandler<EventArgs> CleanData;
        protected virtual void OnCleanData(EventArgs e)
        {
            if (CleanData != null)
            {
                CleanData(this, e);
            }
        }


        #region ########################## PUBLICS METHODS ##########################

        public void PutMessage(String mex, Boolean isError = false)
        {
            MessageDataModel messageDataModel = new MessageDataModel();
            messageDataModel.Message = mex;
            messageDataModel.IsError = isError;
            OnMessage(messageDataModel);
        }
        public void AddValue(DataRowModel dataRow)
        {
            _dataRow = dataRow;
            _threadAddValue = new Thread(ThreadAddValue);
            _threadAddValue.Priority = ThreadPriority.Normal;
            _threadAddValue.Start();
        }
        public void RemoveValue(DataRowModel dataRow)
        {
            _dataRow = dataRow;
            _threadRemoveValue = new Thread(ThreadRemoveValue);
            _threadRemoveValue.Priority = ThreadPriority.Normal;
            _threadRemoveValue.Start();
        }
        public void ViewChart(DataRowModel dataRow)
        {
            _dataRow = dataRow;
            _threadViewChart = new Thread(ThreadViewChart);
            _threadViewChart.Priority = ThreadPriority.Normal;
            _threadViewChart.Start();
        }
        public void GetSymbols(String symbol = "")
        {
            _symbol = symbol;
            _threadGetSymbols = new Thread(ThreadGetSymbols);
            _threadGetSymbols.Priority = ThreadPriority.Normal;
            _threadGetSymbols.Start();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------

        #region ########################## THREAD ##########################
        private void ThreadAddValue()
        {
            _dbContext.RowValueExist += DbContext_RowValueExist;
            _dbContext.AddRowValueSuccess += DbContext_AddRowValueSuccess;
            _dbContext.AddRowValueFail += DbContext_AddRowValueFail;
            if (_dbContext.AddRowValue(_dataRow))
            {
                ChartModel chart = new ChartModel();
                chart.Chart = _dbContext.GetDataTable(_dataRow.Symbol);
                List<DataRowModel> list = _dbContext.GetRawsList(_dataRow.Symbol);
                if (chart.Chart.Rows.Count > 0)
                {
                    OnUpdateChart(chart);
                    OnUpdateList(list);
                }
            }
            _dbContext.RowValueExist -= DbContext_RowValueExist;
            _dbContext.AddRowValueSuccess -= DbContext_AddRowValueSuccess;
            _dbContext.AddRowValueFail -= DbContext_AddRowValueFail;
            _threadAddValue.Abort();
        }
        private void ThreadRemoveValue()
        {
            MessageDataModel message = new MessageDataModel();
            if (_dbContext.RemoveRowValue(_dataRow))
            {
                ChartModel chart = new ChartModel();
                chart.Chart = _dbContext.GetDataTable(_dataRow.Symbol);
                List<DataRowModel> list = _dbContext.GetRawsList(_dataRow.Symbol);
                message.Message = " + Raw values removed";
                if(list.Count > 0)
                {
                    OnUpdateChart(chart);
                    OnUpdateList(list);
                }
                else
                {
                    OnCleanData(new EventArgs());
                }
                OnMessage(message);
            }
            else
            {
                message.Message = " - Raw values not removed";
                OnMessage(message);
            }
            _threadRemoveValue.Abort();
        }
        private void ThreadViewChart()
        {
            _dbContext.RowValueExist += DbContext_RowValueExist;
            _dbContext.AddRowValueSuccess += DbContext_AddRowValueSuccess;
            _dbContext.AddRowValueFail += DbContext_AddRowValueFail;
            _dbContext.ViewChartFail += DbContext_ViewChartFail;

            ChartModel chart = new ChartModel();
            chart.Chart = _dbContext.GetDataTable(_dataRow.Symbol);
            List<DataRowModel> list = _dbContext.GetRawsList(_dataRow.Symbol);
            if (chart.Chart.Rows.Count > 0)
            {
                OnUpdateChart(chart);
                OnUpdateList(list);
            }

            _dbContext.RowValueExist -= DbContext_RowValueExist;
            _dbContext.AddRowValueSuccess -= DbContext_AddRowValueSuccess;
            _dbContext.AddRowValueFail -= DbContext_AddRowValueFail;
            _dbContext.ViewChartFail -= DbContext_ViewChartFail;
            _threadViewChart.Abort();
        }
        private void ThreadGetSymbols()
        {
            List<String> symbols = new List<string>();
            List<String> newSymbols = new List<string>();
            symbols = _dbContext.GetSymbols();
            if (!String.IsNullOrEmpty(_symbol))
            {
                newSymbols.Add(_symbol);
                symbols.Remove(_symbol);
                foreach (String symbol in symbols)
                {
                    newSymbols.Add(symbol);
                }
            }
            else
                newSymbols.AddRange(symbols);
            OnGetSymbolsDb(newSymbols);
            _threadGetSymbols.Abort();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------

        #region ########################## EVENTS FROM DBCONTEXT ##########################
        private void DbContext_RowValueExist(object sender, EventArgs e)
        {
            MessageDataModel messageData = new MessageDataModel();
            messageData.Message = " - Row value already exist";
            OnMessage(messageData);
        }
        private void DbContext_AddRowValueSuccess(object sender, EventArgs e)
        {
            MessageDataModel messageData = new MessageDataModel();
            messageData.Message = " + Row value added with SUCCESS";
            OnMessage(messageData);
        }
        private void DbContext_AddRowValueFail(object sender, EventArgs e)
        {
            MessageDataModel messageData = new MessageDataModel();
            messageData.Message = " - FAIL to add row value";
            OnMessage(messageData);
        }
        private void DbContext_ViewChartFail(object sender, EventArgs e)
        {
            MessageDataModel messageData = new MessageDataModel();
            messageData.Message = " - No existing data for inserted CODE";
            OnMessage(messageData);
        }
        #endregion
    }
}
