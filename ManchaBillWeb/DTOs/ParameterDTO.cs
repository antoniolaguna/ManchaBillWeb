using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class ParameterDTO:BaseDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
