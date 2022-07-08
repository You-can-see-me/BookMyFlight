using FlightUser.Service.EntityModels;
using FlightUser.Service.Repository;
using FlightUser.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly BookMyFlightDBContext db;
        private readonly IJWTMangerRepository iJWTMangerRepository;

        public UserLoginController(BookMyFlightDBContext _db, IJWTMangerRepository _iJWTMangerRepository)
        {
            db = _db;
            iJWTMangerRepository = _iJWTMangerRepository;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var token = iJWTMangerRepository.Authenticate(loginViewModel, false);
            if (token == null)
            {
                return Unauthorized("Unauthorized User");
            }
            return Ok(token);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(LoginViewModel registerViewModel)
        {
            var token = iJWTMangerRepository.Authenticate(registerViewModel, true);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
