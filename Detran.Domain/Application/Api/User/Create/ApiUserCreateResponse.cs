using Detran.Infrastructure.Entity;
using System.Collections.Generic;

namespace Detran.Domain.Application.Api.User.Create
{
    public class ApiUserCreateResponse
    {
        public ApiUserCreateResponse()
        {
            this.Roles = new List<ApiUserRole>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<ApiUserRole> Roles { get; set; }
    }
}
