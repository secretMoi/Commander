using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
	// repo de test
	public class MockCommanderRepo : ICommanderRepo
	{
		public bool SaveChanges()
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Command> GetAllCommands()
		{
			List<Command> commands = new List<Command>
			{
				new Command
				{
					Id = 0,
					HowTo = "Boil an egg",
					Line = "Boil water",
					Platform = "Kettle & pan"
				},

				new Command
				{
					Id = 0,
					HowTo = "Cut bread",
					Line = "get a knife",
					Platform = "knife & chopping board"
				},

				new Command
				{
					Id = 0,
					HowTo = "Make a cup of tea",
					Line = "Place teabag in cup",
					Platform = "Kettle & cup"
				}
			};

			return commands;
		}

		public Command GetCommandById(int id)
		{
			return new Command
			{
				Id = 0,
				HowTo = "Boil an egg",
				Line = "Boil water",
				Platform = "Kettle & pan"
			};
		}

		public void CreateCommand(Command command)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateCommand(Command command)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteCommand(Command command)
		{
			throw new System.NotImplementedException();
		}
	}
}
