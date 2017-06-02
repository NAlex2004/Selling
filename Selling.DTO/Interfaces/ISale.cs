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

        IManager Manager { get; set; }
        ICustomer Customer { get; set; }
        IProduct Product { get; set; }
    }
}
