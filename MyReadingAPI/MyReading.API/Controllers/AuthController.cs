﻿using Microsoft.AspNetCore.Mvc;
using MyReading.API.Service;

namespace MyReading.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "Andrey" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Model.Book());
                return Ok(token);
            }

            return BadRequest("username or password invalid");
        }
    }
}