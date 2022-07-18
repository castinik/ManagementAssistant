using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistantTester.Model
{
    public class DataRowModel
    {
        public Int32 Id { get; set; }
        public Int32 Xvalue { get; set; }
        public String Symbol { get; set; }
        public String Name { get; set; }
        public String Open { get; set; }
        public String High { get; set; }
        public String Low { get; set; }
        public String Close { get; set; }
        public String Volume { get; set; }
    }
}
