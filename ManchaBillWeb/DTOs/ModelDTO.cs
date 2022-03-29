using ManchaBillWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class ModelDTO : BaseDTO
    {
        public ModelDTO()
        {
            Colour = "Generic";
        }
        public int Id { get; set; }

        public string Colour { get; set; }

        [Required]
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Required]
        [Display(Name = "Store")]
        public int CountStore { get; set; }

        [Required]
        [Display(Name = "Sold")]
        public int CountSold { get; set; }

        [Required]
        public int ItemId { get; set; }
        public ItemDTO Item { get; set; }

        
        public int? SizeId { get; set; }
        public SizeDTO Size { get; set; }

        public ICollection<BillLineDTO> LineBills { get; set; }
    }
}
