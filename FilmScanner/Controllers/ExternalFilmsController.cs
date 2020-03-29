using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmdbServicesLibs.Services;

namespace FilmScanner.Controllers
{
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class ExternalFilmsController : ControllerBase
	{
		private readonly IOmdbService _OmdbService;
		public ExternalFilmsController(IOmdbService omdbService)
		{
			_OmdbService = omdbService;
		}

		[HttpGet("{searchCritera}")]
		public async Task<IActionResult> Search(string searchCritera)
		{
			var result = await _OmdbService.GetSearchResultsBasedOnSearchCritera(searchCritera);

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
