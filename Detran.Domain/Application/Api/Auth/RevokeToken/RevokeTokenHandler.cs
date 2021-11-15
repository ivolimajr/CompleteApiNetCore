using Detran.Infrastructure.Entity;
using Detran.Infrastructure.Repository;
using Detran.Shared.Helpers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Detran.Domain.Application.Api.Auth.RevokeToken
{
    class RevokeTokenHandler : IRequestHandler<RevokeTokenInput, string>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public RevokeTokenHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }

        public async Task<string> Handle(RevokeTokenInput request, CancellationToken cancellationToken)
        {
            if (request.UserName == string.Empty) throw new HttpClientCustomException("Parâmetros Inválidos");

            try
            {
                ApiUserModel authUser = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (authUser == null) throw new HttpClientCustomException("Credenciais Não encontrada");

                ApiUserModel usuario = await _authUserRepository.GetByIdAsync(authUser.Id);

                usuario.RefreshToken = null;
                _authUserRepository.Update(usuario);
                await _authUserRepository.SaveChangesAsync();
                return "Usuário da Api Removido";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
