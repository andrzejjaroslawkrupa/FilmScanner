using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Repository
{
	public class FilmRecordRepository : RepositoryBase<FilmRecord>, IFilmRecordRepository
	{
		public FilmRecordRepository(RepositoryContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public async Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUserAsync(int userId)
		{
			return await FindByCondition(f => f.ID == userId).ToListAsync();
		}

		public async Task<FilmRecord> GetFilmRecordForUserByIdAsync(int userId, int id)
		{
			return await FindByCondition(f => f.UserId == userId && f.ID == id).FirstOrDefaultAsync();
		}
	}
}