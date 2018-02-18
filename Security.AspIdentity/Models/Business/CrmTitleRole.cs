using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity.Models.Business
{
    public class CrmTitleRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Personnel Title")]
        public virtual CrmTitle Title { get; set; }

        [Required]
        [Display(Name = "Application Role")]
        public virtual CrmRole Role { get; set; }
    }
}