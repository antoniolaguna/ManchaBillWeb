using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.Models
{
    public class Return : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BillId { get; set; }
        public Bill Bill { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string SeriesReturn { get; set; }
        [Required]
        public int YearReturn { get; set; }
        [Required]
        public string NumberReturn { get; set; }
        [Required]
        public double Prize { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public ICollection<ReturnLine> ReturnLines { get; set; }

        public string Client { get; set; }
    }
}
