using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;

namespace FilmScanner.Repository
{
	public class FilmRepository : RepositoryBase<FilmRecord>, IFilmRepository
	{
		public FilmRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}