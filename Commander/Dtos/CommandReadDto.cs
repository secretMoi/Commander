using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
	// copié/collé de la classe command
	// on retire les annotations car inutiles, on ne va pas dialoguer avec la bdd
	public class CommandReadDto
	{
		public int Id { get; set; }

		public string HowTo { get; set; }

		public string Line { get; set; } // command line to store in database
	}
}
