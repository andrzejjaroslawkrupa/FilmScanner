using System;
using System.ComponentModel.DataAnnotations;

namespace FilmScanner.Models
{
	public class Film
	{
		public int ID { get; set; }
		public string ExternalID { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
