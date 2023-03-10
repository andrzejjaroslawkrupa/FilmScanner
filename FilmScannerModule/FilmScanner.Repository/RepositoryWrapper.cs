using FilmScanner.Contracts;
using FilmScanner.Entities;
using System.Threading.Tasks;

namespace FilmScanner.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private readonly RepositoryContext _repositoryContext;
		private IFilmRecordRepository _film;

		public IFilmRecordRepository FilmRecord => _film ??= new FilmRecordRepository(_repositoryContext);

        public RepositoryWrapper(RepositoryContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
		}

		public async Task SaveAsync()
		{
			await _repositoryContext.SaveChangesAsync();
		}
	}
}