using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class Supplier:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
