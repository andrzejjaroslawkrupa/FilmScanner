using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmScanner.Data;
using FilmScanner.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;

namespace FilmScanner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserContext _context;

		public UsersController(UserContext context)
		{
			_context = context;
		}

		#region Users

		// GET: api/Users
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		// GET: api/Users/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _context.Users.FindAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			return user;
		}

		// PUT: api/Users/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user)
		{
			if (id != user.ID)
			{
				return BadRequest();
			}

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Users
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			var tableName = "Films_" + user.ID.ToString();

			using (var context = new UserContext())
			{
				await context.Database.ExecuteSqlRawAsync((string)$"CREATE TABLE {tableName} (ID int  IDENTITY(1,1) PRIMARY KEY, ExternalID varchar(255), CreatedAt datetime2(7) NOT NULL)");
			}

			return CreatedAtAction("GetUser", new { id = user.ID }, user);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<User>> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return user;
		}

		private bool UserExists(int id)
		{
			return _context.Users.Any(e => e.ID == id);
		}

		#endregion

		#region Films

		// GET: api/Users/1/Films
		[HttpGet("{userId}/films")]
		public async Task<ActionResult<IEnumerable<Film>>> GetFilms(int userId)
		{
			List<Film> films;

			if (!UserExists(userId))
			{
				return NotFound();
			}

			var tableName = "Films_" + userId.ToString();

			using (var context = new UserContext())
			{
				films = await context.Films.FromSqlRaw((string)$"SELECT * FROM {tableName}").ToListAsync();
			}

			return films;
		}

		// GET: api/Users/1/Films/2
		[HttpGet("{userId}/films/{id}")]
		public async Task<ActionResult<Film>> GetFilm(int userId, int id)
		{
			Film film;

			if (!UserExists(userId))
			{
				return NotFound();
			}

			var tableName = "Films_" + userId.ToString();

			using (var context = new UserContext())
			{
				film = await context.Films.FromSqlRaw((string)$"SELECT * FROM {tableName} WHERE ID = {id}").FirstOrDefaultAsync();
			}

			return film;
		}


		// POST: api/Users/1/films
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPost("{userId}/films")]
		public async Task<ActionResult<Film>> PostFilm(int userId, Film film)
		{
			if (!UserExists(userId))
			{
				return NotFound();
			}

			var tableName = "Films_" + userId.ToString();

			using (var context = new UserContext())
			{
				await context.Database.ExecuteSqlRawAsync((string)$"INSERT INTO {tableName} (ExternalID, CreatedAt) VALUES ('{film.ExternalID}', '{film.CreatedAt}')");
			}

			return film;
		}

		// DELETE: api/Users/1/films/1
		[HttpDelete("{userId}/films/{id}")]
		public async Task<ActionResult<Film>> DeleteFilm(int userId, int id)
		{
			Film film;

			var tableName = "Films_" + userId.ToString();

			using (var context = new UserContext())
			{
				film = await context.Films.FromSqlRaw((string)$"SELECT * FROM {tableName} WHERE ID = {id}").FirstOrDefaultAsync();
				await context.Database.ExecuteSqlRawAsync((string)$"Delete FROM {tableName} WHERE ID = {id}");
			}

			return film;
		}

		#endregion
	}
}