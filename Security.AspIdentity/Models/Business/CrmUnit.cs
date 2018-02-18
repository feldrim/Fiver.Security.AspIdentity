using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security.AspIdentity.Models.Business
{
    public class CrmUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Busines unit name cannot be longer than 50 characters.")]
        [Display(Name = "Business Unit Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Busines unit type cannot be longer than 50 characters.")]
        [Display(Name = "Business Unit Type")]
        public string Type { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Busines unit description cannot be longer than 150 characters.")]
        [Display(Name = "Business Unit Description")]
        public string Description { get; set; }

        public Guid ParentId { get; set; }

        [Display(Name = "Parent Business Unit")]
        public virtual CrmUnit Parent { get; set; }

        [Display(Name = "Children Business Units")]
        public virtual ICollection<CrmUnit> Children { get; set; }
    }
}