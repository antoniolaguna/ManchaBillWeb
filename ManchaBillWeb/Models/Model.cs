using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManchaBillWeb.Models
{
    public class Model : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Colour { get; set; }

        [Required]
        public string Barcode { get; set; }

        [Required]
        public int CountStore { get; set; }

        [Required]
        public int CountSold { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int? SizeId { get; set; }
        public Size Size { get; set; }

        public ICollection<BillLine> LineBills { get; set; }

    }
}
