using System.Collections.Generic;
using System.Linq;
using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmsController : ControllerBase
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public FilmsController(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		// GET: api/Films
		[HttpGet]
		public IEnumerable<FilmRecord> GetFilms()
		{
			return _repositoryWrapper.Film.FindAll();
		}

		// GET: api/Users/1/FilmsRecords/2
		[HttpGet("{userId}/record/{id}")]
		public ActionResult<FilmRecord> GetFilmRecord(int userId, int id)
		{
			return _repositoryWrapper.Film.FindByCondition(f => f.UserRefID == userId && f.ID == id).First();
		}


		// POST: api/Users/1/films
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPost("{userId}")]
		public ActionResult<FilmRecord> PostFilmRecord(int userId, FilmRecord film)
		{
			if (!_repositoryWrapper.User.FindByCondition(u => u.ID == userId).Any())
			{
				return NotFound();
			}

			_repositoryWrapper.Film.Create(film);
			_repositoryWrapper.Save();

			return CreatedAtAction("PostFilmRecord", new { id = film.ID }, film);
		}

		//// DELETE: api/Users/1/films/1
		//[HttpDelete("{userId}/films/{id}")]
		//public async Task<ActionResult<FilmRecord>> DeleteFilm(int userId, int id)
		//{
		//	FilmRecord film;

		//	var tableName = "Films_" + userId.ToString();

		//	using (var context = new UserContext())
		//	{
		//		film = await context.FilmsRecords.FromSqlRaw((string)$"SELECT * FROM {tableName} WHERE ID = {id}").FirstOrDefaultAsync();
		//		await context.Database.ExecuteSqlRawAsync((string)$"Delete FROM {tableName} WHERE ID = {id}");
		//	}

		//	return film;
		//}

		//#endregion
	}
}
