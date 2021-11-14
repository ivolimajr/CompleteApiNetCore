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
                var userExists = _authUserRepository.Find(u => u.UserName == request.UserName);
                if (userExists != null) throw new HttpClientCustomException("Usuário já cadastrado");

                var roles = new List<ApiUserRole>();

                for (int index = 0; index < request.Roles.Count; index++)
                {
                    var result = _authUserRepository.Context.Set<ApiUserRole>().Where(e => e.Role == request.Roles[index]).FirstOrDefault();
                    if (result != null)
                    {
                        roles.Add(result);
                    }
                    else
                    {
                        throw new HttpClientCustomException("Role inválida");
                    }
                }

                var AuthUserModel = new ApiUserModel()
                {
                    Name = request.Name.ToUpper(),
                    UserName = request.UserName.ToUpper(),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = roles,
                    CreatedAt = DateTime.Now,
                };

                _authUserRepository.CreateTransaction();
                var authUser = await _authUserRepository.CreateAsync(AuthUserModel);

                _authUserRepository.Commit();
                _authUserRepository.Save();

                return new ApiUserCreateResponse
                {
                    Id = authUser.Id,
                    Name = authUser.Name,
                    UserName = authUser.UserName
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
