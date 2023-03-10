using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
			return await FindByCondition(f => f.Id == userId).ToListAsync();
		}

		public async Task<FilmRecord> GetFilmRecordForUserByIdAsync(Guid userId, int id)
		{
			return await FindByCondition(f => f.UserId == userId && f.Id == id).FirstOrDefaultAsync();
		}
	}
}