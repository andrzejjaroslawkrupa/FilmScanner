using FilmScanner.Entities.Models;
using FilmScanner.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userInput)
        {
            var user = new User { UserName = userInput.UserName, Email = userInput.Email };
            var userResult = await _userManager.CreateAsync(user, userInput.Password);
            var roleResult = await _userManager.AddToRoleAsync(user, userInput.Role);
            if (userResult.Succeeded && roleResult.Succeeded)
            {
                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            }
            return BadRequest(userResult.Errors.Concat(roleResult.Errors));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userInput)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = userInput.UserName;
            user.Email = userInput.Email;
            var roleResult = await _userManager.AddToRoleAsync(user, userInput.Role);
            var userResult = await _userManager.UpdateAsync(user);
            if (roleResult.Succeeded && userResult.Succeeded)
            {
                return Ok(user);
            }
            return BadRequest(userResult.Errors.Concat(roleResult.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var defaultRole = "User";
                    if (!await _roleManager.RoleExistsAsync(defaultRole))
                    {
                        await _roleManager.CreateAsync(new IdentityRole<Guid>(defaultRole));
                    }
                    await _userManager.AddToRoleAsync(user, defaultRole);

                    return Ok(new { message = "User registered successfully" });
                }
                else
                {
                    return BadRequest(new { message = "Failed to register user", errors = result.Errors });
                }
            }
            return BadRequest(new { message = "Invalid registration data" });
        }
    }
}