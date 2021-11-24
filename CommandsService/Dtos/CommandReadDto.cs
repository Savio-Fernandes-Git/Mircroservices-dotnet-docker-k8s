using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        
        public string Howto { get; set; }
        
        public string CommandLine { get; set; }
        
        public int PlatformId { get; set; } //foreign key reference
    }
}