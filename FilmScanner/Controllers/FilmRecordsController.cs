using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		[HttpGet("{userId}/records")]
		public async Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUser(int userId)
		{
			return await _repositoryWrapper.FilmRecord.GetAllFilmRecordsForUserAsync(userId);
		}

		[HttpGet("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> GetFilmRecordForUser(int userId, int id)
		{
			return await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(userId, id);
		}

		[HttpPut("{userId}/records/{id}")]
		public async Task<IActionResult> PutFilmRecord(int id, FilmRecord filmRecord)
		{
			if (filmRecord.ID != id)
			{
				return BadRequest();
			}
			_repositoryWrapper.FilmRecord.Update(filmRecord);
			await _repositoryWrapper.SaveAsync();

			return NoContent();
		}

		[HttpPost("{userId}/records")]
		public async Task<ActionResult<FilmRecord>> PostFilmRecord(int userId, FilmRecord film)
		{
			if (await UserDoesNotExist(userId))
			{
				return NotFound();
			}

			_repositoryWrapper.FilmRecord.Create(film);
			await _repositoryWrapper.SaveAsync();

			return CreatedAtAction("PostFilmRecord", new { id = film.ID }, film);
		}

		[HttpDelete("{userId}/records/{id}")]
		public async Task<ActionResult<FilmRecord>> DeleteFilmRecord(int userId, int id)
		{
			if (await UserDoesNotExist(userId))
			{
				return NotFound();
			}

			var film = await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(userId, id);
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
