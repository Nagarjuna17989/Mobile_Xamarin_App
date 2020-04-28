using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovableBridges.Model
{
    public class Bridge
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Recall_Number { get; set; }
        public string Bridge_Name { get; set; }
        public string Type_Draw { get; set; }     
        public int Tenders { get; set; }
        public double MilePoint { get; set; }
        public string StateRoute { get; set; }
        public int Parish_Id { get; set; }
    }
}
