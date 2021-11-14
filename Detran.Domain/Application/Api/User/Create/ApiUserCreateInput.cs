using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Detran.Domain.Application.Api.User.Create
{
    public class ApiUserCreateInput : IRequest<ApiUserCreateResponse>
    {
        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> Roles { get; set; }
    }
}
