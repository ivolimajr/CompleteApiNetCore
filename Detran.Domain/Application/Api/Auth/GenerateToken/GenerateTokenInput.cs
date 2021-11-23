using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Detran.Domain.Application.Api.Auth.GenerateToken
{
    /// <summary>
    /// Data to generate access token to use API
    /// </summary>
    public class GenerateTokenInput : IRequest<GenerateTokenResponse>
    {
        // User e-mail
        [Required(ErrorMessage ="Campo necessário")]
        [EmailAddress(ErrorMessage = "Campo do tipo email")]
        [MaxLength(150)]
        public string UserName { get; set; }

        // User password unencrypted
        [Required(ErrorMessage = "Campo necessário")]
        [MaxLength(150)]
        public string Password { get; set; }
    }
}