using System.Threading.Tasks;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Omdb.Interfaces;

namespace Omdb.ServicesLibs.Services
{
    public class OmdbService : IOmdbService
    {
        private readonly IGetSearchResults _getSearchResults;
        private readonly IGetFilmById _getFilmById;

        public OmdbService(IGetSearchResults getSearchResults, IGetFilmById getFlmById)
        {
            _getSearchResults = getSearchResults;
            _getFilmById = getFlmById;
        }

        public async Task<SearchResultModel> GetSearchResultsBasedOnSearchCritera(string searchCritera, int? page)
        {
            return await _getSearchResults.ReturnSearchResults(searchCritera, page);
        }

        public async Task<FilmModel> GetFilmBasedOnImdbId(string imdbId)
        {
            return await _getFilmById.ReturnFilm(imdbId);
        }
    }
}
