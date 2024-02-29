using BankAPI.Models;
using BankAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext _context;
        private IConfiguration _configuration;
        
        public UserController(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.PassWord))
                {
                    return BadRequest();
                }

                var user = _context.User.Where(s => s.UserName == model.UserName).FirstOrDefault<User>();

                if (user != null)
                {
                    var result = BCrypt.Net.BCrypt.Verify(model.PassWord, user.PassWord);

                    if (result)
                    {
                        var token = GenerateJwtToken(user.UserName);
                        return Ok(new { user = user, token = token });
                    }                            
                    return Ok(new { message = "username or password is invalid", status = 200});
                }

                return Ok(new { message = "username or password is invalid", status = 200 });
            }
            catch (Exception ex)
            {
                return null;
            }     
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                var existingUser = _context.User.FirstOrDefault(u => u.UserName == model.UserName);
                if (existingUser != null)
                {
                    return Conflict("Username already exists.");
                }
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.PassWord);

                User user = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PassWord = passwordHash,
                    CurrentBalance = 0
                };
          
                _context.User.Add(user);
                _context.SaveChanges();

                return Ok(model) ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GenerateJwtToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["AuthenticationIdentitySigningKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, username)
                    }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(45),
                Issuer = _configuration["AuthenticationIdentityIssuer"],
                Audience = _configuration["AuthenticationIdentityAudience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
