using System.Threading.Tasks;
using OmdbLibs.Models;
using OmdbLibs.Omdb;

namespace OmdbLibs.Services
{
	public class OmdbService : IOmdbService
	{
		private readonly IGetSearchResults _GetSearchResults;

		public OmdbService(IGetSearchResults getSearchResults)
		{
			_GetSearchResults = getSearchResults;
		}

		public async Task<OmdbModel> GetSearchResultsBasedOnSearchCritera(string searchCritera)
		{
			return await _GetSearchResults.ReturnSearchResults(searchCritera);
		}
	}
}
