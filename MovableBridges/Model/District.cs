using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MovableBridges.Model
{
    public class District
    {
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string District_Name { get; set; }
    }
}
