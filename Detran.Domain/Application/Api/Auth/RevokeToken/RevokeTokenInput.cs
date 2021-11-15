using MediatR;

namespace Detran.Domain.Application.Api.Auth.RevokeToken
{
    public class RevokeTokenInput : IRequest<string>
    {
        public string UserName { get; set; }
    }
}
