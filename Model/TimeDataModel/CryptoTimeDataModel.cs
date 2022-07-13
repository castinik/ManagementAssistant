using System;

namespace ManagementAssistant.Model.TimeDataModel
{
    public class CryptoTimeDataModel
    {
        public Int32 Id { get; set; }
        public String Open { get; set; }
        public String High { get; set; }
        public String Low { get; set; }
        public String Close { get; set; }
        public String Volume { get; set; }
        public String MarketCap { get; set; }
        public DateTime DateTime { get; set; } = DateTime.MinValue;
    }
}
