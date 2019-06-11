using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalkeeAPI.Models;

namespace TalkeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context??throw new Exception("Error");
        }
        // Post /auth/login
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");

            }


            var userLogin = _context.Users.Where(u => u.Email == user.email && u.Password == user.password).ToList().FirstOrDefault();

            
            if (userLogin != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userLogin.Email)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                var userjson = Newtonsoft.Json.JsonConvert.SerializeObject(userLogin);
                return Ok(new { Token = tokenString, userlog = userjson });
            }
            else
            {
                return Unauthorized();
            }
        }

        // Post /auth/register
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody]RegisterModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var userRegistered= _context.Users.Where(u => u.Email == user.email && u.Password == user.password).ToList().FirstOrDefault();
            
            if (userRegistered == null)
            {
                UserModel newUser = new UserModel();
                newUser.Email = user.email;
                newUser.Password = user.password;
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Invalid client request");
            }
        }
    }
}
