using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovableBridges.Model
{
    public class NavigationOpening
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Bridge_ID { get; set; }
        public string Bridge_Name { get; set; }
        public DateTime Entry_Date { get; set; }
        public TimeSpan Opening_Time { get; set; }
        public TimeSpan Closing_Time { get; set; }
        public string User_Modified { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Modified { get; set; }       
    }
}
