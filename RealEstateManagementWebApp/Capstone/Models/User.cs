using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class User 
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set;}

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string  Role { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

    }
}
