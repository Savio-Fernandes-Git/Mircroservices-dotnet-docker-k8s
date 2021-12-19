using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;
        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform for PlatformId : {platformId}");

            if( !repository.PlatformExists(platformId) ) return NotFound();

            var commands = repository.GetCommandsForPlatform(platformId);

            return Ok( mapper.Map<IEnumerable<CommandReadDto>>(commands) );
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform for PlatformId & CommandId : {platformId} : {commandId}");

            if( !repository.PlatformExists(platformId) ) return NotFound();

            var command = repository.GetCommand(platformId, commandId);

            if ( command == null ) return NotFound();

            return Ok( mapper.Map<CommandReadDto>(command) );
        }

        [HttpPost]
        public ActionResult<CommandCreateDto> CreateCommand(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform for PlatformId: {platformId}");

            if( !repository.PlatformExists(platformId) ) return NotFound();

            var command = mapper.Map<Command>(commandCreateDto);

            repository.CreateCommand(platformId,command);
            repository.SaveChanges();

            var commandReadDto = mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute( nameof(GetCommandForPlatform), new {platformId = platformId, commandId = commandReadDto.Id}, commandReadDto );
        }
    }
}