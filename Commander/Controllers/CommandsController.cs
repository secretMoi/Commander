using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
	// controllerbase, classe de base de aspnetcore qui supporte uniquement model et controller
	// controller, classe de base de aspnetcore qui supporte model et controller + vue
	// on a pas de vue ici

	//[Route("api/[controller]")] // => /api/commands (nom de la classe sans controller)
	[Route("api/commands")] // permet de renommer la classe sans que les clients ne soient impactés
	[ApiController] // indique que cette classe est un controller api
	public class CommandsController : ControllerBase
	{
		//private readonly MockCommanderRepo _repo = new MockCommanderRepo();
		private readonly ICommanderRepo _repository; // repo sur lequel le controller va travailler
		private readonly IMapper _mapper;

		public CommandsController(ICommanderRepo repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		// GET api/commands
		[HttpGet] // indique que cette méthode répond à une requete http
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			var commandItems = _repository.GetAllCommands();

			// mapper, mets l'objet commandItems dans CommandReadDto
			return Ok(_mapper.Map<IEnumerable<CommandReadDto> >(commandItems)); // méthode Ok définie dans controllerBase
		}

		// GET api/commands/5
		// GET api/commands/{id}
		[HttpGet("{id}", Name = "GetCommandById")] // indique que cette méthode répond à une requete http
		public ActionResult<CommandReadDto> GetCommandById(int id)
		{
			var commandItem = _repository.GetCommandById(id);

			if (commandItem != null)
				return Ok(_mapper.Map<CommandReadDto>(commandItem)); // map commandItem en CommandReadDto pour renvoyer les données formattées au client

			return NotFound(); // si pas trouvé renvoie 404 not found
		}

		// POST api/commands
		[HttpPost]
		public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
		{
			var commandModel = _mapper.Map<Command>(commandCreateDto); // trouve le model à utiliser
			_repository.CreateCommand(commandModel); // crée la command en ram
			_repository.SaveChanges(); // sauvegarde les changements dans la bdd

			// mapper, mets l'objet commandModel dans CommandReadDto
			var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

			// renvoie l'uri permettant d'accéder à l'élément créé
			// fonction gérant la route à appeler (GET api/commands/{id})
			// paramètre à passer à la route (l'id créé)
			// classe pour formatter les données
			return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
			//return Ok(commandReadDto);
		}

		// PUT api/commands/{id}
		[HttpPut("{id}")]
		public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
		{
			var commandModelFromRepo = _repository.GetCommandById(id); // cherche l'objet dans la bdd
			if (commandModelFromRepo == null)
				return NotFound(); // si il n'existe pas on quitte et envoie 404
			// met commandUpdateDto dans commandModelFromRepo
			_mapper.Map(commandUpdateDto, commandModelFromRepo);

			_repository.UpdateCommand(commandModelFromRepo); // update l'objet

			_repository.SaveChanges(); // sauvegarde l'état de l'objet dans la bdd

			return NoContent();
		}

		// PATCH api/commands/{id}
		[HttpPatch("{id}")]
		public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
		{
			var commandModelFromRepo = _repository.GetCommandById(id); // cherche l'objet dans la bdd
			if (commandModelFromRepo == null)
				return NotFound(); // si il n'existe pas on quitte et envoie 404

			var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo); // récupère les données du repo
			patchDocument.ApplyTo(commandToPatch, ModelState); // essaye de rentrer les données dans le model de la bdd

			if (!TryValidateModel(commandToPatch)) // si les données ne sont pas valides
				return ValidationProblem(ModelState); // retourne une erreur de validation

			_mapper.Map(commandToPatch, commandModelFromRepo); // injecte les données à patcher dans le repo

			_repository.UpdateCommand(commandModelFromRepo); // update l'objet

			_repository.SaveChanges(); // sauvegarde les changements

			return NoContent();
		}

		// DELETE api/commands/{id}
		[HttpDelete("{id}")]
		public ActionResult DeleteCommand(int id)
		{
			var commandModelFromRepo = _repository.GetCommandById(id); // cherche l'objet dans la bdd
			if (commandModelFromRepo == null)
				return NotFound(); // si il n'existe pas on quitte et envoie 404

			_repository.DeleteCommand(commandModelFromRepo); // supprime l'id dans l'objet
			_repository.SaveChanges(); // sauvegarde dans la bdd

			return NoContent();
		}
	}
}
