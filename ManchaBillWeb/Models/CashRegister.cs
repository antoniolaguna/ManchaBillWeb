using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class CashRegister:BaseEntity
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        [Required]
        public bool Close { get; set; }
        [Required]
        public double LastCloseRemander { get; set; }
        [Required]
        public double CashSales { get; set; }
        [Required]
        public double Returns { get; set; }
        [Required]
        public double OutFlows { get; set; }
        [Required]
        public double Cash { get; set; }
        [Required]
        public double RealCash { get; set; }
        [Required]
        public double CashOut { get; set; }
        [Required]
        public double Remander { get; set; }
        [Required]
        public double Unbalance { get; set; }
        [Required]
        public double UnbalancePercentage { get; set; }
        public string Observations { get; set; }


    }
}
