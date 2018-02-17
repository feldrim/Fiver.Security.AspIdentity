using System.ComponentModel.DataAnnotations;

namespace Security.AspIdentity.Models.ViewModel.Security
{
    public class LoginViewModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}