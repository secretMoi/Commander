using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
	// dbcontext permet de manipuler une bdd avec entity framework
	public class CommanderContext : DbContext
	{
		public CommanderContext(DbContextOptions<CommanderContext> options) : base(options)
		{
			
		}

		public DbSet<Command> Commands { get; set; } // map entity framework avec nos modèles
	}
}
