using ManagementAssistant.Code;
using ManagementAssistant.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementAssistant.Data
{
    public class Manager
    {
        private APIManager _apiManager = new APIManager();
        private DbContext _dbContext = new DbContext();
        private Processing _processing = new Processing();
        private String _symbol = String.Empty;
        private String _digitalCurrencyCode = String.Empty;
        private String _marketCode = String.Empty;
        private String _fromSymbol = String.Empty;
        private String _toSymbol = String.Empty;
        private String _interval = String.Empty;
        private String _whichMarket = String.Empty;
        private Boolean _isDaily = true;
        private Thread _threadGetDbData;
        private Thread _threadGetDailyDbData;
        private Thread _threadGetIntradyDbData;
        private Thread _threadSetMarket;
        private Thread _threadPROCESS_1;

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** EVENTI ********************************
        public event EventHandler<MessageDataModel> PutMessageEvent;
        protected virtual void OnPutMessageEvent(MessageDataModel e)
        {
            if (PutMessageEvent != null)
            {
                PutMessageEvent(this, e);
            }
        }
        public event EventHandler<MarketSymbolsDataModel> GetMarketSymbolsSuccess;
        protected virtual void OnGetMarketSymbols(MarketSymbolsDataModel e)
        {
            if (GetMarketSymbolsSuccess != null)
            {
                GetMarketSymbolsSuccess(this, e);
            }
        }
        public event EventHandler<EventArgs> RequestDaily;
        protected virtual void OnRequestDaily(EventArgs e)
        {
            if (RequestDaily != null)
            {
                RequestDaily(this, e);
            }
        }
        public event EventHandler<MarketDataModel> GetMarketFromDbSuccess;
        protected virtual void OnGetMarketFromDbSuccess(MarketDataModel e)
        {
            if (GetMarketFromDbSuccess != null)
            {
                GetMarketFromDbSuccess(this, e);
            }
        }

        #endregion
        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** GENERAL ********************************
        #region ------------------- METODI -------------------
        public void PutMessage(String message, Boolean isError = false)
        {
            MessageDataModel messageDataModel = new MessageDataModel();
            messageDataModel.message = $"{message}\n";
            messageDataModel.isError = isError;
            OnPutMessageEvent(messageDataModel);
        }
        #endregion
        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** MANAGER <-> DBCONTEXT ********************************
        #region ------------------- METODI -------------------
        #region _______ GET _______
        public void GetMarketSymbols(String market)
        {
            _whichMarket = market;
            _threadGetDbData = new Thread(ThreadGetMarketSymbols);
            _threadGetDbData.Priority = ThreadPriority.Normal;
            _threadGetDbData.Start();
        }
        public void GetStockDaily(String symbol)
        {
            _symbol = symbol;
            _isDaily = true;
            _whichMarket = "stock";
            _threadGetDailyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetDailyDbData.Priority = ThreadPriority.Normal;
            _threadGetDailyDbData.Start();
        }
        public void GetCryptoDaily(String digitalCurrencyCode, String marketCode)
        {
            _digitalCurrencyCode = digitalCurrencyCode;
            _marketCode = marketCode;
            _isDaily = true;
            _whichMarket = "crypto";
            _threadGetDailyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetDailyDbData.Priority = ThreadPriority.Normal;
            _threadGetDailyDbData.Start();
        }
        public void GetForexDaily(String fromSymbol, String toSymbol)
        {
            _fromSymbol = fromSymbol;
            _toSymbol = toSymbol;
            _isDaily = true;
            _whichMarket = "forex";
            _threadGetDailyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetDailyDbData.Priority = ThreadPriority.Normal;
            _threadGetDailyDbData.Start();
        }
        public void GetStockIntraday(String symbol, String interval)
        {
            _symbol = symbol;
            _interval = interval;
            _isDaily = false;
            _whichMarket = "stock";
            _threadGetIntradyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetIntradyDbData.Priority = ThreadPriority.Normal;
            _threadGetIntradyDbData.Start();
        }
        public void GetCryptoIntraday(String digitalCurrencyCode, String interval, String marketCode)
        {
            _digitalCurrencyCode = digitalCurrencyCode;
            _interval = interval;
            _marketCode = marketCode;
            _isDaily = false;
            _whichMarket = "crypto";
            _threadGetIntradyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetIntradyDbData.Priority = ThreadPriority.Normal;
            _threadGetIntradyDbData.Start();
        }
        public void GetForexIntraday(String fromSymbol, String toSymbol, String interval)
        {
            _fromSymbol = fromSymbol;
            _toSymbol = toSymbol;
            _interval = interval;
            _isDaily = false;
            _whichMarket = "forex";
            _threadGetIntradyDbData = new Thread(ThreadGetMarketFromDb);
            _threadGetIntradyDbData.Priority = ThreadPriority.Normal;
            _threadGetIntradyDbData.Start();
        }
        #endregion
        //---------------------------------------------------------------------------------------------
        #region _______ SET _______
        #endregion
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #region ------------------- THREAD -------------------
        private void ThreadGetMarketSymbols()
        {
            MarketSymbolsDataModel marketSymbols = new MarketSymbolsDataModel();
            marketSymbols.marketSymbols = _dbContext.GetMarketSymbols(_whichMarket);

            marketSymbols.whichMarket = _whichMarket;
            OnGetMarketSymbols(marketSymbols);
            ResetVariables();
            _threadGetDbData.Abort();
        }
        private void ThreadGetMarketFromDb()
        {
            OnRequestDaily(new EventArgs());
            MarketDataModel marketDataModel = new MarketDataModel();
            switch (_whichMarket)
            {
                case "stock":
                    if (_isDaily)
                        marketDataModel.dataTable = _dbContext.GetStockDaily(_symbol);
                    else
                        marketDataModel.dataTable = _dbContext.GetStockIntraday(_symbol, _interval);
                    //Get the current value of STOCK
                    marketDataModel.currentValue = _apiManager.GetCurrentValue(_whichMarket, _symbol);
                    if (String.IsNullOrEmpty(marketDataModel.currentValue))
                    {
                        marketDataModel.currentValue = _dbContext.GetCurrentValue(_whichMarket, _symbol);
                        PutMessage(" - - API CALL: Fail -> LAST current value. You're probably not seeing the right value.");
                    }
                    break;
                case "crypto":
                    if (_isDaily)
                        marketDataModel.dataTable = _dbContext.GetCryptoDaily(_digitalCurrencyCode, _marketCode);
                    else
                        marketDataModel.dataTable = _dbContext.GetCryptoIntraday(_digitalCurrencyCode, _interval, _marketCode);
                    //Get the current value of CRYPTO
                    marketDataModel.currentValue = _apiManager.GetCurrentValue(_whichMarket, _symbol, _digitalCurrencyCode);
                    if (String.IsNullOrEmpty(marketDataModel.currentValue))
                    {
                        marketDataModel.currentValue = _dbContext.GetCurrentValue(_whichMarket, _symbol, _digitalCurrencyCode);
                        PutMessage(" - - API CALL: Fail -> LAST current value. You're probably not seeing the right value.");
                    }
                    break;
                case "forex":
                    if (_isDaily)
                        marketDataModel.dataTable = _dbContext.GetForexDaily(_fromSymbol, _toSymbol);
                    else
                        marketDataModel.dataTable = _dbContext.GetForexIntraday(_fromSymbol, _toSymbol, _interval);
                    //Get the current value of FOREX
                    marketDataModel.currentValue = _apiManager.GetCurrentValue(_whichMarket, _symbol, _fromSymbol, _toSymbol);
                    if (String.IsNullOrEmpty(marketDataModel.currentValue))
                    {
                        marketDataModel.currentValue = _dbContext.GetCurrentValue(_whichMarket, _symbol, _fromSymbol, _toSymbol);
                        PutMessage(" - - API CALL: Fail -> LAST current value. You're probably not seeing the right value.");
                    }
                    break;
                default:
                    break;
            }
                
            //Get MAX and MIN value of datamodel
            marketDataModel.minValue = MinMax(marketDataModel.dataTable)[0];
            marketDataModel.maxValue = MinMax(marketDataModel.dataTable)[1];
            if (marketDataModel.maxValue == 0 && marketDataModel.minValue == 0)
                marketDataModel.informations = "No data available";
            else
                marketDataModel.informations = _dbContext.GetLastRefresh(_whichMarket, _isDaily, _symbol, _digitalCurrencyCode, _fromSymbol, _toSymbol);

            OnGetMarketFromDbSuccess(marketDataModel);
            ResetVariables();
            _threadGetDbData.Abort();
        }
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** MANAGER <-> APIMANAGER ********************************
        #region ------------------- METODI -------------------
        #region _______ GET _______
        #endregion
        //---------------------------------------------------------------------------------------------
        #region _______ SET _______
        public void SetMarket(String symbol = "", String digitalCurrencyCode = "", String fromToSymbol = "", String interval = "")
        {
            _symbol = symbol;
            _digitalCurrencyCode = digitalCurrencyCode;
            if (!String.IsNullOrEmpty(interval))
            {
                _isDaily = false;
                _interval = interval;
            }
            if (!String.IsNullOrEmpty(fromToSymbol))
            {
                _fromSymbol = fromToSymbol.Split('-')[0];
                _toSymbol = fromToSymbol.Split('-')[1];
                if (String.IsNullOrEmpty(_fromSymbol))
                {
                    _fromSymbol = fromToSymbol.Split('/')[0];
                    _toSymbol = fromToSymbol.Split('/')[1];
                }
                _fromSymbol = _fromSymbol.Substring(0, 3);
                _toSymbol = _toSymbol.Substring(1, 3);
            }
            _threadSetMarket = new Thread(ThreadSetMarket);
            _threadSetMarket.Priority = ThreadPriority.Normal;
            _threadSetMarket.Start();
        }
        #endregion
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #region ------------------- THREAD -------------------
        //Update or download the insert items from API to DB
        private void ThreadSetMarket()
        {
            _apiManager.GetFromAPISuccess += ApiManager_GetFromAPISuccess;
            _apiManager.GetFromAPIFailInvalidInput += ApiManager_GetFromAPIFailInvalidInput;
            _apiManager.GetFromAPIFailEndApiCall += ApiManager_GetFromAPIFailEndApiCall;
            if (!String.IsNullOrEmpty(_symbol))
            {
                if (_isDaily)
                    _apiManager.GetDailyStock(_symbol);
                else
                    _apiManager.GetIntradayStock(_symbol, _interval);
                _whichMarket = "stock";
            }
            else if (!String.IsNullOrEmpty(_digitalCurrencyCode))
            {
                if (_isDaily)
                    _apiManager.GetDailyCrypto(_digitalCurrencyCode);
                else
                    _apiManager.GetIntradayCrypto(_digitalCurrencyCode, _interval);
                _whichMarket = "crypto";
            }
            else if (!String.IsNullOrEmpty(_fromSymbol))
            {
                if (_isDaily)
                    _apiManager.GetDailyForex(_fromSymbol, _toSymbol);
                else
                    _apiManager.GetIntradayForex(_interval, _fromSymbol, _toSymbol);
                _whichMarket = "forex";
            }
            GetMarketSymbols(_whichMarket);
            ResetVariables();
            _apiManager.GetFromAPISuccess -= ApiManager_GetFromAPISuccess;
            _apiManager.GetFromAPIFailInvalidInput -= ApiManager_GetFromAPIFailInvalidInput;
            _apiManager.GetFromAPIFailEndApiCall -= ApiManager_GetFromAPIFailEndApiCall;
            _threadSetMarket.Abort();
        }
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #region ------------------- SOTTOSCRIZIONE EVENTI -------------------
        private void ApiManager_GetFromAPISuccess(object sender, EventArgs e)
        {
            PutMessage(" - - API CALL: Success -> download and upload");
        }
        private void ApiManager_GetFromAPIFailInvalidInput(object sender, ErrorAPIDataModel e)
        {
            PutMessage(" - - API CALL: Fail -> Invalid INPUT");
        }
        private void ApiManager_GetFromAPIFailEndApiCall(object sender, ErrorAPIDataModel e)
        {
            PutMessage(" - - API CALL: Fail -> End API call");
        }
        #endregion
        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ******************************** MANAGER <-> PROCESS ********************************
        #region ------------------- METODI -------------------
        public void TryProcess()
        {
            _threadPROCESS_1 = new Thread(ThreadTryProcess);
            _threadPROCESS_1.Priority = ThreadPriority.Normal;
            _threadPROCESS_1.Start();
        }
        #region _______ GET _______
        #endregion
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #region ------------------- THREAD -------------------
        private void ThreadTryProcess()
        {
            List<String[]> candlesticks = _dbContext.GetCandlesticks("stock", "DW TREND", "");
            _processing.isTrendUp(candlesticks);
            
        }
        #endregion
        //---------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------
        #region ------------------- SOTTOSCRIZIONE EVENTI -------------------

        #endregion
        #endregion

        /*********************************************************************************************
         ---------------------------------------------------------------------------------------------
        *********************************************************************************************/

        #region ------------------- UTILITY -------------------
        private float[] MinMax(DataTable dataTable)
        {
            float[] minMax = new float[] { 0f, 0f };
            if (dataTable.Rows.Count == 0)
                return minMax;
            List<float> highValues = new List<float>();
            List<float> lowValues = new List<float>();
            DataColumn highCol = dataTable.Columns[1];
            DataColumn lowCol = dataTable.Columns[2];

            foreach (DataRow row in dataTable.Rows)
            {
                String highValue = row.Field<String>(highCol).Replace('.', ',');
                String lowValue = row.Field<String>(lowCol).Replace('.', ',');
                highValues.Add(float.Parse(highValue));
                lowValues.Add(float.Parse(lowValue));
            }

            minMax[0] = lowValues.Min();
            minMax[1] = highValues.Max();

            return minMax;
        }
        private void ResetVariables()
        {
            _symbol = String.Empty;
            _digitalCurrencyCode = String.Empty;
            _marketCode = String.Empty;
            _fromSymbol = String.Empty;
            _toSymbol = String.Empty;
            _interval = String.Empty;
            _whichMarket = String.Empty;
            _isDaily = true;
        }
        #endregion
    }
}
