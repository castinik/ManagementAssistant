using ManagementAssistantCharger.Model.MetaDataModel;
using ManagementAssistantCharger.Model.TimeDataModel;
using System.Collections.Generic;

namespace ManagementAssistantCharger.Model
{
    public class StockModel
    {
        public StockMetaDataModel StockMetaDataModel { get; set; } = new StockMetaDataModel();
        public List<StockTimeDataModel> StockTimeDataModels { get; set; } = new List<StockTimeDataModel>();
    }
}
