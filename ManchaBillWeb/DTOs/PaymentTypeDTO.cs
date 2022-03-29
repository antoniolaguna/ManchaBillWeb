using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class PaymentTypeDTO:BaseDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
