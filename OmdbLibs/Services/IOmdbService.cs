using System.Threading.Tasks;
using OmdbServicesLibs.Models;

namespace OmdbServicesLibs.Services
{
	public interface IOmdbService
	{
		Task<SearchResultModel> GetSearchResultsBasedOnSearchCritera(string searchCritera);
		Task<FilmModel> GetFilmBasedOnImdbId(string imdbId);
	}
}
