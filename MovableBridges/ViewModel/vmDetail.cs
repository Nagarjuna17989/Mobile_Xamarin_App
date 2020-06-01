using MovableBridges.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovableBridges.ViewModel
{
    public class vmDetail
    {
        public int ID { get; set; }
        public DateTime Entry_Date { get; set; }
        public TimeSpan Opening_Time { get; set; }
        public TimeSpan Closing_Time { get; set; }

        public Parish parish { get; set; }
        public Bridge bridge { get; set; }
        public District district { get; set; }
        //public int Bridge_ID { get; set; }
        //public string Bridge_Name { get; set; }
        //public int Parish_ID { get; set; }
        //public string Parish_Name { get; set; }
        ////public int District_ID { get; set; }
        //public string District_Name { get; set; }
    }
}
