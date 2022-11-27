using System.Threading.Tasks;
using Omdb.ServicesLibs.Models;

namespace Omdb.ServicesLibs.Services
{
    public interface IOmdbService
    {
        Task<SearchResultModel> GetSearchResultsBasedOnSearchCritera(string searchCritera, int? page);
        Task<FilmModel> GetFilmBasedOnImdbId(string imdbId);
    }
}
