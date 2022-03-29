using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class SizeType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Size> Sizes { get; set; }

        public ICollection<ItemType> ItemTypes { get; set; }
    }
}
