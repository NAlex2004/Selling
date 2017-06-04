using System;

namespace NAlex.Selling.DTO.Classes
{
    public class SaleDTO
    {
        public CustomerDTO Customer { get; set; }        
        public int Id { get; set; }        
        public ManagerDTO Manager { get; set; }        
        public ProductDTO Product { get; set; }
        public DateTime SaleDate { get; set; }
        public double Total { get; set; }        
    }
}
