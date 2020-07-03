using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
	// hérite de la classe profile qui gère l'auto map
	public class CommandsProfile : Profile
	{
		public CommandsProfile()
		{
			// arg 1 from, arg 2 To
			// source -> target
			CreateMap<Command, CommandReadDto>();
			CreateMap<CommandCreateDto, Command>();
			CreateMap<CommandUpdateDto, Command>();
			CreateMap<Command, CommandUpdateDto>();
		}
	}
}
