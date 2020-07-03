using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
	public class CommandUpdateDto
	{
		// pas besoin d'id vu qu'il est géré par la bdd

		[Required]
		[MaxLength(250)]
		public string HowTo { get; set; }

		[Required]
		public string Line { get; set; } // command line to store in database

		[Required]
		public string Platform { get; set; } // application cliente
	}
}
