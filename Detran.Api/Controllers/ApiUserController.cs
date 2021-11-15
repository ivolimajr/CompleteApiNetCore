using Detran.Domain.Application.Api.User.Create;
using Detran.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Detran.Api.Controllers
{
    /// <summary>
    /// Endpoints para criar um usuário para consumir a API
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria o usuário
        /// </summary>
        /// <param name="request">Dados do usuario</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiUserCreateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ValueTask<ActionResult> Post([FromBody] ApiUserCreateInput request)
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
