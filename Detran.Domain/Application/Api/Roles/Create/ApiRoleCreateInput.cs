using Detran.Infrastructure.Entity;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Detran.Domain.Application.Api.Roles.Create
{
    public class ApiRoleCreateInput : IRequest<ApiUserRole>
    {
        [Required]
        [MaxLength(5)]
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
