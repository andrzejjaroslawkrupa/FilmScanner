using FilmScanner.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmScanner.Entities
{
	public class RepositoryContext : DbContext
	{
		public RepositoryContext()
		{
		}

		public RepositoryContext(DbContextOptions<RepositoryContext> options)
		  : base(options) 
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<FilmRecord> FilmsRecords { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}
	}
}
