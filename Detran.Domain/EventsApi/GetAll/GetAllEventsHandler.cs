using Detran.Infrastructure.Models.NodeApi;
using Detran.Shared.Configurations;
using Detran.Shared.Services.HttpMethods;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Detran.Domain.EventsApi.GetAll
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsInput, List<Events>>
    {
        public IConfiguration Configuration { get; }
        public EventsApiTokenService TokenService { get; }

        private const string ENDPOINT = "/events";
        public GetAllEventsHandler(IConfiguration configuration, EventsApiTokenService tokenService)
        {
            Configuration = configuration;
            TokenService = tokenService;
            var config = Configuration.GetSection("EventsApiConfig").Get<EventsConfig>();

            TokenService.BaseAddress = config.Url;
        }

        public async Task<List<Events>> Handle(GetAllEventsInput request, CancellationToken cancellationToken)
        {
            var client = new HttpClientService<GetAllEventsInput, List<Events>, EventsApiTokenService, Guid>();
            client.Init(TokenService.BaseAddress + ENDPOINT,TokenService, ignoreHeaders: true);
            var response = await client.Get();


            return response;
        }
    }
}
