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
       Task<bool> SubscribeUserAsync(RequestDto request);
        Task UnsubscribeUserAsync(RequestDto request);
        Task<bool> IsValidTokenAsync(string tokenId);
          Task<object> CheckStatus(RequestDto request);
    }
}