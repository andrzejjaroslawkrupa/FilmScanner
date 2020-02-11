using System;
using System.ComponentModel.DataAnnotations;

namespace FilmScanner.Models
{
	public class FilmsList
	{
		public int Id { get; set; }
		public int ExternalId { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
