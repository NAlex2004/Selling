using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DTO.Interfaces
{
    public interface ISale
    {
        int Id { get; set; }
        DateTime SaleDate { get; set; }
        double Total { get; set; }

        int ManagerId { get; set; }
        int CustomerId { get; set; }
        int ProductId { get; set; }
    }
}
