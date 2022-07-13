using ManagementAssistant.Model.MetaData;
using ManagementAssistant.Model.TimeDataModel;
using System.Collections.Generic;

namespace ManagementAssistant.Model
{
    public class StockModel
    {
        public StockMetaDataModel StockMetaDataModel { get; set; } = new StockMetaDataModel();
        public List<StockTimeDataModel> StockTimeDataModels { get; set; } = new List<StockTimeDataModel>();
    }
}
