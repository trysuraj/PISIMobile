using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PISIAssessment.Model;
using PISIAssessment.Model.DTOs;
using PISIAssessment.PISIService;

namespace PISIAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControler : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthControler(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseService<int>>> Register(ServiceRegisterDto request)
        {
            var response = await _auth.Register(
                new Service { Username = request.Username }, request.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseService<int>>> Login(ServiceLogInDto request)
        {
            var response = await _auth.Login(request.Username, request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}