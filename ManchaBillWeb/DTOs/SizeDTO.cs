using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.DTOs
{
    public class SizeDTO: BaseDTO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Size Type")]
        public int SizeTypeId { get; set; }
        [Display(Name = "Size Type")]
        public SizeTypeDTO SizeType { get; set; }
    }
}
