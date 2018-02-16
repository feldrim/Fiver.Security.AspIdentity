using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity.Models.Business
{
    public class CrmPersonnel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName.ToUpper(CultureInfo.CurrentCulture)}";

        [Required]
        [Display(Name = "Application User")]
        public virtual CrmUser UserData { get; set; }
    }
}