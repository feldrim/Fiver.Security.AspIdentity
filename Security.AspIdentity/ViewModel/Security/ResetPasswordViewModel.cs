using System.ComponentModel.DataAnnotations;

namespace Security.AspIdentity.ViewModel.Security
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }

        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}