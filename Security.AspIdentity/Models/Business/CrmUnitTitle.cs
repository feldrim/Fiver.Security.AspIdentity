using System.ComponentModel.DataAnnotations;

namespace Security.AspIdentity.Models.Business
{
    public class CrmUnitTitle
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Business Unit")]
        public virtual CrmUnit Unit { get; set; }

        [Required]
        [Display(Name = "Personnel Title")]
        public virtual CrmTitle Title { get; set; }
    }
}