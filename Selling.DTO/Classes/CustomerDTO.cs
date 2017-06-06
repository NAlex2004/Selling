using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DTO.Classes
{
    public class CustomerDTO: IEquatable<CustomerDTO>
    {
        public int Id { get; set; }        
        public string CustomerName { get; set; }

        public bool Equals(CustomerDTO other)
        {
            return other != null ? Id == other.Id && CustomerName == other.CustomerName : false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CustomerDTO);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(CustomerName) ? 0 : 
              string.Format("{0}_{1}", Id, CustomerName).GetHashCode();
        }
    }
}
