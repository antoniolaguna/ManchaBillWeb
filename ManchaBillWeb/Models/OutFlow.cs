using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class OutFlow:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
