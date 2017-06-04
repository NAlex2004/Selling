using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DTO.Classes
{
    public class ManagerDTO : IManager
    {
        public int Id { get; set; }        
        public string LastName { get; set; }        
    }
}
