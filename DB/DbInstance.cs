using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SqliteFromScratch.DB {
    public class DbInstance {

        public string DataSource => "Data Source=" + Path.GetFullPath("chinook.db");

        public DbInstance(string sql, Action<SqliteDataReader> func) {
            using (SqliteConnection conn = new(DataSource)) {
                conn.Open();
                using (SqliteCommand command = new(sql, conn)) {

                    using SqliteDataReader reader = command.ExecuteReader();

                    while (reader.Read()) {                        
                        func(reader);
                    }
                }
                conn.Close();
            }
        }

        
    }
}
