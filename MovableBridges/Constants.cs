using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MovableBridges
{
    public static class Constants
    {
        public const string DBName = "MVB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string dBPath
        {
            get
            {
                var basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DBName);
            }
        }
    }
}
