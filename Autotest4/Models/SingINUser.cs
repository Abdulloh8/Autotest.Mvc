using System.ComponentModel.DataAnnotations;

namespace Autotest4.Models;

public class SingINUser
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
