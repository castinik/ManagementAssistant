using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ManagementAssistant.Model;
using ManagementAssistant.Model.TimeDataModel;
using ManagementAssistant.Model.MetaData;
using System.Globalization;

namespace ManagementAssistant.API
{
    public class APIMarkets
    {
        private String _key = "NGWM1W7QHEOS86L3";

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### STOCKS #####

        public StockModel GetStockDaily(String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            StockModel stockModel = new StockModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, String>> json_timeSeries = new Dictionary<String, Dictionary<String, String>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            stockModel.StockMetaDataModel = new StockMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            stockModel.StockMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. Symbol", out outValue);
                            stockModel.StockMetaDataModel.Symbol = outValue;
                            json_metaData.TryGetValue("3. Last Refreshed", out outValue);
                            stockModel.StockMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("4. Output Size", out outValue);
                            stockModel.StockMetaDataModel.OutputZone = outValue;
                            json_metaData.TryGetValue("5. Time Zone", out outValue);
                            stockModel.StockMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(item.Value);
                        foreach(Dictionary<String, String> timeSeriesItem in json_timeSeries.Values)
                        {
                            StockTimeDataModel stockTimeDataModel = new StockTimeDataModel();
                            String outValue = String.Empty;
                            timeSeriesItem.TryGetValue("1. open", out outValue);
                            stockTimeDataModel.Open = outValue;
                            timeSeriesItem.TryGetValue("2. high", out outValue);
                            stockTimeDataModel.High = outValue;
                            timeSeriesItem.TryGetValue("3. low", out outValue);
                            stockTimeDataModel.Low = outValue;
                            timeSeriesItem.TryGetValue("4. close", out outValue);
                            stockTimeDataModel.Close = outValue;
                            timeSeriesItem.TryGetValue("5. volume", out outValue);
                            stockTimeDataModel.Volume = outValue;
                            stockModel.StockTimeDataModels.Add(stockTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach(KeyValuePair<String, Dictionary<String, String>> timeSeries in json_timeSeries)
                        {
                            stockModel.StockTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return stockModel;
        }
        public StockModel GetStockIntraday(String interval, String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}min&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            StockModel stockModel = new StockModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, String>> json_timeSeries = new Dictionary<String, Dictionary<String, String>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            stockModel.StockMetaDataModel = new StockMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            stockModel.StockMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. Symbol", out outValue);
                            stockModel.StockMetaDataModel.Symbol = outValue;
                            json_metaData.TryGetValue("3. Last Refreshed", out outValue);
                            stockModel.StockMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("4. Interval", out outValue);
                            stockModel.StockMetaDataModel.Interval = outValue;
                            json_metaData.TryGetValue("5. Output Size", out outValue);
                            stockModel.StockMetaDataModel.OutputZone = outValue;
                            json_metaData.TryGetValue("6. Time Zone", out outValue);
                            stockModel.StockMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(item.Value);
                        foreach (Dictionary<String, String> timeSeriesItem in json_timeSeries.Values)
                        {
                            StockTimeDataModel stockTimeDataModel = new StockTimeDataModel();
                            String outValue = String.Empty;
                            timeSeriesItem.TryGetValue("1. open", out outValue);
                            stockTimeDataModel.Open = outValue;
                            timeSeriesItem.TryGetValue("2. high", out outValue);
                            stockTimeDataModel.High = outValue;
                            timeSeriesItem.TryGetValue("3. low", out outValue);
                            stockTimeDataModel.Low = outValue;
                            timeSeriesItem.TryGetValue("4. close", out outValue);
                            stockTimeDataModel.Close = outValue;
                            timeSeriesItem.TryGetValue("5. volume", out outValue);
                            stockTimeDataModel.Volume = outValue;
                            stockModel.StockTimeDataModels.Add(stockTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach (KeyValuePair<String, Dictionary<String, String>> timeSeries in json_timeSeries)
                        {
                            stockModel.StockTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return stockModel;
        }
        public String GetStockName(String symbol)
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={symbol}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            String stockName = String.Empty;
            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);
                Dictionary<String, List<Dictionary<String, String>>> json_data = JsonSerializer.Deserialize<Dictionary<String, List<Dictionary<String, String>>>>(downloaded);
                Dictionary<String, String> last_data = json_data["bestMatches"][0];
                foreach (KeyValuePair<String, String> kvp in last_data)
                {
                    if (kvp.Key == "2. name")
                    {
                        stockName = kvp.Value;
                        client.Dispose();
                        return stockName;
                    }
                }
                client.Dispose();
            }
            return stockName;
        }
        public String GetStockCurrentValue(String symbol)
        {
            String currentValue = String.Empty;
            string QUERY_URL = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);
                Dictionary<String, Dictionary<String, String>> json_data = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(downloaded);
                currentValue = json_data["Global Quote"]["05. price"];
                client.Dispose();
            }
            return currentValue;
        }
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### CRYPTO #####

