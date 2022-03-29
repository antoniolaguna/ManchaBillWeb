using System;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class BaseEntity
    {
        [Required]
        public bool Active { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        public string Comments { get; set; }
    }
}
