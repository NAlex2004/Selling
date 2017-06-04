using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DTO.Interfaces
{
    public interface ITempSale
    {
        Guid SessionId { get; set; }     
        DateTime SaleDate { get; set; }        
        double Total { get; set; }        
        string ManagerName { get; set; }
        string CustomerName { get; set; }        
        string ProductName { get; set; }
    }
}

