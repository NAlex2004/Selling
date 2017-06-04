using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DTO.Classes
{
    public class ProductDTO : IProduct
    {
        public int Id { get; set; }        
        public double Price { get; set; }
        public string ProductName { get; set; }        
    }
}
