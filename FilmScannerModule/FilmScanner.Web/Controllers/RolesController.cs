using FilmScanner.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RolesController(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _roleManager.Roles.Select(r => r.Name);
        }

        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            return (await _roleManager.FindByIdAsync(id)).Name;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IdentityRoleDto identityRole)
        {
            if (identityRole == null)
            {
                return BadRequest(string.Empty);
            }

            await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = identityRole.Name });

            return Ok(identityRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] IdentityRoleDto identityRole)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = identityRole.Name;

            await _roleManager.UpdateAsync(role);

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);

            return Ok(role);
        }
    }
}
