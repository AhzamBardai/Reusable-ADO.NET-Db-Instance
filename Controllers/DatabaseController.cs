using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.Models;
using System.Collections.Generic;
using System.IO;

namespace SqliteFromScratch.Controllers {

    // MVC is handling the routing for you.
    // old way taught in class in WOZ U that I refactored to resuse 
    [Route("api/[Controller]")]
    public class DatabaseController : Controller {

        public List<Track> Tracks { get; set; }
        public string DataSource => "Data Source=" + Path.GetFullPath("chinook.db");

        public DatabaseController() {
            Tracks = new List<Track>();
        }

        // api/database
        [HttpGet]
        public List<Track> GetData() {

            using (SqliteConnection conn = new(DataSource)) {

                conn.Open();

                // sql is the string that will be run as an sql command
                string sql = $"select * from tracks limit 200;";

                // command combines the connection and the command string and creates the query
                using (SqliteCommand command = new(sql, conn)) {

                    // reader allows you to read each value that comes back and do something to it.
                    using SqliteDataReader reader = command.ExecuteReader();

                    // Read returns true while there are more rows to advance to. then false when done.
                    while (reader.Read()) {

                        // map the data to the model.
                        // add each one to the list.
                        Track newTrack = new Track() {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            AlbumId = reader.GetInt32(2),
                            MediaTypeId = reader.GetInt32(3),
                            GenreId = reader.GetInt32(4),
                            Composer = reader.GetValue(5).ToString(),
                            Milliseconds = reader.GetInt32(6),
                            Bytes = reader.GetInt32(7),
                            UnitPrice = reader.GetInt32(8)
                        };
                        Tracks.Add(newTrack);
                    }
                }

                conn.Close();

            }

            return Tracks;
        }
    }
}