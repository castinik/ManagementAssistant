using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementAssistantCharger.Data
{
    public class Download
    {
        private APIManager _apiManager = new APIManager();
        private DbContext _dbContext = new DbContext();
        //file surgent
        private String _fileSurgent = String.Empty;
        private String _tempFileSurgent = String.Empty;
        private String _fileDownloaded = String.Empty;

        private String _type = String.Empty;
        private String _interval = String.Empty;

        private Int32 _attempt = 0;

        private Boolean _isDownloading = true;
        private List<String> _itemsSurgent = new List<String>();
        private List<String> _newItemsSurgent = new List<String>();
        private List<String> _itemsDownloaded = new List<String>();

        private FileWorker _readerSurgent;
        private FileWorker _readerDownloaded;

        public Download(String fileToDownload, String type, Boolean fromDB = false)
        {
            _fileSurgent = fileToDownload;
            _type = type;
            SetEnvironment();
            if (fromDB)
            {
                UploadFromDBDaily();
            }
            else
            {
                DownloadDaily();
            }
        }

        public Download(String fileToDownload, String type, Int32 interval, Boolean fromDB=false)
        {
            _fileSurgent = fileToDownload;
            _type = type;
            switch (interval)
            {
                case 0: _interval = "1"; break;
                case 1: _interval = "5"; break;
                case 2: _interval = "15"; break;
                case 3: _interval = "30"; break;
                case 4: _interval = "60"; break;
                default: _interval = "60"; break;
            }
            SetEnvironment(false);
            if (fromDB)
            {
                UploadFromDBIntraday();
            }
            else
            {
                DownloadIntraday();
            }
        }

        //----------------------------------------------------------------------------------------
        //****************************************************************************************
        //----------------------------------------------------------------------------------------

        #region ************************** EVENTS FROM APIMANAGER **************************

        private void SetEnvironment(Boolean isDaily = true)
        {
            if(isDaily)
                _fileDownloaded = $"{_fileSurgent.Split('.').First()}Downloaded.txt";
            else
                _fileDownloaded = $"{_fileSurgent.Split('.').First()}DownloadedIntraday.txt";
            _readerSurgent = new FileWorker(_fileSurgent);
            _readerDownloaded = new FileWorker(_fileDownloaded);
            _apiManager.GetFromAPISuccess += ApiManager_GetFromAPISuccess;
            _apiManager.GetFromAPIFailInvalidInput += ApiManager_GetFromAPIFailInvalidInput;
            _apiManager.GetFromAPIFailEndApiCall += ApiManager_GetFromAPIFailEndApiCall;
        }

        private void ApiManager_GetFromAPIFailEndApiCall(object sender, DataModel.ErrorAPIDataModel e)
        {
            Console.Beep();
            Console.WriteLine(" - API call error: End API call.\n # Waiting 10 seconds.");
            _attempt += 1;
            if(_attempt == 9)
            {
                Console.Beep();
                Console.WriteLine(" - API call error: End daily API call.");
                Console.ReadKey();
                Console.WriteLine(" # Exiting. . .");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            Thread.Sleep(10000);
        }

        private void ApiManager_GetFromAPIFailInvalidInput(object sender, DataModel.ErrorAPIDataModel e)
        {
            Console.Beep();
            Console.WriteLine(" - API call error: Invalid input.\n");
            _isDownloading = false;
        }

        private void ApiManager_GetFromAPISuccess(object sender, EventArgs e)
        {
            Console.WriteLine(" + Item's market downloaded.\n");
            _attempt = 0;
            _isDownloading = false;
        }
        #endregion

        //----------------------------------------------------------------------------------------
        //****************************************************************************************
        //----------------------------------------------------------------------------------------

        #region ************************** METHODS **************************
        private void UploadFromDBIntraday()
        {
            List<String> symbols = new List<string>();
            symbols = _dbContext.GetMarketSymbols(_type);

            foreach (String symbol in symbols)
            {
                Console.WriteLine($" # Try to update {symbol}");
                while (_isDownloading)
                {
                    switch (_type)
                    {
                        case "stock":
                            _apiManager.GetIntradayStock(symbol, _interval);
                            break;
                        case "crypto":
                            _apiManager.GetIntradayCrypto(symbol, _interval);
                            break;
                        case "forex":
                            String from = symbol.Split('-').First().ToUpper().Substring(0, 3);
                            String to = symbol.Split('-').Last().ToUpper().Substring(1, 3);
                            _apiManager.GetIntradayForex(_interval, from, to);
                            break;
                        default:
                            Console.Beep();
                            Console.WriteLine(" - Make sure that surgent file's name is valid. (Only: stocks.txt, crypto.txt, forex.txt)\n");
                            Console.ReadKey();
                            return;
                    }
                    if (_isDownloading)
                        Console.WriteLine($" # Retry to download {symbol}. Attempt[{_attempt}/8]");
                }
                _isDownloading = true;
            }
            Console.WriteLine(" + Downloading DONE.");
        }

        private void UploadFromDBDaily()
        {
            List<String> symbols = new List<string>();
            symbols = _dbContext.GetMarketSymbols(_type);

            foreach(String symbol in symbols)
            {
                Console.WriteLine($" # Try to update {symbol}");
                while (_isDownloading)
                {
                    switch (_type)
                    {
                        case "stock":
                            _apiManager.GetDailyStock(symbol);
                            break;
                        case "crypto":
                            _apiManager.GetDailyCrypto(symbol);
                            break;
                        case "forex":
                            String from = symbol.Split('-').First().ToUpper().Substring(0, 3);
                            String to = symbol.Split('-').Last().ToUpper().Substring(1, 3);
                            _apiManager.GetDailyForex(from, to);
                            break;
                        default:
                            Console.Beep();
                            Console.WriteLine(" - Make sure that surgent file's name is valid. (Only: stocks.txt, crypto.txt, forex.txt)\n");
                            Console.ReadKey();
                            return;
                    }
                    if (_isDownloading)
                        Console.WriteLine($" # Retry to download {symbol}. Attempt[{_attempt}/8]");
                }
                _isDownloading = true;
            }
            Console.WriteLine(" + Downloading DONE.");
        }

        private void DownloadIntraday()
        {
            //Save items of file "_fileSurgent" in a list (_itemsSurgent)
            _itemsSurgent.AddRange(_readerSurgent.ReadFile());
            _newItemsSurgent.AddRange(_readerSurgent.ReadFile());

            //Save items yet downloaded saved in "_fileDownloaded" in a list (_itemsDownloaded)
            _itemsDownloaded.AddRange(_readerDownloaded.ReadFile());

            if (_itemsSurgent.Count == 0)
            {
                Console.WriteLine(" - No items to download");
                return;
            }

            foreach (String item in _itemsSurgent)
            {
                Console.WriteLine($" # Try to download {item}");
                if (_itemsDownloaded.Contains(item))
                {
                    Console.WriteLine($" - {item} yet downloaded.");
                    UploadFile(item, true);
                    continue;
                }
                while (_isDownloading)
                {
                    switch (_type)
                    {
                        case "stocks":
                            _apiManager.GetIntradayStock(item, _interval);
                            break;
                        case "crypto":
                            _apiManager.GetIntradayCrypto(item, _interval);
                            break;
                        case "forex":
                            String from = item.Split('-').First().ToUpper();
                            String to = item.Split('-').Last().ToUpper();
                            _apiManager.GetIntradayForex(_interval, from, to);
                            break;
                        default:
                            Console.Beep();
                            Console.WriteLine(" - Make sure that surgent file's name is valid. (Only: stocks.txt, crypto.txt, forex.txt)\n");
                            Console.ReadKey();
                            return;
                    }
                    if (_isDownloading)
                        Console.WriteLine($" # Retry to download {item}. Attempt[{_attempt}/8]");
                }
                UploadFile(item);
                _isDownloading = true;
            }
            Console.WriteLine(" + Downloading DONE.");
        }

        private void DownloadDaily()
        {
            //Save items of file "_fileSurgent" in a list (_itemsSurgent)
            _itemsSurgent.AddRange(_readerSurgent.ReadFile());
            _newItemsSurgent.AddRange(_readerSurgent.ReadFile());

            //Save items yet downloaded saved in "_fileDownloaded" in a list (_itemsDownloaded)
            _itemsDownloaded.AddRange(_readerDownloaded.ReadFile());

            if (_itemsSurgent.Count == 0)
            {
                Console.WriteLine(" - No items to download");
                return;
            }

            foreach (String item in _itemsSurgent)
            {
                Console.WriteLine($" # Try to download {item}");
                if (_itemsDownloaded.Contains(item))
                {
                    Console.WriteLine($" - {item} yet downloaded.");
                    UploadFile(item, true);
                    continue;
                }
                while (_isDownloading)
                {
                    switch (_type)
                    {
                        case "stocks":
                            _apiManager.GetDailyStock(item);
                            break;
                        case "crypto":
                            _apiManager.GetDailyCrypto(item);
                            break;
                        case "forex":
                            _apiManager.GetDailyForex(item.Split('-').First().ToUpper(), item.Split('-').Last().ToUpper());
                            break;
                        default:
                            Console.Beep();
                            Console.WriteLine(" - Make sure that surgent file's name is valid. (Only: stocks.txt, crypto.txt, forex.txt)\n");
                            Console.ReadKey();
                            return;
                    }
                    if (_isDownloading)
                        Console.WriteLine($" # Retry to download {item}. Attempt[{_attempt}/8]");
                }
                UploadFile(item);
                _isDownloading = true;
            }
            Console.WriteLine(" + Downloading DONE.");
        }

        private void UploadFile(String item, Boolean isDouble=false)
        {
            //remove item from list _newItemsSurgent.
            _newItemsSurgent.Remove(item);

            //remove "item" from _fileSurgent.
            _readerSurgent.RemoveFromFile(item);

            //if isDouble is false append "item" to file _fileDownloadedand and to list _itemsDownloaded
            if (!isDouble)
            {
                _readerDownloaded.AppendToEndFile(item);
                _itemsDownloaded.Add(item);
            }   
        }

        #endregion

        //----------------------------------------------------------------------------------------
        //****************************************************************************************
        //----------------------------------------------------------------------------------------
    }
}
