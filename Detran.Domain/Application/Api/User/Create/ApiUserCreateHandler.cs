using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Detran.Infrastructure.Entity;
using Detran.Infrastructure.Repository;
using Detran.Shared.Helpers;

namespace Detran.Domain.Application.Api.User.Create
{
    public class ApiUserCreateHandler : IRequestHandler<ApiUserCreateInput, ApiUserCreateResponse>
    {
        private readonly IRepository<ApiUserModel> _authUserRepository;

        public ApiUserCreateHandler(IRepository<ApiUserModel> authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }
        public async Task<ApiUserCreateResponse> Handle(ApiUserCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _authUserRepository.CreateTransaction();

                var userExists = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (userExists != null) throw new HttpClientCustomException("Usuário já cadastrado");

                var roles = new List<ApiUserRole>();

                for (int index = 0; index < request.Roles.Count; index++)
                {
                    var result = _authUserRepository.Context.Set<ApiUserRole>().Where(e => e.Role == request.Roles[index]).FirstOrDefault();
                    if (result == null) throw new HttpClientCustomException("Role inválida");
                    roles.Add(result);
                }

                var AuthUserModel = new ApiUserModel()
                {
                    Name = request.Name.ToUpper(),
                    UserName = request.UserName.ToUpper(),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = roles,
                    CreatedAt = DateTime.Now,
                };

                var authUser = await _authUserRepository.CreateAsync(AuthUserModel);

                _authUserRepository.Commit();
                _authUserRepository.Save();

                return new ApiUserCreateResponse
                {
                    Id = authUser.Id,
                    Name = authUser.Name,
                    UserName = authUser.UserName,
                    Roles = roles
                };
            }
            catch (Exception e)
            {
                _authUserRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _authUserRepository.Context.Dispose();
            }
        }
    }
}
