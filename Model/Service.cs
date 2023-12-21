using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PISIAssessment.Model
{
    public class Service
    {
        [Key]
        public int Id {get; set;}
        public int ServiceId { get; set;}
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
       public Subscriber? subscriber{get; set;} 

    }
} 