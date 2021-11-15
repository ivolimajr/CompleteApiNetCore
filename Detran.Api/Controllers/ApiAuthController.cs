using Detran.Domain.Application.Api.Auth.GenerateToken;
using Detran.Domain.Application.Api.Auth.RefreshToken;
using Detran.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Detran.Api.Controllers
{
    /// <summary>
    /// Endpoints para obter o token e refreshToken
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obter o token para consumo da API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o Token bearer</response>
        /// <response code="401">Provavelmente você não tem acesso a API</response>
        [HttpPost]
        [Route("getToken")]
        [ProducesResponseType(typeof(GenerateTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult> GetToken([FromBody] GenerateTokenInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
            }
            catch (HttpClientCustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualizar o Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Atualiza o Token</response>
        /// <response code="401">Credenciais Inválidas</response>
        [HttpPost]
        [Route("refresh")]
        [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async ValueTask<ActionResult> Refresh([FromBody] RefreshTokenInput request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _mediator.Send(request));
            }
            catch (HttpClientCustomException e)
            {
                return Problem(detail: e.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
