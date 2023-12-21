using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PISIAssessment.Model.DTOs
{
    public class RequestDto
    {
        public int ServiceId {get; set; }
        public string Token {get; set; } = string.Empty;
        public string  Phone {get; set; } = string.Empty;
        public string SubscriberId { get; set;} = string.Empty;
    }
}