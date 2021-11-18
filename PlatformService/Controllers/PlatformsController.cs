using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repository;
        private readonly IMapper mapper;
        private readonly ICommandDataClient commandDataClient;

        public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient
            )
        {
            this.mapper = mapper;
            this.commandDataClient = commandDataClient;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto model)
        {
            var platformModel = mapper.Map<Platform>(model);
            repository.CreatePlatform(platformModel);

            repository.SaveChanges();

            var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);
            
            try
            {
                await commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not send synchronously: {ex}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("Getting Platforms...");

            var platformItem = repository.GetAllPlatforms();
            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet( "{id}", Name = "GetPlatformById" )]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatformById(int id)
        {
            Console.WriteLine("Getting Platforms...");

            var platformItem = repository.GetPlatformById(id);
            
            if(platformItem == null) return NotFound();

            return Ok(mapper.Map<PlatformReadDto>(platformItem));
        }
    }
}