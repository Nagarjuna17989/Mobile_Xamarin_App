using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MovableBridges.Model
{
    public class Bridges
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }

        public DateTime dt { get; set; }
    }
}
