using FilmScanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmScanner.Data
{
	public class UserContext : DbContext
	{
		public UserContext()
		{
		}

		public UserContext(DbContextOptions<UserContext> options)
		  : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Film> Films { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}
	}
}
