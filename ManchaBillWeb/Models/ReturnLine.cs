using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class ReturnLine:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Units { get; set; }
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
        public int? ReturnId { get; set; }
        public Return Return { get; set; }
    }
}
