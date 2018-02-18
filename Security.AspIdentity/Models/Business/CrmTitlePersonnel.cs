using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security.AspIdentity.Models.Business
{
    public class CrmTitlePersonnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Personnel Title")]
        public virtual CrmTitle Title { get; set; }

        [Required]
        [Display(Name = "Personnel Data")]
        public virtual CrmPersonnel Personnel { get; set; }
    }
}