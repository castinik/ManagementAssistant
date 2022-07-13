using ManagementAssistant.Model;
using ManagementAssistant.Model.MetaData;
using ManagementAssistant.Model.TimeDataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Data
{
    public class DbContext
    {
        private String _connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region *********************** API MANAGER <-> DB ***********************
        #region ############## UPDATE DB ##############
        public void UpdateCurrentValue(String market, String currentValue, String symbol, String from_currency, String to_currency) 
        {
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            String sql = UpdateCurrentValue(market);
            try
            {
                switch (market)
                {
                    case "stock":
                        sql += $"\'{currentValue}\' WHERE Symbol = \'{symbol}\'";
                        break;
                    case "crypto":
                        sql += $"\'{currentValue}\' WHERE DigitalCurrencyCode = \'{from_currency}\'";
                        break;
                    case "forex":
                        sql += $"\'{currentValue}\' WHERE FromSymbol = \'{from_currency}\'" +
                            $" AND ToSymbol = \'{to_currency}\'";
                        break;
                    default:
                        break;
                }
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                cnn.Dispose();
            }
            catch
            {
                currentValue = String.Empty;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ############## METADATA ##############
        #region ##### CHECK METADATA #####
        public Int32 CheckExistStocks(StockMetaDataModel stockMetaData)
        {
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].[_Stocks] WHERE Symbol = \'{stockMetaData.Symbol}\'";
            try
            {
                Int32 Id = 0;
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                        Id = reader.GetInt32(0);
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return Id;
            }
            catch
            {
                return 0;
            }
        }
        public Int32 CheckExistCrypto(CryptoMetaDataModel cryptoMetaData)
        {
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].[_Cryptocurrencies] WHERE DigitalCurrencyName = \'{cryptoMetaData.DigitalCurrencyName}\'";
            try
            {
                Int32 Id = 0;
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                        Id = reader.GetInt32(0);
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return Id;
            }
            catch
            {
                return 0;
            }
        }
        public Int32 CheckExistForex(ForexMetaDataModel forexMetaData)
        {
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].[_Forex] WHERE FromSymbol = \'{forexMetaData.FromSymbol}\' AND" +
                $" ToSymbol = \'{forexMetaData.ToSymbol}\'";
            try
            {
                Int32 Id = 0;
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                        Id = reader.GetInt32(0);
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return Id;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region ##### UPLOAD METADATA ##### (Creazione di nuovi dati in meta tabella)
        public Int32 UploadMetaStock(StockMetaDataModel stockMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            Int32 Id = 0;
            try
            {
                String sql = $"INSERT INTO [dbo].[_Stocks]" +
                    $" ( Symbol, Information, {lastRefshed}, OutputZone, TimeZone, CurrentValue)" +
                    $" VALUES (\'{stockMetaData.Symbol}\', \'{stockMetaData.Information}\', \'{stockMetaData.LastRefreshed}\'," +
                    $" \'{stockMetaData.OutputZone}\', \'{stockMetaData.TimeZone}\', \'{stockMetaData.CurrentValue}\')";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                sql = $"SELECT * FROM [dbo].[_Stocks] WHERE Symbol = \'{stockMetaData.Symbol}\'";
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
                cnn.Close();
                cmd.Dispose();
                return Id;
            }
            catch
            {
                return Id;
            }
        }
        public Int32 UploadMetaCrypto(CryptoMetaDataModel cryptoMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            Int32 Id = 0;
            try
            {
                String sql = $"INSERT INTO [dbo].[_Cryptocurrencies]" +
                    $" ( DigitalCurrencyCode, DigitalCurrencyName, MarketCode, MarketName, TimeZone, {lastRefshed})" +
                    $" VALUES (\'{cryptoMetaData.DigitalCurrencyCode}\', \'{cryptoMetaData.DigitalCurrencyName}\'," +
                    $" \'{cryptoMetaData.MarketCode}\', \'{cryptoMetaData.MarketName}\'," +
                    $" \'{cryptoMetaData.TimeZone}\', \'{cryptoMetaData.LastRefreshed}\')";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                sql = $"SELECT * FROM [dbo].[_Cryptocurrencies] WHERE DigitalCurrencyName = \'{cryptoMetaData.DigitalCurrencyName}\'" +
                    $" AND MarketName = \'{cryptoMetaData.MarketName}\'";
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
                cnn.Close();
                cmd.Dispose();
                return Id;
            }
            catch
            {
                return Id;
            }
        }
        public Int32 UploadMetaForex(ForexMetaDataModel forexMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            Int32 Id = 0;
            try
            {
                String sql = $"INSERT INTO [dbo].[_Forex]" +
                    $" ( FromSymbol, ToSymbol, OutputSize, TimeZone, {lastRefshed})" +
                    $" VALUES (\'{forexMetaData.FromSymbol}\', \'{forexMetaData.ToSymbol}\'," +
                    $" \'{forexMetaData.OutputSize}\', \'{forexMetaData.TimeZone}\', \'{forexMetaData.LastRefreshed}\')";

                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                sql = $"SELECT * FROM [dbo].[_Forex] WHERE FromSymbol = \'{forexMetaData.FromSymbol}\'" +
                    $" AND ToSymbol = \'{forexMetaData.ToSymbol}\'";
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
                cnn.Close();
                cmd.Dispose();
                return Id;
            }
            catch
            {
                return Id;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region ##### UPDATE METADATA ##### (Aggiornamento dei nuovi dati in meta tabella)
        public void UpdateMetaStock(StockMetaDataModel stockMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            try
            {
                String sql = $"UPDATE [dbo].[_Stocks] SET {lastRefshed} = \'{stockMetaData.LastRefreshed}\'," +
                    $" Information = \'{stockMetaData.Information}\', CurrentValue = \'{stockMetaData.CurrentValue}\'" +
                    $" WHERE Id = {stockMetaData.Id}";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                cmd.Dispose();
            }
            catch
            {

            }
        }
        public void UpdateMetaCrypto(CryptoMetaDataModel cryptoMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            try
            {
                String sql = $"UPDATE [dbo].[_Cryptocurrencies] SET {lastRefshed} = \'{cryptoMetaData.LastRefreshed}\'" +
                    $" WHERE Id = {cryptoMetaData.Id}";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                cmd.Dispose();
            }
            catch
            {

            }
        }
        public void UpdateMetaForex(ForexMetaDataModel forexMetaData, Boolean isDaily)
        {
            String lastRefshed = SetDaily(isDaily)[0];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            try
            {
                String sql = $"UPDATE [dbo].[_Forex] SET {lastRefshed} = \'{forexMetaData.LastRefreshed}\'" +
                    $" WHERE Id = {forexMetaData.Id}";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                cmd.Dispose();
            }
            catch
            {

            }
        }
        #endregion
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ############## TIMEDATA ##############
        #region ##### CHECK TIMEDATA #####
        public StockModel CheckExistTimeDataStock(StockModel stockModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[1];
            StockModel stock = new StockModel();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].{tableName} WHERE Id_Stock = \'{stockModel.StockMetaDataModel.Id}\'";
            if (!isDaily)
                sql += $" AND Interval = \'{stockModel.StockMetaDataModel.Interval}\'";
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        StockTimeDataModel stockTimeData = new StockTimeDataModel();
                        stockTimeData.Open = reader.GetString(2);
                        stockTimeData.High = reader.GetString(3);
                        stockTimeData.Low = reader.GetString(4);
                        stockTimeData.Close = reader.GetString(5);
                        stockTimeData.Volume = reader.GetString(6);
                        stockTimeData.DateTime = reader.GetDateTime(7);
                        stock.StockTimeDataModels.Add(stockTimeData);
                    }
                }
                if (!isDaily)
                    stock.StockMetaDataModel.Interval = stockModel.StockMetaDataModel.Interval;
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return stock;
            }
            catch
            {
                return stock;
            }
        }
        public CryptoModel CheckExistTimeDataCrypto(CryptoModel cryptoModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[2];
            CryptoModel crypto = new CryptoModel();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].{tableName} WHERE Id_Crypto = \'{cryptoModel.CryptoMetaDataModel.Id}\'";
            if (!isDaily)
                sql += $" AND Interval = \'{cryptoModel.CryptoMetaDataModel.Interval}\'";
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        CryptoTimeDataModel cryptoTimeData = new CryptoTimeDataModel();
                        cryptoTimeData.Open = reader.GetString(2);
                        cryptoTimeData.High = reader.GetString(3);
                        cryptoTimeData.Low = reader.GetString(4);
                        cryptoTimeData.Close = reader.GetString(5);
                        cryptoTimeData.Volume = reader.GetString(6);
                        cryptoTimeData.MarketCap = reader.GetString(7);
                        cryptoTimeData.DateTime = reader.GetDateTime(8);
                        crypto.CryptoTimeDataModels.Add(cryptoTimeData);
                    }
                }
                if (!isDaily)
                    crypto.CryptoMetaDataModel.Interval = cryptoModel.CryptoMetaDataModel.Interval;
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return crypto;
            }
            catch
            {
                return crypto;
            }
        }
        public ForexModel CheckExistTimeDataForex(ForexModel forexModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[3];
            ForexModel forex = new ForexModel();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = $"SELECT * FROM [dbo].{tableName} WHERE Id_Forex = \'{forexModel.ForexMetaDataModel.Id}\'";
            if (!isDaily)
                sql += $" AND Interval = \'{forexModel.ForexMetaDataModel.Interval}\'";
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        ForexTimeDataModel forexTimeData = new ForexTimeDataModel();
                        forexTimeData.Open = reader.GetString(2);
                        forexTimeData.High = reader.GetString(3);
                        forexTimeData.Low = reader.GetString(4);
                        forexTimeData.Close = reader.GetString(5);
                        forexTimeData.DateTime = reader.GetDateTime(6);
                        forex.ForexTimeDataModels.Add(forexTimeData);
                    }
                }
                if (!isDaily)
                    forex.ForexMetaDataModel.Interval = forexModel.ForexMetaDataModel.Interval;
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return forex;
            }
            catch
            {
                return forex;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------

        #region ##### UPLOAD TIMEDATA ##### (Creazione o aggiunta di nuovi dati in timeData tabella)
        public void UploadTimeDataStock(StockModel stockModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[1];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            String sql = String.Empty;
            try
            {
                cnn.Open();
                foreach (StockTimeDataModel stockTimeData in stockModel.StockTimeDataModels)
                {
                    if (isDaily)
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Stock, [Open], High, Low, [Close], Volume, Date) VALUES" +
                            $" ({stockModel.StockMetaDataModel.Id}, \'{stockTimeData.Open}\', \'{stockTimeData.High}\'," +
                            $" \'{stockTimeData.Low}\', \'{stockTimeData.Close}\', \'{stockTimeData.Volume}\', \'{stockTimeData.DateTime}\')";
                    }
                    else
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Stock, [Open], High, Low, [Close], Volume, Date, Interval) VALUES" +
                            $" ({stockModel.StockMetaDataModel.Id}, \'{stockTimeData.Open}\', \'{stockTimeData.High}\'," +
                            $" \'{stockTimeData.Low}\', \'{stockTimeData.Close}\', \'{stockTimeData.Volume}\'," +
                            $" \'{stockTimeData.DateTime}\', \'{stockModel.StockMetaDataModel.Interval}\')";
                    }
                    cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                cnn.Close();
            }
            catch
            {

            }
        }
        public void UploadTimeDataCrypto(CryptoModel cryptoModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[2];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            String sql = String.Empty;
            try
            {
                cnn.Open();
                foreach (CryptoTimeDataModel cryptoTimeData in cryptoModel.CryptoTimeDataModels)
                {
                    if (isDaily)
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Crypto, [Open], High, Low, [Close], Volume, MarketCap, Date) VALUES" +
                            $" ({cryptoModel.CryptoMetaDataModel.Id}, \'{cryptoTimeData.Open}\', \'{cryptoTimeData.High}\'," +
                            $" \'{cryptoTimeData.Low}\', \'{cryptoTimeData.Close}\', \'{cryptoTimeData.Volume}\'," +
                            $" \'{cryptoTimeData.MarketCap}\', \'{cryptoTimeData.DateTime}\')";
                    }
                    else
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Crypto, [Open], High, Low, [Close], Volume, MarketCap, Date, Interval) VALUES" +
                            $" ({cryptoModel.CryptoMetaDataModel.Id}, \'{cryptoTimeData.Open}\', \'{cryptoTimeData.High}\'," +
                            $" \'{cryptoTimeData.Low}\', \'{cryptoTimeData.Close}\', \'{cryptoTimeData.Volume}\'," +
                            $" \'{cryptoTimeData.MarketCap}\', \'{cryptoTimeData.DateTime}\', \'{cryptoModel.CryptoMetaDataModel.Interval}\')";
                    }
                    cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                cnn.Close();
            }
            catch
            {

            }
        }
        public void UploadTimeDataForex(ForexModel forexModel, Boolean isDaily)
        {
            String tableName = SetDaily(isDaily)[3];
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            String sql = String.Empty;
            try
            {
                cnn.Open();
                foreach (ForexTimeDataModel forexTimeData in forexModel.ForexTimeDataModels)
                {
                    if (isDaily)
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Forex, [Open], High, Low, [Close], Date) VALUES" +
                            $" ({forexModel.ForexMetaDataModel.Id}, \'{forexTimeData.Open}\', \'{forexTimeData.High}\'," +
                            $" \'{forexTimeData.Low}\', \'{forexTimeData.Close}\', \'{forexTimeData.DateTime}\')";
                    }
                    else
                    {
                        sql = $"INSERT INTO [dbo].{tableName}" +
                            $" (Id_Forex, [Open], High, Low, [Close], Date, Interval) VALUES" +
                            $" ({forexModel.ForexMetaDataModel.Id}, \'{forexTimeData.Open}\', \'{forexTimeData.High}\'," +
                            $" \'{forexTimeData.Low}\', \'{forexTimeData.Close}\', \'{forexTimeData.DateTime}\', \'{forexModel.ForexMetaDataModel.Interval}\')";
                    }
                    cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                cnn.Close();
            }
            catch
            {

            }
        }
        #endregion
        #endregion
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region *********************** MANAGER <-> DB ***********************
        #region ############## GET DATA ##############
        public Dictionary<String, String> GetMarketSymbols(String market)
        {
            Dictionary<String, String> marketInfo = new Dictionary<String, String>();

            String sql = SetMarketSymbol(market);
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        String symbol = String.Empty;
                        if(market == "forex")
                        {
                            symbol = $"{reader.GetString(0)} - {reader.GetString(1)}";
                            marketInfo.Add(symbol, "");
                        }
                        else
                        {
                            String name = String.Empty;
                            try
                            {
                                name = reader.GetString(1);
                            }
                            catch
                            {
                                name = "No name";
                            }
                            marketInfo.Add(reader.GetString(0), name);
                        }
                    }
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return marketInfo;
            }
            catch
            {
                return marketInfo;
            }
        }
        public String GetLastRefresh(String market, Boolean isDaily, String symbol="", String digitalCurrecyCode="", String fromSymbol="", String toSymbol="")
        {
            String dateTime = String.Empty;
            String sql = SetMarketLastRefresh(market, isDaily, symbol, digitalCurrecyCode, fromSymbol, toSymbol);
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        dateTime = reader.GetString(0);
                    }
                }
                reader.Close();
                cmd.Dispose();
                cnn.Close();
                return dateTime;
            }
            catch
            {
                return dateTime;
            }
        }
        public String GetCurrentValue(String market, String symbol = "", String from_currency = "", String to_currency = "")
        {
            String currentValue = String.Empty;
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            String sql = String.Empty;
            try
            {
                switch (market)
                {
                    case "stock":
                        sql = $"SELECT CurrentValue FROM [dbo].[_Stocks] WHERE Symbol = \'{symbol}\'";
                        break;
                    case "crypto":
                        sql = $"SELECT CurrentValue FROM [dbo].[_Cryptocurrencies] WHERE DigitalCurrencyCode = \'{from_currency}\'";
                        break;
                    case "forex":
                        sql = $"SELECT CurrentValue FROM [dbo].[_Forex] WHERE FromSymbol = \'{from_currency}\'" +
                            $" AND ToSymbol = \'{to_currency}\'";
                        break;
                    default:
                        break;
                }
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        currentValue = reader.GetString(0);
                    }
                }
            }
            catch
            {
                currentValue = String.Empty;
            }
            finally
            {
                if (String.IsNullOrEmpty(currentValue))
                    currentValue = "No data avilable";
            }
            return currentValue;
        }


        public DataTable GetStockDaily(String symbol)
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataStocksDaily] WHERE Id_Stock = (" +
                $"SELECT Id FROM [dbo].[_Stocks] WHERE Symbol = \'{symbol}\')";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public DataTable GetStockIntraday(String symbol, String interval)
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataStocksIntraday] WHERE Id_Stock = (" +
                $"SELECT Id FROM [dbo].[_Stocks] WHERE Symbol = \'{symbol}\')" +
                $" AND Interval = \'{interval}min\'";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        
        public DataTable GetCryptoDaily(String digitalCurrencyCode, String marketCode = "USD")
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataCryptoDaily] WHERE Id_Crypto = (" +
                $"SELECT Id FROM [dbo].[_Cryptocurrencies]" +
                $" WHERE DigitalCurrencyCode = \'{digitalCurrencyCode}\' AND MarketCode = \'{marketCode}\')";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public DataTable GetCryptoIntraday(String digitalCurrencyCode, String interval, String marketCode = "USD")
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataCryptoIntraday] WHERE Id_Crypto = (" +
                $"SELECT Id FROM [dbo].[_Cryptocurrencies]" +
                $" WHERE DigitalCurrencyCode = \'{digitalCurrencyCode}\' AND MarketCode = \'{marketCode}\')" +
                $" AND Interval = \'{interval}min\'";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        
        public DataTable GetForexDaily(String fromSymbol="EUR", String toSymbol="USD")
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataForexDaily] WHERE Id_Forex = (" +
                $"SELECT Id FROM [dbo].[_Forex]" +
                $" WHERE FromSymbol = \'{fromSymbol}\' AND ToSymbol = \'{toSymbol}\')";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public DataTable GetForexIntraday(String fromSymbol, String toSymbol, String interval)
        {
            String sql = $"SELECT [Open], [High], [Low], [Close], [Date]" +
                $" FROM [dbo].[DataForexDaily] WHERE Id_Forex = (" +
                $"SELECT Id FROM [dbo].[_Forex]" +
                $" WHERE FromSymbol = \'{fromSymbol}\' AND ToSymbol = \'{toSymbol}\')" +
                $" AND Interval = \'{interval}min\'";
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ############## SET DATA ##############

        #endregion
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region *********************** PROCESSING <-> DB ***********************
        #region ############## GET DATA ##############
        public List<String[]> GetCandlesticks(String market, String nameItem, String toItem = "", String interval = "", Boolean isDaily = true)
        {
            List<String[]> candelsticks = new List<String[]>();
            String sql = SqlGetCandles(market, nameItem, toItem, interval, isDaily);
            SqlDataReader reader;
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            try
            { 
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String[] candle = new string[4];
                    candle[0] = reader.GetString(0);
                    candle[1] = reader.GetString(1);
                    candle[2] = reader.GetString(2);
                    candle[3] = reader.GetString(3);
                    //Replace of "." with ","
                    for (Int32 i = 0; i < 4; i++)
                    {
                        candle[i] = candle[i].Replace('.', ',');
                    }
                    candelsticks.Add(candle);
                }
                cnn.Close();
                cmd.Dispose();
                
                return candelsticks;
            }
            catch
            {
                return candelsticks;
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ############## SET DATA ##############

        #endregion
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### UTILITY #####
        private String[] SetDaily(Boolean isDaily)
        {
            String[] output = new string[4];
            if (isDaily)
            {
                output[0] = "LastRefreshedDaily";
                output[1] = "[DataStocksDaily]";
                output[2] = "[DataCryptoDaily]";
                output[3] = "[DataForexDaily]";
            }
            else
            {
                output[0] = "LastRefreshedIntraday";
                output[1] = "[DataStocksIntraday]";
                output[2] = "[DataCryptoIntraday]";
                output[3] = "[DataForexIntraday]";
            }
            return output;
        }
        private String SetMarketSymbol(String market)
        {
            String marketSql = String.Empty;
            switch (market)
            {
                case "stock":
                    marketSql = "SELECT [Symbol], [Information] FROM [dbo].[_Stocks]";
                    break;
                case "crypto":
                    marketSql = "SELECT [DigitalCurrencyCode], [DigitalCurrencyName] FROM [dbo].[_Cryptocurrencies]";
                    break;
                case "forex":
                    marketSql = "SELECT [FromSymbol], [ToSymbol] FROM [dbo].[_Forex]";
                    break;
                default:
                    break;
            }
            return marketSql;
        }
        private String UpdateCurrentValue(String market)
        {
            String marketSql = String.Empty;
            switch (market)
            {
                case "stock":
                    marketSql = "UPDATE [dbo].[_Stocks] SET CurrentValue = ";
                    break;
                case "crypto":
                    marketSql = "UPDATE [dbo].[_Cryptocurrencies] SET CurrentValue = ";
                    break;
                case "forex":
                    marketSql = "UPDATE [dbo].[_Forex] SET CurrentValue = ";
                    break;
                default:
                    break;
            }
            return marketSql;
        }
        private String SetMarketLastRefresh(String market, Boolean isDaily, String symbol = "", String digitalCurrecyCode = "", String fromSymbol = "", String toSymbol = "")
        {
            String marketSql = String.Empty;
            String lastRefresh = SetDaily(isDaily)[0];
            switch (market)
            {
                case "stock":
                    marketSql = $"SELECT {lastRefresh} FROM [dbo].[_Stocks] WHERE Symbol = \'{symbol}\'";
                    break;
                case "crypto":
                    marketSql = $"SELECT {lastRefresh} FROM [dbo].[_Cryptocurrencies] WHERE DigitalCurrencyCode = \'{digitalCurrecyCode}\'";
                    break;
                case "forex":
                    marketSql = $"SELECT {lastRefresh} FROM [dbo].[_Forex] WHERE FromSymbol = \'{fromSymbol}\' AND ToSymbol = \'{toSymbol}\'";
                    break;
                default:
                    break;
            }
            return marketSql;
        }

        #region ----- PROCESSING -----
        private String SqlGetCandles(String market, String nameItem, String toItem = "", String interval = "", Boolean isDaily = true)
        {
            String marketType = String.Empty;
            String intervalType = String.Empty;
            String sql = String.Empty;
            String sqlInterval = String.Empty;
            if (isDaily)
                intervalType = "Daily";
            else
            {
                intervalType = "Intraday";
                sqlInterval = $" AND Interval = \'{interval}min\'";
            }

            switch (market)
            {
                case "stock":
                    marketType = "Stocks";
                    sql = $" WHERE Id_Stock = (SELECT Id FROM [dbo].[_Stocks] WHERE Symbol =" +
                        $" \'{nameItem}\'){sqlInterval}";
                    break;
                case "crypto":
                    marketType = "Crypto";
                    sql = $" WHERE Id_Crypto = (SELECT Id FROM [dbo].[_Cryptocurrencies] WHERE DigitalCurrencyCode =" +
                        $" \'{nameItem}\'){sqlInterval}";
                    break;
                case "forex":
                    marketType = "Forex";
                    sql = $" WHERE Id_Forex = (SELECT Id FROM [dbo].[_Forex] WHERE FromSymbol =" +
                        $" \'{nameItem}\' AND ToSymbol = \'{toItem}\'){sqlInterval}";
                    break;
                default:
                    break;
            }
            return $"SELECT [Open],[High],[Low],[Close] FROM [dbo].[Data{marketType}{intervalType}] {sql}";
        }
        #endregion
        #endregion
    }
}
