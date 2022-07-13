using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistantCharger.Model.MetaDataModel
{
    public class ForexMetaDataModel
    {
        public Int32 Id { get; set; }
        public String Information { get; set; }
        public String FromSymbol { get; set; }
        public String ToSymbol { get; set; }
        public String OutputSize { get; set; }
        public String LastRefreshed { get; set; }
        public String TimeZone { get; set; }
        public String Interval { get; set; } = String.Empty;
    }
}
