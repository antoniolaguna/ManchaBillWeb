using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManchaBillWeb.Models 
{
    public class Size : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Model> Models { get; set; }
       
        public int SizeTypeId { get; set; }
        public SizeType SizeType { get; set; }
    }
}
