using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
	public class Command
	{
		[Key]
		public int Id { get; set; }

		[Required] // not null
		[MaxLength(250)]
		public string HowTo { get; set; }

		[Required]
		public string Line { get; set; } // command line to store in database

		[Required]
		public string Platform { get; set; } // application cliente
	}
}
