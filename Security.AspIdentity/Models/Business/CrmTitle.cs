using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security.AspIdentity.Models.Business
{
    public class CrmTitle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        [Display(Name = "Personnel Title")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Subitle cannot be longer than 50 characters.")]
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        [Display(Name = "Personnel Title")]
        public string Description { get; set; }

        public Guid ParentId { get; set; }

        [Display(Name = "Parent Title")]
        public virtual CrmTitle Parent { get; set; }

        [Required]
        [Display(Name = "Children Titles")]
        public virtual ICollection<CrmTitle> Children { get; set; }
    }
}