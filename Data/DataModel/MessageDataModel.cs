using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Data.DataModel
{
    public class MessageDataModel
    {
        public String message { get; set; }
        public Boolean isError { get; set; } = false;
    }
}
