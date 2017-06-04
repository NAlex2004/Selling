using System;

namespace NAlex.Selling.DTO.Classes
{
    public class TempSaleDTO
    {
        public string CustomerName { get; set; }
        public string ManagerName { get; set; }
        public string ProductName { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid SessionId { get; set; }
        public double Total { get; set; }        
    }
}
