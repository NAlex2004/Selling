using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DAL.Repositories;
using NAlex.DataModel.Entities;

namespace NAlex.Selling.DAL.Units
{
    public interface ISalesUnit: IDisposable
    {
        IRepository<Customer, int> Customers { get; }
        IRepository<Product, int> Products { get; }
        IRepository<Manager, int> Managers { get; }
        IRepository<Sale, int> Sales { get; }

        bool SaveChanges();
    }
}
