using System.ComponentModel.DataAnnotations;

namespace Fiver.Security.AspIdentity.Models.Security
{
    public class LoginViewModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}