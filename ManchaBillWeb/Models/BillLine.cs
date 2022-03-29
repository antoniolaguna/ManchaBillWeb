using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManchaBillWeb.Models
{
    public class BillLine:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Units { get; set; }
        [Required]
        public int ReturnedUnits { get; set; }
        [Required]
        public double UnitPrize { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        [Display(Name = "Discount")]
        public double Discount { get; set; }
        public double Sum { get; set; }
        public int? ModelId { get; set; }
        public Model Model { get; set; }
        public int? BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
