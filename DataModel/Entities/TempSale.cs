using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace NAlex.DataModel.Entities
{
    public class TempSale
    {
        [Required]
        [Key]
        public Guid SessionId { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string ManagerName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string ProductName { get; set; }        
    }
}
