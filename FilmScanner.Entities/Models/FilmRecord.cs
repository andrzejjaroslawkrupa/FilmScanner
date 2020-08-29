using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmScanner.Entities.Models
{
	public class FilmRecord
	{
		[Required]
		public int ID { get; set; }

		[Required]
		[ForeignKey("User")]
		public int UserRefID { get; set; }

		[Required]
		public string ExternalID { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
