using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class Season:BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
       
        public ICollection<Item> Items { get; set; }
    }
}
