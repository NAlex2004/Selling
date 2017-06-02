using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.DataModel.Entities
{
    public class Product: IProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
