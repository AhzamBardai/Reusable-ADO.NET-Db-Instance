using Microsoft.AspNetCore.Mvc;
using SqliteFromScratch.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.DB;
using System;

namespace SqliteFromScratch.Controllers {
    [Route("api/[Controller]")]
    public class TrackController : Controller {

        public List<Track> Tracks { get; set; }
        private readonly string sql = $"select * from tracks limit 200;";

        public TrackController() {
            Tracks = new List<Track>();
            Action<SqliteDataReader> action = reader => MakeTrack(reader);
            DbInstance db = new DbInstance(sql, action);
        }

        public void MakeTrack( SqliteDataReader reader ) {
            Track track = new Track() {
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
            Tracks.Add(track);
        }

        [HttpGet]
        public List<Track> Index() {
            return Tracks;
        }
    }
}
