using System.ComponentModel.DataAnnotations;

namespace Security.AspIdentity.Models.Business
{
    public class CrmTitlePersonnel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Personnel Title")]
        public virtual CrmTitle Title { get; set; }

        [Required]
        [Display(Name = "Personnel Data")]
        public virtual CrmPersonnel Personnel { get; set; }
    }
}