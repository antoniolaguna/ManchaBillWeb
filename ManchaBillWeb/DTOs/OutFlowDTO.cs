using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class OutFlowDTO
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
