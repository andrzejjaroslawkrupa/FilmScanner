using System;
using System.ComponentModel.DataAnnotations;

namespace FilmScanner.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
