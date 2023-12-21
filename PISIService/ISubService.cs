using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PISIAssessment.Model;
using PISIAssessment.Model.DTOs;

namespace PISIAssessment.PISIService
{
    public interface ISubService
    {
       Task<bool> SubscribeUserAsync(Subscriber request, int serviceId, string phone);
        Task <bool> UnsubscribeUserAsync( int serviceId, string phone);
        Task<bool> IsValidTokenAsync(string tokenId);
          Task<object> CheckStatus( int serviceId, string phone);
    }
}