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

			var users = new[]
			{
				new User{UserName="User1",Password="Password1",CreatedAt=DateTime.Parse("2020-02-17")},
				new User{UserName="User2",Password="Password2",CreatedAt=DateTime.Parse("2020-02-18")},
				new User{UserName="User3",Password="Password3",CreatedAt=DateTime.Parse("2020-02-19")}
			};
			foreach (var user in users)
			{
				context.Users.Add(user);
			}
			context.SaveChanges();

			var films = new[]
			{
				new Film{ExternalID="tt0316654", CreatedAt=DateTime.Parse("2020-02-17")},
				new Film{ExternalID="tt0948470", CreatedAt=DateTime.Parse("2020-02-18")},
				new Film{ExternalID="tt0145487", CreatedAt=DateTime.Parse("2020-02-19")}
			};
			foreach (var film in films)
			{
				context.Films.Add(film);
			}
			context.SaveChanges();
		}
	}
}
