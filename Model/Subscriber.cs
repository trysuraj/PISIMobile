using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PISIAssessment.Model
{
    public class Subscriber
    {
        [Key]
        public string SubscriberId { get; set;}  = Guid.NewGuid().ToString();
        public string Phone { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } 
        public DateTime Expires { get; set; }
        public DateTime?  SubscriptionDate  { get; set;}  = DateTime.Now;
        public DateTime?  UnsubscriptionDate  { get; set;} = DateTime.Now;
        public int ServiceId{get; set;}
       
        public Service? service { get; set; }
        
    }
    
}