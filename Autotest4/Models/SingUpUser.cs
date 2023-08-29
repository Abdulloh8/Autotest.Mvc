using System.ComponentModel.DataAnnotations;

namespace Autotest4.Models
{
    public class SingUpUser
    {
        [Required]
        [MaxLength(12)]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(12)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
