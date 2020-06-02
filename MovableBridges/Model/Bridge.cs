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
        public double Mile_Point { get; set; }
        public string State_Route { get; set; }
        public int Parish_Id { get; set; }
        public string User_Modified { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Modified { get; set; }
    }
}
