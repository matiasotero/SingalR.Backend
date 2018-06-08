using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class Action
    {
        public int ActionNumber { get; set; }
        public int TeamNumber { get; set; }
        public int Pno { get; set; }
        public string Clock { get; set; }
        public int Period { get; set; }
        public string ActionType { get; set; }
        public string SubType { get; set; }
        public int Success { get; set; }
        public string Score1 { get; set; }
        public string Score2 { get; set; }
        public string PeriodType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Area { get; set; }
        public string Side { get; set; }
    }
}
