using System;

namespace ManagementAssistantCharger.Model.MetaDataModel
{
    public class CryptoMetaDataModel
    {
        public Int32 Id { get; set; }
        public String Information { get; set; }
        public String DigitalCurrencyCode { get; set; }
        public String DigitalCurrencyName { get; set; }
        public String MarketCode { get; set; }
        public String MarketName { get; set; }
        public String LastRefreshed { get; set; }
        public String TimeZone { get; set; }
        public String Interval { get; set; } = String.Empty;
    }
}
