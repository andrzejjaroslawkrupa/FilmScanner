using FilmScanner.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Contracts
{
	public interface IUserRepository : IRepositoryBase<User>
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> GetUserByIdAsync(int id);
	}
}