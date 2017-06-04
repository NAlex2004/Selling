using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DTO.Classes
{
    public class CustomerDTO : ICustomer
    {
        public int Id { get; set; }        
        public string CustomerName { get; set; }        
    }
}
