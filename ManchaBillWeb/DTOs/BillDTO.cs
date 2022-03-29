using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class BillDTO:BaseDTO
    {
        public BillDTO()
        {
            MoneyReturned = 0;
            MoneyDelivered = 0;
        }
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Delivered")]
        public double? MoneyDelivered { get; set; }
        [Display(Name = "Returned")]
        public double? MoneyReturned { get; set; }
        public string SeriesBill { get; set; }
        public int YearBill { get; set; }
        public string NumberBill { get; set; }
        [Display(Name = "Discount")]
        public double Discount { get; set; }
        public double Prize { get; set; }
        public double Tax { get; set; }
        public double Value { get; set; }
        public ICollection<BillLineDTO> BillLines { get; set; }
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }
        public PaymentTypeDTO PaymentType { get; set; }
        public string Client { get; set; }
    }
}
