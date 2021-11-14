using System.ComponentModel.DataAnnotations;

namespace Detran.Api
{
    public class RoleInput
    {
        [Required]
        [MaxLength(5)]
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
