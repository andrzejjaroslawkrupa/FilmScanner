using System.Threading.Tasks;
using OmdbServicesLibs.Models;

namespace OmdbServicesLibs.Services
{
	public interface IOmdbService
	{
		Task<OmdbModel> GetSearchResultsBasedOnSearchCritera(string searchCritera);
	}
}
