using FilmScanner.Models;
using System;
using System.Linq;

namespace FilmScanner.Data
{
	public static class DbInitializer
	{
		public static void Initialize(UserContext context)
		{
			context.Database.EnsureCreated();

			// Look for any users.
			if (context.Users.Any())
			{
				return;   // DB has been seeded
			}

			var users = new User[]
			{
				new User{UserName="Kaszub1",Password="Kaszub1",CreatedAt=DateTime.Parse("2020-02-17")},
				new User{UserName="Kaszub2",Password="Kaszub2",CreatedAt=DateTime.Parse("2020-02-18")},
				new User{UserName="Kaszub3",Password="Kaszub3",CreatedAt=DateTime.Parse("2020-02-19")}
			};
			foreach (User u in users)
			{
				context.Users.Add(u);
			}
			context.SaveChanges();

			var films = new Film[]
			{
				new Film{ExternalID="FC1", CreatedAt=DateTime.Parse("2020-02-17")},
				new Film{ExternalID="FC2", CreatedAt=DateTime.Parse("2020-02-18")},
				new Film{ExternalID="FC3", CreatedAt=DateTime.Parse("2020-02-19")}
			};
			foreach (Film f in films)
			{
				context.Films.Add(f);
			}
			context.SaveChanges();
		}
	}
}
