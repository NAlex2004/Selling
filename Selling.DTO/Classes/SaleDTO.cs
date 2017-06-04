using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DTO.Classes
{
    public class SaleDTO : ISale
    {
        public ICustomer Customer { get; set; }        
        public int Id { get; set; }        
        public IManager Manager { get; set; }        
        public IProduct Product { get; set; }
        public DateTime SaleDate { get; set; }
        public double Total { get; set; }        
    }
}
