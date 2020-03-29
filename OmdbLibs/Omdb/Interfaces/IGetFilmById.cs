using System.Threading.Tasks;
using OmdbServicesLibs.Models;

namespace OmdbServicesLibs.Omdb.Interfaces
{
	public interface IGetFilmById
	{
		Task<FilmModel> ReturnFilm(string imdbId);
	}
}