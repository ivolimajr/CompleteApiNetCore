using MediatR;

namespace Detran.Domain.Application.Api.Auth.RefreshToken
{
    public class RefreshTokenInput : IRequest<RefreshTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
