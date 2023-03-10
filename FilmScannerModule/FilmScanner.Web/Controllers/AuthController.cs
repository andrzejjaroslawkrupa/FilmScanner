using FilmScanner.Entities.Models;
using FilmScanner.Web.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FilmScanner.Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = Environment.GetEnvironmentVariable("FILMSCANNER_SECRET");
                if (secret == null)
                    throw new NullReferenceException(nameof(secret));
                var key = Encoding.UTF8.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = "filmscanner-consumer",
                    Issuer = "filmscanner",
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                tokenDescriptor.Subject.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }
        }
    }
}
