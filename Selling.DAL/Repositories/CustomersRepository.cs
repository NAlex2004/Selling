using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NAlex.DataModel.Entities;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DAL.Repositories
{
    public class CustomersRepository: Repository<Customer, int>
    {
        public CustomersRepository(DbContext context) : base(context ?? new SalesContext())
        {
        }
    }
}
