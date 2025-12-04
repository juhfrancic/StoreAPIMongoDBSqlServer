using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Situacao { get; set; }

        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Situacao = true;
        }

        public Customer(string firstName, string lastName, 
            string email, string? phoneNumber) 
            : this(firstName, lastName, email)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
