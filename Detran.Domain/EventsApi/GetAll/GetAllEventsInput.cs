using Detran.Infrastructure.Models.NodeApi;
using MediatR;
using System.Collections.Generic;

namespace Detran.Domain.EventsApi.GetAll
{
    public class GetAllEventsInput : IRequest<List<Events>>
    {
    }
}
