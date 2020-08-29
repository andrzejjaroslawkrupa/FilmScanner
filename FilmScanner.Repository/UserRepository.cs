using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Repository
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await FindByCondition(u => u.ID == id).FirstOrDefaultAsync();
		}
	}
}