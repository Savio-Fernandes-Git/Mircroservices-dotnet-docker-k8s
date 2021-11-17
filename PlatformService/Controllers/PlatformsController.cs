using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repository;
        private readonly IMapper mapper;
        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto model)
        {
            var platformModel = mapper.Map<Platform>(model);
            repository.CreatePlatform(platformModel);

            repository.SaveChanges();

            var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);
            
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