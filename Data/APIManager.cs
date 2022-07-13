using ManagementAssistantCharger.API;
using ManagementAssistantCharger.Data.DataModel;
using ManagementAssistantCharger.Model;
using ManagementAssistantCharger.Model.TimeDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementAssistantCharger.Data
{
    public class APIManager
    {
        APIMarkets _apiMarkets = new APIMarkets();
        DbContext _dbContext = new DbContext();

        #region ##### EVENTI #####
        public event EventHandler<EventArgs> GetFromAPISuccess;
        protected virtual void OnGetFromAPISuccess(EventArgs e)
        {
            if (GetFromAPISuccess != null)
            {
                GetFromAPISuccess(this, e);
            }
        }
        public event EventHandler<ErrorAPIDataModel> GetFromAPIFailEndApiCall;
        protected virtual void OnGetFromAPIFailEndApiCall(ErrorAPIDataModel e)
        {
            if (GetFromAPIFailEndApiCall != null)
            {
                GetFromAPIFailEndApiCall(this, e);
            }
        }
        public event EventHandler<ErrorAPIDataModel> GetFromAPIFailInvalidInput;
        protected virtual void OnGetFromAPIFailInvalidInput(ErrorAPIDataModel e)
        {
            if (GetFromAPIFailInvalidInput != null)
            {
                GetFromAPIFailInvalidInput(this, e);
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### PUBLIC CALL METHOD #####
        public void GetDailyStock(String symbol)
        {
            try
            {
                StockModel stockModel = new StockModel();
                stockModel = _apiMarkets.GetStockDaily(symbol);
                stockModel.StockMetaDataModel.Information = _apiMarkets.GetStockName(symbol);
                OnGetFromAPISuccess(new EventArgs());
                SetStock(stockModel);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "stock";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "stock";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        public void GetIntradayStock(String symbol, String interval = "60")
        {
            try
            {
                StockModel stockModel = new StockModel();
                stockModel = _apiMarkets.GetStockIntraday(interval, symbol);
                stockModel.StockMetaDataModel.Information = _apiMarkets.GetStockName(symbol);
                OnGetFromAPISuccess(new EventArgs());
                SetStock(stockModel, false);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "stock";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "stock";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        public void GetDailyCrypto(String symbol, String market = "USD")
        {
            try
            {
                CryptoModel cryptoModel = new CryptoModel();
                cryptoModel = _apiMarkets.GetCryptoDaily(symbol, market);
                OnGetFromAPISuccess(new EventArgs());
                SetCrypto(cryptoModel);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "crypto";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "crypto";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        public void GetIntradayCrypto(String symbol, String interval = "60", String market = "USD")
        {
            try
            {
                CryptoModel cryptoModel = new CryptoModel();
                cryptoModel = _apiMarkets.GetCryptoIntraday(interval, symbol, market);
                OnGetFromAPISuccess(new EventArgs());
                SetCrypto(cryptoModel, false);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "crypto";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "crypto";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        public void GetDailyForex(String from_symb = "EUR", String to_symb = "USD")
        {
            try
            {
                ForexModel forexModel = new ForexModel();
                forexModel = _apiMarkets.GetForexDaily(from_symb, to_symb);
                OnGetFromAPISuccess(new EventArgs());
                SetForex(forexModel);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "forex";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "forex";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        public void GetIntradayForex(String interval = "60", String from_symb = "EUR", String to_symb = "USD")
        {
            try
            {
                ForexModel forexModel = new ForexModel();
                forexModel = _apiMarkets.GetForexIntraday(interval, from_symb, to_symb);
                OnGetFromAPISuccess(new EventArgs());
                SetForex(forexModel, false);
            }
            catch (ArgumentException ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "forex";
                OnGetFromAPIFailInvalidInput(errorAPI);
            }
            catch (Exception ex)
            {
                ErrorAPIDataModel errorAPI = new ErrorAPIDataModel();
                errorAPI.message = ex.Message;
                errorAPI.marketType = "forex";
                OnGetFromAPIFailEndApiCall(errorAPI);
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### PRIVATE SET METHOD FOR DBCONTEXT #####
        private void SetStock(StockModel stockModel, Boolean isDaily = true)
        {
            Int32 IdStock = _dbContext.CheckExistStocks(stockModel.StockMetaDataModel);
            if (IdStock == 0)
            {
                IdStock = _dbContext.UploadMetaStock(stockModel.StockMetaDataModel, isDaily);
                stockModel.StockMetaDataModel.Id = IdStock;
            }
            else
            {
                stockModel.StockMetaDataModel.Id = IdStock;
                _dbContext.UpdateMetaStock(stockModel.StockMetaDataModel, isDaily);
            }
            StockModel newStockModel = _dbContext.CheckExistTimeDataStock(stockModel, isDaily);
            if (newStockModel.StockTimeDataModels.Count > 0)
            {
                List<StockTimeDataModel> oldTimeData = new List<StockTimeDataModel>(stockModel.StockTimeDataModels);
                foreach (StockTimeDataModel newTimeData in newStockModel.StockTimeDataModels)
                {
                    foreach (StockTimeDataModel timeData in stockModel.StockTimeDataModels)
                    {
                        if (StocksAreEquals(timeData, newTimeData))
                            oldTimeData.Remove(timeData);
                    }
                }
                stockModel.StockTimeDataModels = oldTimeData;
            }
            _dbContext.UploadTimeDataStock(stockModel, isDaily);
        }
        private void SetCrypto(CryptoModel cryptoModel, Boolean isDaily = true)
        {
            Int32 IdCrypto = _dbContext.CheckExistCrypto(cryptoModel.CryptoMetaDataModel);
            if (IdCrypto == 0)
            {
                IdCrypto = _dbContext.UploadMetaCrypto(cryptoModel.CryptoMetaDataModel, isDaily);
                cryptoModel.CryptoMetaDataModel.Id = IdCrypto;
            }
            else
            {
                cryptoModel.CryptoMetaDataModel.Id = IdCrypto;
                _dbContext.UpdateMetaCrypto(cryptoModel.CryptoMetaDataModel, isDaily);
            }
            CryptoModel newCryptoModel = _dbContext.CheckExistTimeDataCrypto(cryptoModel, isDaily);
            if (newCryptoModel.CryptoTimeDataModels.Count > 0)
            {
                List<CryptoTimeDataModel> oldTimeData = new List<CryptoTimeDataModel>(cryptoModel.CryptoTimeDataModels);
                foreach (CryptoTimeDataModel newTimeData in newCryptoModel.CryptoTimeDataModels)
                {
                    foreach (CryptoTimeDataModel timeData in cryptoModel.CryptoTimeDataModels)
                    {
                        if (CryptoAreEquals(timeData, newTimeData))
                            oldTimeData.Remove(timeData);
                    }
                }
                cryptoModel.CryptoTimeDataModels = oldTimeData;
            }
            _dbContext.UploadTimeDataCrypto(cryptoModel, isDaily);
        }
        private void SetForex(ForexModel forexModel, Boolean isDaily = true)
        {
            Int32 IdForex = _dbContext.CheckExistForex(forexModel.ForexMetaDataModel);
            if (IdForex == 0)
            {
                IdForex = _dbContext.UploadMetaForex(forexModel.ForexMetaDataModel, isDaily);
                forexModel.ForexMetaDataModel.Id = IdForex;
            }
            else
            {
                forexModel.ForexMetaDataModel.Id = IdForex;
                _dbContext.UpdateMetaForex(forexModel.ForexMetaDataModel, isDaily);
            }
            ForexModel newForexModel = _dbContext.CheckExistTimeDataForex(forexModel, isDaily);
            if (newForexModel.ForexTimeDataModels.Count > 0)
            {
                List<ForexTimeDataModel> oldTimeData = new List<ForexTimeDataModel>(forexModel.ForexTimeDataModels);
                foreach (ForexTimeDataModel newTimeData in newForexModel.ForexTimeDataModels)
                {
                    foreach (ForexTimeDataModel timeData in forexModel.ForexTimeDataModels)
                    {
                        if (ForexAreEquals(timeData, newTimeData))
                            oldTimeData.Remove(timeData);
                    }
                }
                forexModel.ForexTimeDataModels = oldTimeData;
            }
            _dbContext.UploadTimeDataForex(forexModel, isDaily);
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        private Boolean StocksAreEquals(StockTimeDataModel A, StockTimeDataModel B)
        {
            if (A.Open != B.Open) return false;
            if (A.Close != B.Close) return false;
            if (A.High != B.High) return false;
            if (A.Low != B.Low) return false;
            if (A.Volume != B.Volume) return false;

            return true;
        }
        private Boolean CryptoAreEquals(CryptoTimeDataModel A, CryptoTimeDataModel B)
        {
            if (A.DateTime == B.DateTime) return true;
            if (A.Open != B.Open) return false;
            if (A.Close != B.Close) return false;
            if (A.High != B.High) return false;
            if (A.Low != B.Low) return false;
            if (A.Volume != B.Volume) return false;
            if (A.MarketCap != B.MarketCap) return false;

            return true;
        }
        private Boolean ForexAreEquals(ForexTimeDataModel A, ForexTimeDataModel B)
        {
            if (A.DateTime == B.DateTime) return true;
            if (A.Open != B.Open) return false;
            if (A.Close != B.Close) return false;
            if (A.High != B.High) return false;
            if (A.Low != B.Low) return false;

            return true;
        }
    }
}
