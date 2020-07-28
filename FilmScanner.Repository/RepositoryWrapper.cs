using FilmScanner.Contracts;
using FilmScanner.Entities;

namespace FilmScanner.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private readonly RepositoryContext _repositoryContext;
		private IFilmRepository _film;
		private IUserRepository _user;

		public IFilmRepository Film => _film ??= new FilmRepository(_repositoryContext);
		public IUserRepository User => _user ??= new UserRepository(_repositoryContext);
		public RepositoryWrapper(RepositoryContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
		}

		public void Save()
		{
			_repositoryContext.SaveChanges();
		}
	}
}