using System.Threading.Tasks;
using Omdb.ServicesLibs.Models;

namespace Omdb.ServicesLibs.Omdb.Interfaces
{
    public interface IGetFilmById
    {
        Task<FilmModel> ReturnFilm(string imdbId);
    }
}