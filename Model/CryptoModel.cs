using ManagementAssistantCharger.Model.MetaDataModel;
using ManagementAssistantCharger.Model.TimeDataModel;
using System.Collections.Generic;

namespace ManagementAssistantCharger.Model
{
    public class CryptoModel
    {
        public CryptoMetaDataModel CryptoMetaDataModel { get; set; } = new CryptoMetaDataModel();
        public List<CryptoTimeDataModel> CryptoTimeDataModels { get; set; } = new List<CryptoTimeDataModel>();
    }
}
