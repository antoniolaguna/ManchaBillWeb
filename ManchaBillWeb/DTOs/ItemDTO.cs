using ManchaBillWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class ItemDTO : BaseDTO
    {
      
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display (Name ="Description")]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Cost")]
        public double CostPrize { get; set; }

        [Required]
        [Display(Name = "Value")]
        public double Value { get; set; }

        [Required]
        [Display(Name = "Tax")]
        public double Tax { get; set; }

        [Required]
        [Display(Name = "Discount")]
        public double Discount { get; set; }

        [Required]
        [Display(Name = "Prize")]
        public double Prize { get; set; }

        [Required(ErrorMessage = "You must select a Type")]
        [Display(Name = "Type")]
        public int ItemTypeId { get; set; }
        [Display(Name = "Type")]
        public ItemTypeDTO ItemType { get; set; }
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Supplier")]
        public SupplierDTO? Supplier { get; set; }
        [Display(Name = "Season")]
        public int? SeasonId { get; set; }
        [Display(Name = "Season")]
        public SeasonDTO? Season { get; set; }

        public ICollection<ModelDTO> Models { get; set; }
    }
}
