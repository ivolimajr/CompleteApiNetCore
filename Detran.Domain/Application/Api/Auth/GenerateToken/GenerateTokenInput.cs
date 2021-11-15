using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Detran.Domain.Application.Api.Auth.GenerateToken
{
    public class GenerateTokenInput : IRequest<GenerateTokenResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}