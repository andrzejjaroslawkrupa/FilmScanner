using System.Collections.Generic;
using System.Linq;
using FilmScanner.Contracts;
using Microsoft.AspNetCore.Mvc;
using FilmScanner.Entities.Models;

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
		public IEnumerable<User> GetUsers()
		{
			return _repositoryWrapper.User.FindAll();
		}

		// GET: api/Users/5
		[HttpGet("{id}")]
		public ActionResult<User> GetUser(int id)
		{
			var user = _repositoryWrapper.User.FindByCondition(u => u.ID == id).Single();

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		// PUT: api/Users/5
		[HttpPut("{id}")]
		public IActionResult PutUser(int id, User user)
		{
			if (user.ID != id)
			{
				return BadRequest();
			}
			_repositoryWrapper.User.Update(user);
			_repositoryWrapper.Save();

			return NoContent();
		}

		// POST: api/Users
		[HttpPost]
		public ActionResult<User> PostUser(User user)
		{
			_repositoryWrapper.User.Create(user);
			_repositoryWrapper.Save();

			return CreatedAtAction("PostUser", new { id = user.ID }, user);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		public ActionResult<User> DeleteUser(int id)
		{
			var user = _repositoryWrapper.User.FindByCondition(u => u.ID == id).Single();
			if (user == null)
			{
				return NotFound();
			}

			_repositoryWrapper.User.Delete(user);
			_repositoryWrapper.Save();

			return user;
		}

		//private readonly UserContext _context;

		//public UsersController(UserContext context)
		//{
		//	_context = context;
		//}

		//#region Users

		//// GET: api/Users
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		//{
		//	return await _context.Users.ToListAsync();
		//}

		//// GET: api/Users/5
		//[HttpGet("{id}")]
		//public async Task<ActionResult<User>> GetUser(int id)
		//{
		//	var user = await _context.Users.FindAsync(id);

		//	if (user == null)
		//	{
		//		return NotFound();
		//	}

		//	return user;
		//}

		//// PUT: api/Users/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		//// more details see https://aka.ms/RazorPagesCRUD.
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutUser(int id, User user)
		//{
		//	if (id != user.ID)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Entry(user).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!UserExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		//// POST: api/Users
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		//// more details see https://aka.ms/RazorPagesCRUD.
		//[HttpPost]
		//public async Task<ActionResult<User>> PostUser(User user)
		//{
		//	_context.Users.Add(user);
		//	await _context.SaveChangesAsync();

		//	var tableName = "Films_" + user.ID.ToString();

		//	using (var context = new UserContext())
		//	{
		//		await context.Database.ExecuteSqlRawAsync((string)$"CREATE TABLE {tableName} (ID int  IDENTITY(1,1) PRIMARY KEY, ExternalID varchar(255), CreatedAt datetime2(7) NOT NULL)");
		//	}

		//	return CreatedAtAction("GetUser", new { id = user.ID }, user);
		//}

		//// DELETE: api/Users/5
		//[HttpDelete("{id}")]
		//public async Task<ActionResult<User>> DeleteUser(int id)
		//{
		//	var user = await _context.Users.FindAsync(id);
		//	if (user == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.Users.Remove(user);
		//	await _context.SaveChangesAsync();

		//	return user;
		//}

		//private bool UserExists(int id)
		//{
		//	return _context.Users.Any(e => e.ID == id);
		//}

		//#endregion

		
	}
}