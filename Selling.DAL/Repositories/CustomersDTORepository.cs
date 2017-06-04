using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Interfaces;
using NAlex.Selling.DTO.Classes;
using NAlex.Selling.DTO;
using System.Linq.Expressions;
using System.Data.Entity;
using NAlex.DataModel.Entities;

namespace NAlex.Selling.DAL.Repositories
{
    public class CustomersDTORepository : DtoRepository<Customer, CustomerDTO, int>
    {
        public CustomersDTORepository(DbContext context) : base(context)
        {
        }
    }
}
