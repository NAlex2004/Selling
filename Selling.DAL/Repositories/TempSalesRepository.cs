using NAlex.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace NAlex.Selling.DAL.Repositories
{
    public class TempSalesRepository : Repository<TempSale, Guid>
    {
        public TempSalesRepository(DbContext context) : base(context)
        {
        }
    }
}
