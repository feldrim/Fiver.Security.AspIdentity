using System.ComponentModel.DataAnnotations;

namespace Security.AspIdentity.ViewModel.Security
{
    public class LoginViewModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}