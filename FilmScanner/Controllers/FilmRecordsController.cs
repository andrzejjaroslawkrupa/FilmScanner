using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class FilmRecordsController : ControllerBase
	{
		private readonly IRepositoryWrapper _repositoryWrapper;

		public FilmRecordsController(IRepositoryWrapper repositoryWrapper)
		{
			_repositoryWrapper = repositoryWrapper;
		}

		// GET: api/Users/{userId}/Records
		[HttpGet("{userId}/records")]
		public async Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUser(int userId)
		{
			return await _repositoryWrapper.Film.GetAllFilmRecordsForUserAsync(userId);
		}

		// GET: api/Users/{userId}/Records/{id}
		[HttpGet("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> GetFilmRecordForUser(int userId, int id)
		{
			return await _repositoryWrapper.Film.GetFilmRecordForUserByIdAsync(userId, id);
		}

		// PUT: api/Users/{userId}/Records/{id}
		[HttpPut("{userId}/records/{id}")]
		public async Task<IActionResult> PutFilmRecord(int id, FilmRecord filmRecord)
		{
			if (filmRecord.ID != id)
			{
				return BadRequest();
			}
			_repositoryWrapper.Film.Update(filmRecord);
			await _repositoryWrapper.SaveAsync();

			return NoContent();
		}

		// POST: api/Users/{userId}/Records
		[HttpPost("{userId}/records")]
		public async Task<ActionResult<FilmRecord>> PostFilmRecord(int userId, FilmRecord film)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			_repositoryWrapper.Film.Create(film);
			await _repositoryWrapper.SaveAsync();

			return CreatedAtAction("PostFilmRecord", new { id = film.ID }, film);
		}

		// DELETE: api/Users/{userId}/Records/{id}
		[HttpDelete("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> DeleteFilmRecord(int userId, int id)
		{
			var film = await _repositoryWrapper.Film.GetFilmRecordForUserByIdAsync(userId, id);
			_repositoryWrapper.Film.Delete(film);
			await _repositoryWrapper.SaveAsync();

			return film;
		}
	}
}
