using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Detran.Infrastructure.Entity
{
    public class ApiUserRole
    {
        public ApiUserRole()
        {
            this.Users = new List<ApiUserModel>();
        }
        public int Id { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<ApiUserModel> Users { get; set; }
    }
}
