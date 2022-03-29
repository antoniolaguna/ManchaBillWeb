using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManchaBillWeb.Models
{
    public class Bill : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double MoneyDelivered { get; set; }
        [Required]
        public double MoneyReturned { get; set; }
        [Required]
        public string SeriesBill { get; set; }
        [Required]
        public int YearBill { get; set; }
        [Required]
        public string NumberBill { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Prize { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public ICollection<BillLine> BillLines { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Client { get; set; }

    }
}
