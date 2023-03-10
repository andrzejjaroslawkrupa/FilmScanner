using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmScanner.Entities.Models
{
	public class FilmRecord
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string ExternalID { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
