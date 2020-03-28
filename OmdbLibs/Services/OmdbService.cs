using System.Threading.Tasks;
using OmdbServicesLibs.Models;
using OmdbServicesLibs.Omdb;

namespace OmdbServicesLibs.Services
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
