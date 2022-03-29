using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class SupplierDTO : BaseDTO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "EMail")]
        public string EMail { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
    }
}
