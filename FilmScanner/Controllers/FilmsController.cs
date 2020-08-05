using System.Collections.Generic;
using System.Linq;
using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class FilmsController : ControllerBase
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public FilmsController(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		// GET: api/Users/{userId}/Records
		[HttpGet("{userId}/records")]
		public IEnumerable<FilmRecord> GetFilms(int userId)
		{
			return _repositoryWrapper.Film.FindByCondition(f=>f.UserRefID == userId);
		}

		// GET: api/Users/{userId}/Records/{id}
		[HttpGet("{userId}/records/{id}")]
		public ActionResult<FilmRecord> GetFilmRecord(int userId, int id)
		{
			return _repositoryWrapper.Film.FindByCondition(f => f.UserRefID == userId && f.ID == id).First();
		}

		// PUT: api/Users/{userId}/Records/{id}
		[HttpPut("{userId}/records/{id}")]
		public IActionResult PutUser(int id, FilmRecord filmRecord)
		{
			if (filmRecord.ID != id)
			{
				return BadRequest();
			}
			_repositoryWrapper.Film.Update(filmRecord);
			_repositoryWrapper.Save();

			return NoContent();
		}

		// POST: api/Users/{userId}/Records
		[HttpPost("{userId}/records")]
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

		// DELETE: api/Users/{userId}/Records/{id}
		[HttpDelete("{userId}/records/{id}")]
		public ActionResult<FilmRecord> DeleteFilmRecord(int userId, int id)
		{
			var film = _repositoryWrapper.Film.FindByCondition(f => f.UserRefID == userId && f.ID == id).First();
			_repositoryWrapper.Film.Delete(film);
			_repositoryWrapper.Save();

			return film;
		}
	}
}
