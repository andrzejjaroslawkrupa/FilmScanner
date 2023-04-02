using FilmScanner.Contracts;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmScanner.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class FilmRecordsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;

        public FilmRecordsController(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
        }

        [HttpGet("{userId}/records")]
        public async Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUser(int userId)
        {
            return await _repositoryWrapper.FilmRecord.GetAllFilmRecordsForUserAsync(userId);
        }

        [HttpGet("{userId}/records/{id}")]
        public async Task<ActionResult<FilmRecord>> GetFilmRecordForUser(string userId, int id)
        {
            if (await UserDoesNotExist(userId))
                return NotFound();

            var record = await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(Guid.Parse(userId), id);

            if (record == null)
                return NotFound();

            return record;
        }

        [HttpPost("{userId}/records")]
        public async Task<ActionResult<FilmRecord>> CreateFilmRecord(string userId, FilmRecord film)
        {
            if (film == null)
                return BadRequest();

            if (await UserDoesNotExist(userId))
                return NotFound();

            _repositoryWrapper.FilmRecord.Create(film);
            await _repositoryWrapper.SaveAsync();

            return CreatedAtAction("CreateFilmRecord", new { id = film.Id }, film);
        }

        [HttpPut("{userId}/records/{id}")]
        public async Task<IActionResult> UpdateFilmRecord(string userId, int id, FilmRecord filmRecord)
        {
            if (await UserDoesNotExist(userId))
                return NotFound();

            if (filmRecord == null || filmRecord.Id != id)
                return BadRequest();

            _repositoryWrapper.FilmRecord.Update(filmRecord);
            await _repositoryWrapper.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{userId}/records/{id}")]
        public async Task<ActionResult<FilmRecord>> DeleteFilmRecord(string userId, int id)
        {
            if (await UserDoesNotExist(userId))
                return NotFound();

            var film = await _repositoryWrapper.FilmRecord.GetFilmRecordForUserByIdAsync(Guid.Parse(userId), id);

            if (film == null)
                return NotFound();

            _repositoryWrapper.FilmRecord.Delete(film);
            await _repositoryWrapper.SaveAsync();

            return film;
        }

        private async Task<bool> UserDoesNotExist(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user == null;
        }
    }
}
