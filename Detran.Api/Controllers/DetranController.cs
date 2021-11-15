using Detran.Domain.Application.Api.Roles.Create;
using Detran.Domain.CredPay.ConsultDebits;
using Detran.Infrastructure.Entity;
using Detran.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public class DetranController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DetranController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        /// <summary>
        /// Cria uma nova Role
        /// </summary>
        /// <param name="request">Role</param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ConsultDebitsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromBody] ConsultDebitsInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
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
