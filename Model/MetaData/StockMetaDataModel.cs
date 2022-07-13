using System;

namespace ManagementAssistant.Model.MetaData
{
    public class StockMetaDataModel
    {
        public Int32 Id { get; set; }
        public String Information { get; set; }
        public String Symbol { get; set; }
        public String LastRefreshed { get; set; }
        public String OutputZone { get; set; }
        public String TimeZone { get; set; }
        public String Interval { get; set; } = String.Empty;
        public String CurrentValue { get; set; }
    }
}
