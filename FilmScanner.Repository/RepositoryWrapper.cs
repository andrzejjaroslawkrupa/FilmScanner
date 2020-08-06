using FilmScanner.Contracts;
using FilmScanner.Entities;
using System.Threading.Tasks;

namespace FilmScanner.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private readonly RepositoryContext _repositoryContext;
		private IFilmRecordRepository _film;
		private IUserRepository _user;

		public IFilmRecordRepository Film => _film ??= new FilmRecordRepository(_repositoryContext);
		public IUserRepository User => _user ??= new UserRepository(_repositoryContext);

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