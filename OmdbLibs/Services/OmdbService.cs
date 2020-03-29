using System.Threading.Tasks;
using OmdbServicesLibs.Models;
using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibs.Services
{
	public class OmdbService : IOmdbService
	{
		private readonly IGetSearchResults _GetSearchResults;
		private readonly IGetFilmById _GetFilmById;

		public OmdbService(IGetSearchResults getSearchResults, IGetFilmById getFlmById)
		{
			_GetSearchResults = getSearchResults;
			_GetFilmById = getFlmById;
		}

		public async Task<SearchResultModel> GetSearchResultsBasedOnSearchCritera(string searchCritera, int? page)
		{
			return await _GetSearchResults.ReturnSearchResults(searchCritera, page);
		}

		public async Task<FilmModel> GetFilmBasedOnImdbId(string imdbId)
		{
			return await _GetFilmById.ReturnFilm(imdbId);
		}
	}
}
