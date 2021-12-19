using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        public void ProcessEvent(string message)
        {
            throw new NotImplementedException();
        }
    }

    enum EventType

    {
        PlatformPublished,
        Undetermined
    }

}