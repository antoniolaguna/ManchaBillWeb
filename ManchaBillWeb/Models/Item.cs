using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManchaBillWeb.Models
{
    public class Item : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(120)]
        public string LongDescription { get; set; }

        [Required]
        public double CostPrize { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public double Tax { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public double Prize { get; set; }

        public int ItemTypeId { get; set; }

        public ItemType ItemType { get; set; }

        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int? SeasonId { get; set; }

        public Season Season { get; set; }

        public ICollection<Model> Models { get; set; }

    }

}
