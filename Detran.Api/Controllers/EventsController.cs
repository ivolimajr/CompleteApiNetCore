using Detran.Domain.CredPay.ConsultDebits;
using Detran.Domain.EventsApi.GetAll;
using Detran.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Detran.Api.Controllers
{
    /// <summary>
    /// Cria novas roles para usuários da API
    /// </summary>
    [ApiController]
    //[Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ConsultDebitsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(new GetAllEventsInput()));
            }
            catch (HttpClientCustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