        public CryptoModel GetCryptoDaily(String symbol, String market = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&market={market}&symbol={symbol}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            CryptoModel cryptoModel = new CryptoModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, String>> json_timeSeries = new Dictionary<String, Dictionary<String, String>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            cryptoModel.CryptoMetaDataModel = new CryptoMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            cryptoModel.CryptoMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. Digital Currency Code", out outValue);
                            cryptoModel.CryptoMetaDataModel.DigitalCurrencyCode = outValue;
                            json_metaData.TryGetValue("3. Digital Currency Name", out outValue);
                            cryptoModel.CryptoMetaDataModel.DigitalCurrencyName = outValue;
                            json_metaData.TryGetValue("4. Market Code", out outValue);
                            cryptoModel.CryptoMetaDataModel.MarketCode = outValue;
                            json_metaData.TryGetValue("5. Market Name", out outValue);
                            cryptoModel.CryptoMetaDataModel.MarketName = outValue;
                            json_metaData.TryGetValue("6. Last Refreshed", out outValue);
                            cryptoModel.CryptoMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("7. Time Zone", out outValue);
                            cryptoModel.CryptoMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(item.Value);
                        foreach (Dictionary<String, String> timeSeriesItem in json_timeSeries.Values)
                        {
                            CryptoTimeDataModel cryptoTimeDataModel = new CryptoTimeDataModel();
                            String outValue = String.Empty;
                            timeSeriesItem.TryGetValue($"1a. open ({market})", out outValue);
                            cryptoTimeDataModel.Open = outValue;
                            timeSeriesItem.TryGetValue($"2a. high ({market})", out outValue);
                            cryptoTimeDataModel.High = outValue;
                            timeSeriesItem.TryGetValue($"3a. low ({market})", out outValue);
                            cryptoTimeDataModel.Low = outValue;
                            timeSeriesItem.TryGetValue($"4a. close ({market})", out outValue);
                            cryptoTimeDataModel.Close = outValue;
                            timeSeriesItem.TryGetValue("5. volume", out outValue);
                            cryptoTimeDataModel.Volume = outValue;
                            timeSeriesItem.TryGetValue("6. market cap (USD)", out outValue);
                            cryptoTimeDataModel.MarketCap = outValue;
                            cryptoModel.CryptoTimeDataModels.Add(cryptoTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach (KeyValuePair<String, Dictionary<String, String>> timeSeries in json_timeSeries)
                        {
                            cryptoModel.CryptoTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return cryptoModel;
        }
        public CryptoModel GetCryptoIntraday(String interval, String symbol, String market = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=CRYPTO_INTRADAY&symbol={symbol}&market={market}&interval={interval}min&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            CryptoModel cryptoModel = new CryptoModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, object>> json_timeSeries = new Dictionary<String, Dictionary<String, object>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            cryptoModel.CryptoMetaDataModel = new CryptoMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            cryptoModel.CryptoMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. Digital Currency Code", out outValue);
                            cryptoModel.CryptoMetaDataModel.DigitalCurrencyCode = outValue;
                            json_metaData.TryGetValue("3. Digital Currency Name", out outValue);
                            cryptoModel.CryptoMetaDataModel.DigitalCurrencyName = outValue;
                            json_metaData.TryGetValue("4. Market Code", out outValue);
                            cryptoModel.CryptoMetaDataModel.MarketCode = outValue;
                            json_metaData.TryGetValue("5. Market Name", out outValue);
                            cryptoModel.CryptoMetaDataModel.MarketName = outValue;
                            json_metaData.TryGetValue("6. Last Refreshed", out outValue);
                            cryptoModel.CryptoMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("7. Interval", out outValue);
                            cryptoModel.CryptoMetaDataModel.Interval = outValue;
                            json_metaData.TryGetValue("9. Time Zone", out outValue);
                            cryptoModel.CryptoMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(item.Value);

                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, object>>>(item.Value);
                        foreach (Dictionary<String, object> timeSeriesItem in json_timeSeries.Values)
                        {
                            CryptoTimeDataModel cryptoTimeDataModel = new CryptoTimeDataModel();
                            object outValue = String.Empty;
                            timeSeriesItem.TryGetValue("1. open", out outValue);
                            cryptoTimeDataModel.Open = outValue.ToString();
                            timeSeriesItem.TryGetValue("2. high", out outValue);
                            cryptoTimeDataModel.High = outValue.ToString();
                            timeSeriesItem.TryGetValue("3. low", out outValue);
                            cryptoTimeDataModel.Low = outValue.ToString();
                            timeSeriesItem.TryGetValue("4. close", out outValue);
                            cryptoTimeDataModel.Close = outValue.ToString();
                            timeSeriesItem.TryGetValue("5. volume", out outValue);
                            cryptoTimeDataModel.Volume = outValue.ToString();
                            cryptoModel.CryptoTimeDataModels.Add(cryptoTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach (KeyValuePair<String, Dictionary<String, object>> timeSeries in json_timeSeries)
                        {
                            cryptoModel.CryptoTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return cryptoModel;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ##### FOREX #####

        public ForexModel GetForexDaily(String from_symb = "EUR", String to_symb = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={from_symb}&to_symbol={to_symb}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            ForexModel forexModel = new ForexModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, String>> json_timeSeries = new Dictionary<String, Dictionary<String, String>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            forexModel.ForexMetaDataModel = new ForexMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            forexModel.ForexMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. From Symbol", out outValue);
                            forexModel.ForexMetaDataModel.FromSymbol = outValue;
                            json_metaData.TryGetValue("3. To Symbol", out outValue);
                            forexModel.ForexMetaDataModel.ToSymbol = outValue;
                            json_metaData.TryGetValue("4. Output Size", out outValue);
                            forexModel.ForexMetaDataModel.OutputSize = outValue;
                            json_metaData.TryGetValue("5. Last Refreshed", out outValue);
                            forexModel.ForexMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("6. Time Zone", out outValue);
                            forexModel.ForexMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(item.Value);
                        foreach (Dictionary<String, String> timeSeriesItem in json_timeSeries.Values)
                        {
                            ForexTimeDataModel forexTimeDataModel = new ForexTimeDataModel();
                            String outValue = String.Empty;
                            timeSeriesItem.TryGetValue("1. open", out outValue);
                            forexTimeDataModel.Open = outValue;
                            timeSeriesItem.TryGetValue("2. high", out outValue);
                            forexTimeDataModel.High = outValue;
                            timeSeriesItem.TryGetValue("3. low", out outValue);
                            forexTimeDataModel.Low = outValue;
                            timeSeriesItem.TryGetValue("4. close", out outValue);
                            forexTimeDataModel.Close = outValue;
                            forexModel.ForexTimeDataModels.Add(forexTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach (KeyValuePair<String, Dictionary<String, String>> timeSeries in json_timeSeries)
                        {
                            forexModel.ForexTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return forexModel;
        }
        public ForexModel GetForexIntraday(String interval, String from_symb = "EUR", String to_symb = "USD")
        {
            string QUERY_URL = $"https://www.alphavantage.co/query?function=FX_INTRADAY&from_symbol={from_symb}&to_symbol={to_symb}&interval={interval}min&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            ForexModel forexModel = new ForexModel();

            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);

                Dictionary<String, JsonElement> json_data = JsonSerializer.Deserialize<Dictionary<String, JsonElement>>(downloaded);
                Dictionary<String, String> json_metaData = new Dictionary<String, String>();
                Dictionary<String, Dictionary<String, String>> json_timeSeries = new Dictionary<String, Dictionary<String, String>>();

                //ciclo su i DUE dizionari con valori in JSON
                foreach (KeyValuePair<String, JsonElement> item in json_data)
                {
                    //controllo lo stato della chiamata API
                    CheckApiCall(item.Key);
                    //seleziono il dizionario per l'oggeto MetaDataModel
                    if (item.Key == "Meta Data")
                    {
                        json_metaData = JsonSerializer.Deserialize<Dictionary<String, String>>(item.Value);
                        foreach (KeyValuePair<String, String> itemMetaData in json_metaData)
                        {
                            forexModel.ForexMetaDataModel = new ForexMetaDataModel();
                            String outValue = String.Empty;
                            json_metaData.TryGetValue("1. Information", out outValue);
                            forexModel.ForexMetaDataModel.Information = outValue;
                            json_metaData.TryGetValue("2. From Symbol", out outValue);
                            forexModel.ForexMetaDataModel.FromSymbol = outValue;
                            json_metaData.TryGetValue("3. To Symbol", out outValue);
                            forexModel.ForexMetaDataModel.ToSymbol = outValue;
                            json_metaData.TryGetValue("4. Last Refreshed", out outValue);
                            forexModel.ForexMetaDataModel.LastRefreshed = outValue;
                            json_metaData.TryGetValue("5. Interval", out outValue);
                            forexModel.ForexMetaDataModel.Interval = outValue;
                            json_metaData.TryGetValue("6. Output Size", out outValue);
                            forexModel.ForexMetaDataModel.OutputSize = outValue;
                            json_metaData.TryGetValue("7. Time Zone", out outValue);
                            forexModel.ForexMetaDataModel.TimeZone = outValue;
                        }
                    }
                    else
                    {
                        json_timeSeries = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(item.Value);
                        foreach (Dictionary<String, String> timeSeriesItem in json_timeSeries.Values)
                        {
                            ForexTimeDataModel forexTimeDataModel = new ForexTimeDataModel();
                            String outValue = String.Empty;
                            timeSeriesItem.TryGetValue("1. open", out outValue);
                            forexTimeDataModel.Open = outValue;
                            timeSeriesItem.TryGetValue("2. high", out outValue);
                            forexTimeDataModel.High = outValue;
                            timeSeriesItem.TryGetValue("3. low", out outValue);
                            forexTimeDataModel.Low = outValue;
                            timeSeriesItem.TryGetValue("4. close", out outValue);
                            forexTimeDataModel.Close = outValue;
                            forexModel.ForexTimeDataModels.Add(forexTimeDataModel);
                        }
                        Int32 listTimeDataCounter = 0;
                        foreach (KeyValuePair<String, Dictionary<String, String>> timeSeries in json_timeSeries)
                        {
                            forexModel.ForexTimeDataModels[listTimeDataCounter].DateTime = DateTime.ParseExact(timeSeries.Key, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            listTimeDataCounter += 1;
                        }
                    }
                }
                client.Dispose();
            }
            return forexModel;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        public String GetCryptoForexCurrentValue(String from_currency, String to_currency = "USD")
        {
            String currentValue = String.Empty;
            string QUERY_URL = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={from_currency}&to_currency={to_currency}&apikey={_key}";
            Uri queryUri = new Uri(QUERY_URL);
            using (WebClient client = new WebClient())
            {
                String downloaded = client.DownloadString(queryUri);
                Dictionary<String, Dictionary<String, String>> json_data = JsonSerializer.Deserialize<Dictionary<String, Dictionary<String, String>>>(downloaded);
                currentValue = json_data["Realtime Currency Exchange Rate"]["5. Exchange Rate"];
                client.Dispose();
            }
            return currentValue;
        }
        private void CheckApiCall(String key)
        {
            switch (key)
            {
                case "Note":
                    throw new Exception("EndApiCall");
                case "Error Message":
                    throw new ArgumentException("InvalidInput");
                default:
                    break;
            }
        }
    }
}
