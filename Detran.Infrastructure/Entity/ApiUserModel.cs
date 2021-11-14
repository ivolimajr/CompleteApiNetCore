using System;
using System.Collections.Generic;

namespace Detran.Infrastructure.Entity
{
    public class ApiUserModel
    {
        public ApiUserModel()
        {
            this.Roles = new List<ApiUserRole>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public List<ApiUserRole> Roles { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateddAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}