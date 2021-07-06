using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmScanner.Entities.Models
{
	public class FilmRecord
	{
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }

		[Required]
		public string ExternalID { get; set; }

		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
