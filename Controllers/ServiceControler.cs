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
    public async Task<IActionResult> CheckStatusAsync(RequestDto request)
    {
            var status = await _subService.CheckStatus(request);
        
            return Ok(status);
       
    }
    [HttpPost("subscribe"),Authorize]
    public async Task<IActionResult> SubscribeAsync(RequestDto request)
    {
        
        if (await _subService.SubscribeUserAsync(request))
            {
                return Ok(new { Message = "Subscribed successfully" });
            }

            return BadRequest(new { Message = "User is already subscribed" });
        }
    
         [HttpPost("unsubscribe"),Authorize]
        public async Task<IActionResult> UnsubscribeAsync(RequestDto request)
        {
            await _subService.UnsubscribeUserAsync(request);
            return Ok(new { Message = "Unsubscribed successfully" });
        }
    }


    

}
    
