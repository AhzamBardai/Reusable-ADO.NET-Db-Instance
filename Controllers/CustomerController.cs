using Microsoft.AspNetCore.Mvc;
using SqliteFromScratch.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.DB;
using System;

namespace SqliteFromScratch.Controllers {
    [Route("api/[Controller]")]
    public class CustomerController : Controller {

        public List<Customer> Customers { get; set; }
        private readonly string sql = $"select * from customers limit 20;";

        public CustomerController() {
            Customers = new List<Customer>();
            //Action<SqliteDataReader> action = reader => MakeCustomer(reader);
        }

        public void MakeCustomer() {
            DbInstance db = new(sql, reader => {
                Customer cust = new() {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Company = reader.GetValue(3).ToString(),
                    Address = reader.GetValue(4).ToString(),
                    City = reader.GetValue(5).ToString(),
                    State = reader.GetValue(6).ToString(),
                    Country = reader.GetValue(7).ToString(),
                    PostalCode = reader.GetValue(8).ToString(),
                    Phone = reader.GetValue(9).ToString(),
                    Fax = reader.GetValue(10).ToString(),
                    Email = reader.GetValue(11).ToString(),
                    SupportRepId = reader.GetInt32(12),
                };
                Customers.Add(cust);
            });
        }

        [HttpGet]
        public List<Customer> Index() {
            MakeCustomer();
            return Customers;
        }
    }
}
