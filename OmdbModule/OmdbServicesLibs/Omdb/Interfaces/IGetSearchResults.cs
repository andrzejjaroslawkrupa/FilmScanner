using System.Threading.Tasks;
using Omdb.ServicesLibs.Models;

namespace Omdb.ServicesLibs.Omdb.Interfaces
{
    public interface IGetSearchResults
    {
        Task<SearchResultModel> ReturnSearchResults(string searchCritera, int? page);
    }
}