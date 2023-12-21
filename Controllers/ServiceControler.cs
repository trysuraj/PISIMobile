using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PISIAssessment.Data;
using PISIAssessment.Model;
using PISIAssessment.Model.DTOs;
using PISIAssessment.PISIService;

namespace PISIAssessment.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceControler : ControllerBase

    
    {
        private readonly ISubService _subService;
   
    public ServiceControler(ISubService subService)
    {
        _subService = subService;
    }
    [HttpPost("checkstatus"),Authorize]
    public async Task<IActionResult> CheckStatusAsync(int serviceId, string phone)
    {
            var status = await _subService.CheckStatus(serviceId, phone);
        
            return Ok(status);
       
    }
    [HttpPost("subscribe"),Authorize]
    public async Task<IActionResult> SubscribeAsync(RequestDto request)
    {
        var clean = new Subscriber();
         var result = await _subService.SubscribeUserAsync( clean, request.ServiceId, request.Phone );

        if (result)
        {
            return Ok("Subscription successful");
        }
        else
        {
            return BadRequest("Failed to subscribe user");
        }

        }
    
         [HttpPost("unsubscribe"),Authorize]
        public async Task<IActionResult> UnsubscribeAsync(int serviceId, string phone)
        {
            
            var result = await _subService.UnsubscribeUserAsync(serviceId, phone);

        if (result)
        {
            return Ok("Unsubscribed successful");
        }
        else
        {
            return BadRequest("Failed to unsubscribe user");
        }

        }
        
    }

}
    
