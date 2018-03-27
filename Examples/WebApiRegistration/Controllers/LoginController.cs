using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Gearstone.PushNotifications;

using System.Text;

namespace RegistrationWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : Controller
    {
        readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Credentials model)
        {
            if (model.Username == "username" && model.Password == "password")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiry = DateTime.UtcNow.AddMinutes(30);

                var token = new JwtSecurityToken(
                    issuer: "yourdomain.com",
                    audience: "yourdomain.com",
                    claims: claims,
                    expires: expiry,
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires = expiry
                });
            }

            return BadRequest("Could not verify username and password");
        }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
