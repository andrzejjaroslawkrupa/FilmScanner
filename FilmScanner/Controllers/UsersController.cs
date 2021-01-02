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

		[HttpGet]
		public async Task<IEnumerable<User>> GetAllUsers()
		{
			return await _repositoryWrapper.User.GetAllUsersAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(id);

			if (user == null)
				return NotFound();

			return user;
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			if (user == null)
				return BadRequest();

			_repositoryWrapper.User.Create(user);
			await _repositoryWrapper.SaveAsync();

			return CreatedAtAction("CreateUser", new { id = user.ID }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, User user)
		{
			if (user == null || user.ID != id)
				return BadRequest();
			
			_repositoryWrapper.User.Update(user);
			await _repositoryWrapper.SaveAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<User>> DeleteUser(int id)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(id);
			if (user == null)
				return NotFound();

			_repositoryWrapper.User.Delete(user);
			await _repositoryWrapper.SaveAsync();

			return user;
		}
	}
}