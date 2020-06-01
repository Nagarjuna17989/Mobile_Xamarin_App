using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MovableBridges.Model
{
    public class District
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string District_Name { get; set; }
        public string User_Modified { get; set; }
        public System.DateTime Date_Created { get; set; }
        public System.DateTime Date_Modified { get; set; }
        public Nullable<bool> Is_Active { get; set; }
    }
}
