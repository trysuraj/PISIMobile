using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PISIAssessment.Data;
using PISIAssessment.Model;
using PISIAssessment.Model.DTOs;

namespace PISIAssessment.PISIService
{
    public  class SubService : ISubService
    {
        private readonly MyDbContext _context;
        public  SubService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubscribeUserAsync(RequestDto request)
    {
        try
        {
            
            var existingSubscription = await _context.Subscribers
                .FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId && s.Phone == request.Phone);

            if (string.IsNullOrEmpty(request.Phone) || string.IsNullOrEmpty(request.Token))
            {
                return false; 
            }

            
            var newSubscription = new Subscriber
            {
                ServiceId = request.ServiceId,
                Token = request.Token,
                Phone = request.Phone,
                SubscriptionDate = DateTime.UtcNow
            };

            _context.Subscribers.Add(newSubscription);
            await _context.SaveChangesAsync();

            return true; 
        }
        catch (Exception)
        {
           
            return false; 
        }
           
        }
        public async Task<bool> IsValidTokenAsync(string token)
        {
        var tokenRecord = await _context.Subscribers
            .FirstOrDefaultAsync(s => s.Token == token);

        if (tokenRecord != null && tokenRecord.Expires > DateTime.UtcNow)
        {
            return true;
        }

        return false;
    }

       public async Task UnsubscribeUserAsync(RequestDto request)
    {
        try
        {
            // Find the subscription record
            var subscription = await _context.Subscribers
                .FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId && s.Phone == request.Phone);

            if (subscription != null)
            {
                
                subscription.UnsubscriptionDate = DateTime.UtcNow;

                
                await _context.SaveChangesAsync();
            }
            
        }
        catch (Exception)
        {
            
        }
      } 
        
         public async Task<object> CheckStatus(RequestDto request)
        {
            
             var subscriber = await _context.Subscribers
            .FirstOrDefaultAsync(s => s.ServiceId == request.ServiceId && s.Phone == request.Phone);

        if (subscriber != null)
        {
            bool isSubscribed = subscriber.SubscriptionDate != null && subscriber.UnsubscriptionDate == null;

            var status = new
            {
                Subscribed = isSubscribed,
                SubscriptionDate = isSubscribed ? subscriber.SubscriptionDate : null,
                UnsubscriptionDate = isSubscribed ? null : subscriber.UnsubscriptionDate
            };

            return status;
        }

        return new {Message = "User not found"};
        }
    }
}