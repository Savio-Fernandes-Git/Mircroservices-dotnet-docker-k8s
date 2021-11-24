using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> target
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<Command,CommandReadDto>();
            CreateMap<CommandCreateDto,Command>();
        }
    }
}