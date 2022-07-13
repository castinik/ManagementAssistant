using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Data.DataModel
{
    public class MarketSymbolsDataModel
    {
        public List<String> marketSymbols { get; set; } = new List<String>();
        public String whichMarket { get; set; }
    }
}
