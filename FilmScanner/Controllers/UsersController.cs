using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public UsersController(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		// GET: api/Users
		[HttpGet]
		public async Task<IEnumerable<User>> GetAllUsers()
		{
			return await _repositoryWrapper.User.GetAllUsersAsync();
		}

		// GET: api/Users/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		// POST: api/Users
		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			_repositoryWrapper.User.Create(user);
			await _repositoryWrapper.SaveAsync();

			return CreatedAtAction("CreateUser", new { id = user.ID }, user);
		}

		// PUT: api/Users/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, User user)
		{
			if (user.ID != id)
			{
				return BadRequest();
			}
			_repositoryWrapper.User.Update(user);
			await _repositoryWrapper.SaveAsync();

			return NoContent();
		}

		// DELETE: api/Users/{id}
		[HttpDelete("{id}")]
		public async Task<ActionResult<User>> DeleteUser(int id)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_repositoryWrapper.User.Delete(user);
			await _repositoryWrapper.SaveAsync();

			return user;
		}
	}
}