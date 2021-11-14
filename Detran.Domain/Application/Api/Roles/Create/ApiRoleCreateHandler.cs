using Detran.Infrastructure.Entity;
using Detran.Infrastructure.Repository;
using Detran.Shared.Helpers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Detran.Domain.Application.Api.Roles.Create
{
    public class ApiRoleCreateHandler : IRequestHandler<ApiRoleCreateInput, ApiUserRole>
    {
        private readonly IRepository<ApiUserRole> _apiRoleRepository;
        public ApiRoleCreateHandler(IRepository<ApiUserRole> apiRoleRepository)
        {
            _apiRoleRepository = apiRoleRepository;
        }

        public async Task<ApiUserRole> Handle(ApiRoleCreateInput request, CancellationToken cancellationToken)
        {
            try
            {
                _apiRoleRepository.CreateTransaction();
                var res = _apiRoleRepository.Find(e => e.Role == request.Role);
                if (res != null) throw new HttpClientCustomException("Role already exists");

                var role = new ApiUserRole()
                {
                    Role = request.Role.ToUpper(),
                    Description = request.Description.ToUpper()

                };

                var result = await _apiRoleRepository.CreateAsync(role);
                _apiRoleRepository.Commit();
                _apiRoleRepository.Save();

                return result;
            }
            catch (Exception e)
            {
                _apiRoleRepository.Rollback();
                throw;
            }

        }
    }
}
