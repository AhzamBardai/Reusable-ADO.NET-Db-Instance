using Microsoft.AspNetCore.Mvc;
using SqliteFromScratch.Models;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.DB;
using System;

namespace SqliteFromScratch.Controllers {
    [Route("api/[Controller]")]
    public class EmployeeController : Controller {

        public List<Employee> Employees { get; set; }
        private readonly string sql = $"select * from employees where HireDate < '2003-01-01';";

        public EmployeeController() {
            Employees = new List<Employee>();
            Action<SqliteDataReader> action = reader => MakeEmployee(reader);
            DbInstance db = new DbInstance(sql, action);
        }

        public void MakeEmployee( SqliteDataReader reader ) {
             Employee emp = new Employee() {
                 Id = reader.GetInt32(0),
                 FirstName = reader.GetString(1),
                 LastName = reader.GetString(2),
                 Title = reader.GetValue(3).ToString(),
                 ReportsTo = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                 BirthDate = reader.GetDateTime(5),
                 HireDate = reader.GetDateTime(6),
                 Address = reader.GetValue(7).ToString(),
                 City = reader.GetValue(8).ToString(),
                 State = reader.GetValue(9).ToString(),
                 Country = reader.GetValue(10).ToString(),
                 PostalCode = reader.GetValue(11).ToString(),
                 Phone = reader.GetValue(12).ToString(),
                 Fax = reader.GetValue(13).ToString(),
                 Email = reader.GetValue(14).ToString(),
             };
            Employees.Add(emp);
        }

        [HttpGet]
        public List<Employee> Index() {
            return Employees;
        }
    }
}
