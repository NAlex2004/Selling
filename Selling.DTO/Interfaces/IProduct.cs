using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DTO.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string ProductName { get; set; }
        double Price { get; set; }
    }
}
