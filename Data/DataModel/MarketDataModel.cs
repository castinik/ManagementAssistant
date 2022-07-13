using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Data.DataModel
{
    public class MarketDataModel
    {
        public DataTable dataTable { get; set; } = new DataTable();
        public float maxValue { get; set; }
        public float minValue { get; set; }
        public DateTime lastRefresh { get; set; }
        public String informations { get; set; }
    }
}
