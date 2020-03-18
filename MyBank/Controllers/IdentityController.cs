using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBank.Models;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult Token([FromBody]IdentityModel model)
        {
            var identity = GetIdentity(model.UserName, model.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
           
            var jwt = new JwtSecurityToken(
                    issuer: "issuer",
                    audience: "audience",
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(3)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("somethingyouwantwhichissecurewillworkk")), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                userName = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {            
            if (username=="user1" && password=="password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,username),                  
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }            
            return null;
        }

    }
}