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
            var result = await _OmdbService.GetSearchResultsBasedOnSearchCritera(DecodeSlashes(searchCritera), page);

            return Ok(result);
        }

        [HttpGet("{imdbId}")]
        public async Task<IActionResult> Film(string imdbId)
        {
            var result = await _OmdbService.GetFilmBasedOnImdbId(DecodeSlashes(imdbId));

            return Ok(result);
        }

        // Kestrel decodes the request path except for '%2F', so a route value for e.g. "AC/DC"
        // (sent as "AC%2FDC") still contains the escape. Decode it so the value is the original text.
        internal static string DecodeSlashes(string routeValue) =>
            routeValue.Replace("%2F", "/", StringComparison.OrdinalIgnoreCase);
    }
}
