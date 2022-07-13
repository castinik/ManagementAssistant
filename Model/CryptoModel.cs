using ManagementAssistant.Model.MetaData;
using ManagementAssistant.Model.TimeDataModel;
using System.Collections.Generic;

namespace ManagementAssistant.Model
{
    public class CryptoModel
    {
        public CryptoMetaDataModel CryptoMetaDataModel { get; set; } = new CryptoMetaDataModel();
        public List<CryptoTimeDataModel> CryptoTimeDataModels { get; set; } = new List<CryptoTimeDataModel>();
    }
}
