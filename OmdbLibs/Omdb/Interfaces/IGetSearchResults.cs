using System.Threading.Tasks;
using OmdbServicesLibs.Models;

namespace OmdbServicesLibs.Omdb.Interfaces
{
	public interface IGetSearchResults
	{
		Task<SearchResultModel> ReturnSearchResults(string searchCritera);
	}
}