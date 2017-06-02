using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NAlex.DataModel.Entities;

namespace NAlex.Selling.DAL.Repositories
{
    public class BaseSellingRepository<TEntity, TKey>: Repository<TEntity, TKey>
        where TEntity: class
    {
        public BaseSellingRepository(DbContext context): base(context ?? new SalesContext())
        {        
        }
    }
}
