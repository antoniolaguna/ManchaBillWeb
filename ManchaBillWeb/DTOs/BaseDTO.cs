using System;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class BaseDTO
    {
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? DeleteDate { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}
