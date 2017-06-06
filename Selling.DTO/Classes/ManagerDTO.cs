﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DTO.Classes
{
    public class ManagerDTO: IEquatable<ManagerDTO>
    {
        public int Id { get; set; }        
        public string LastName { get; set; }

        public bool Equals(ManagerDTO other)
        {
            return other != null ? Id == other.Id && LastName == other.LastName : false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ManagerDTO);
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(LastName) ? 0 :
              string.Format("{0}_{1}", Id, LastName).GetHashCode();
        }
    }
}
