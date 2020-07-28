using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;

namespace FilmScanner.Repository
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}