using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmdbServicesLibs.Services;

namespace FilmScanner.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OmdbController : ControllerBase
	{
		private readonly IOmdbService _OmdbService;
		public OmdbController(IOmdbService omdbService)
		{
			_OmdbService = omdbService;
		}

		[HttpGet]
		[Route("api/films/search/{searchCritera}")]
		public async Task<IActionResult> SearchForAFilm(string searchCritera)
		{
			var result = await _OmdbService.GetSearchResultsBasedOnSearchCritera(searchCritera);

			return Ok(result);
		}

	}
}