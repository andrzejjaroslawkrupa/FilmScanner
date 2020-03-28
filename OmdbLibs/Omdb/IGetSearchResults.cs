using System.Threading.Tasks;
using OmdbServicesLibs.Models;

namespace OmdbServicesLibs.Omdb
{
	public interface IGetSearchResults
	{
		Task<OmdbModel> ReturnSearchResults(string searchCritera);
	}
}