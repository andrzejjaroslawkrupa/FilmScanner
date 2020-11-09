using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Api.Controllers
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

		[HttpGet("{userId}/records")]
		public async Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUser(int userId)
		{
			return await _repositoryWrapper.FilmRecord.GetAllFilmRecordsForUserAsync(userId);
		}

		[HttpGet("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> GetFilmRecordForUser(int userId, int id)
		{
			if (await UserDoesNotExist(userId))
				return NotFound();

			var record = await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(userId, id);

			if (record == null)
				return NotFound();

			return record;
		}

		[HttpPost("{userId}/records")]
		public async Task<ActionResult<FilmRecord>> CreateFilmRecord(int userId, FilmRecord film)
		{
			if (film == null)
				return BadRequest();
			
			if (await UserDoesNotExist(userId))
				return NotFound();

			_repositoryWrapper.FilmRecord.Create(film);
			await _repositoryWrapper.SaveAsync();

			return CreatedAtAction("CreateFilmRecord", new { id = film.ID }, film);
		}

		[HttpPut("{userId}/records/{id}")]
		public async Task<IActionResult> UpdateFilmRecord(int userId, int id, FilmRecord filmRecord)
		{
			if (await UserDoesNotExist(userId))
				return NotFound();

			if (filmRecord == null || filmRecord.ID != id)
				return BadRequest();
			
			_repositoryWrapper.FilmRecord.Update(filmRecord);
			await _repositoryWrapper.SaveAsync();

			return NoContent();
		}

		[HttpDelete("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> DeleteFilmRecord(int userId, int id)
		{
			if (await UserDoesNotExist(userId))
				return NotFound();

			var film = await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(userId, id);

			if (film == null)
				return NotFound();

			_repositoryWrapper.FilmRecord.Delete(film);
			await _repositoryWrapper.SaveAsync();

			return film;
		}

		private async Task<bool> UserDoesNotExist(int userId)
		{
			var user = await _repositoryWrapper.User.GetUserByIdAsync(userId);
			return user == null;
		}
	}
}
