﻿using FilmScanner.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmScanner.Contracts
{
    public interface IFilmRecordRepository : IRepositoryBase<FilmRecord>
    {
        Task<IEnumerable<FilmRecord>> GetAllFilmRecordsForUserAsync(int userId);
        Task<FilmRecord> GetFilmRecordForUserByIdAsync(Guid userId, int id);
    }
}