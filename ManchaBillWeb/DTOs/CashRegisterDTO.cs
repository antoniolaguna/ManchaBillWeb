using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class CashRegisterDTO:BaseDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Opening")]
        public DateTime OpeningDate { get; set; }
        [Display(Name = "Closing")]
        public DateTime ClosingDate { get; set; }
        [Required]
        public bool Close { get; set; }
        [Required]
        [Display(Name = "Last Close Rem.")]
        public double LastCloseRemander { get; set; }
        [Required]
        [Display(Name = "Sales")]
        public double CashSales { get; set; }
        [Display(Name = "Returns")]
        [Required]
        public double Returns { get; set; }
        [Required]
        [Display(Name = "Outflows")]
        public double OutFlows { get; set; }
        [Required]
        [Display(Name = "Cash")]
        public double Cash { get; set; }
        [Required]
        [Display(Name = "Real Cash")]
        public double RealCash { get; set; }
        [Required]
        [Display(Name = "Cash Out")]
        public double CashOut { get; set; }
        [Required]
        [Display(Name = "Remander")]
        public double Remander { get; set; }
        [Required]
        [Display(Name = "Unbalance")]
        public double Unbalance { get; set; }
        [Required]
        [Display(Name = "Unbalance %")]
        public double UnbalancePercentage { get; set; }
        [Display(Name = "Observations")]
        public string Observations { get; set; }
    }
}
