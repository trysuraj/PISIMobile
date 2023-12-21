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

        public async Task<bool> SubscribeUserAsync(Subscriber request, int serviceId, string phone)
    {
        try
        {
            
            var existingSubscription = await _context.Subscribers
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId && s.Phone == phone);

            // if (string.IsNullOrEmpty(request.Phone) || string.IsNullOrEmpty(request.Token))
            // {
            //     return false; 
            // }

            if( existingSubscription != null)
            {
                return false;
            }
            
            _context.Subscribers.Add(request);
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

       public async Task<bool>  UnsubscribeUserAsync( int serviceId, string phone)
    {
        try
        {
            // Find the subscription record
             var subscriber = await _context.Subscribers
            .FirstOrDefaultAsync(s => s.ServiceId == serviceId && s.Phone == phone);

            if (subscriber != null)
            {
                
                subscriber.UnsubscriptionDate = DateTime.UtcNow;

                
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
      } 
        
         public async Task<object> CheckStatus( int serviceId, string phone)
        {
            
             var subscriber = await _context.Subscribers
            .FirstOrDefaultAsync(s => s.ServiceId == serviceId && s.Phone == phone);

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