using Microsoft.AspNetCore.Mvc;
using Omdb.ServicesLibs.Services;

namespace Omdb.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IOmdbService _OmdbService;
        public FilmsController(IOmdbService omdbService)
        {
            _OmdbService = omdbService;
        }

        [HttpGet("{searchCritera}")]
        [HttpGet("{searchCritera}/{page}")]
        public async Task<IActionResult> Search(string searchCritera, int? page = null)
        {
            var result = await _OmdbService.GetSearchResultsBasedOnSearchCritera(searchCritera, page);

            return Ok(result);
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> Film(string imdbId)
        {
            var result = await _OmdbService.GetFilmBasedOnImdbId(imdbId);

            return Ok(result);
        }
    }
}
