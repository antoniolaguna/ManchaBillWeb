using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManchaBillWeb.DTOs
{
    public class ReturnDTO:BaseDTO
    {
        [Key]
        public int Id { get; set; }
        public int BillId { get; set; }
        public BillDTO Bill { get; set; }
        public DateTime Date { get; set; }
        public string SeriesReturn { get; set; }
        public int YearReturn { get; set; }
        public string NumberReturn { get; set; }
        public double Prize { get; set; }
        public double Tax { get; set; }
        public double Value { get; set; }
        public ICollection<ReturnLineDTO> ReturnLines { get; set; }
        public string Client { get; set; }
    }
}
