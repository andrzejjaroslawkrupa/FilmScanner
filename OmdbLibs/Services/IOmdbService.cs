using System.Threading.Tasks;
using OmdbLibs.Models;

namespace OmdbLibs.Services
{
	public interface IOmdbService
	{
		Task<OmdbModel> GetSearchResultsBasedOnSearchCritera(string searchCritera);
	}
}
