using System.ComponentModel.DataAnnotations;
using System;

namespace SqliteFromScratch.Models {
    public class Customer {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int? SupportRepId { get; set; }

    }
}
