using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FilmScanner.Entities
{
	public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";

        public RepositoryContext()
		{
		}

		public RepositoryContext(DbContextOptions<RepositoryContext> options)
		  : base(options) 
		{
		}

		public DbSet<FilmRecord> FilmsRecords { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            var connectionString = Environment.GetEnvironmentVariable(DB_CONNECTION_STRING);
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder.Entity<FilmRecord>()
				.HasOne(f => f.User)
				.WithMany(u => u.FilmRecords)
				.HasForeignKey(f => f.UserId);
        }
    }
}
