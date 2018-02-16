using System.ComponentModel.DataAnnotations;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity.Models.Business
{
    public class CrmTitleRole
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Personnel Title")]
        public virtual CrmTitle Title { get; set; }

        [Required]
        [Display(Name = "Application Role")]
        public virtual CrmRole Role { get; set; }
    }
}