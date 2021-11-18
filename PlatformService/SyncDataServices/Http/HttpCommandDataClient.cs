using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            this.config = config;
            this.httpClient = httpClient;
            
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync($"{config["CommandService"]}", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("Sync POST to CommandService was NOT OK!");
            }
        }
    }
}