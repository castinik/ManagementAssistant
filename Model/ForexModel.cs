using ManagementAssistantCharger.Model.MetaDataModel;
using ManagementAssistantCharger.Model.TimeDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistantCharger.Model
{
    public class ForexModel
    {
        public ForexMetaDataModel ForexMetaDataModel { get; set; } = new ForexMetaDataModel();
        public List<ForexTimeDataModel> ForexTimeDataModels { get; set; } = new List<ForexTimeDataModel>();
    }
}
