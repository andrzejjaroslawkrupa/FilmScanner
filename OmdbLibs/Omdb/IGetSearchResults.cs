using System.Threading.Tasks;
using OmdbLibs.Models;

namespace OmdbLibs.Omdb
{
	public interface IGetSearchResults
	{
		Task<OmdbModel> ReturnSearchResults(string searchCritera);
	}
}