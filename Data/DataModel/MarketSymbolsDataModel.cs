using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Data.DataModel
{
    public class MarketSymbolsDataModel
    {
        public Dictionary<String, String> marketSymbols { get; set; } = new Dictionary<String, String>();
        public String whichMarket { get; set; }
    }
}
