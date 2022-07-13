using System;

namespace ManagementAssistantCharger.Model.TimeDataModel
{
    public class StockTimeDataModel
    {
        public Int32 Id { get; set; }
        public String Open { get; set; }
        public String High { get; set; }
        public String Low { get; set; }
        public String Close { get; set; }
        public String Volume { get; set; }
        public DateTime DateTime { get; set; } = DateTime.MinValue;
    }
}
