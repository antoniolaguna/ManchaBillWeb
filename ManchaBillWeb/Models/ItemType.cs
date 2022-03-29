using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class ItemType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }

        public int SizeTypeId { get; set; }
        public SizeType SizeType { get; set; }

    }
}
