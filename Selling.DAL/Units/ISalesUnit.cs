using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DAL.Repositories;
using NAlex.DataModel.Entities;
using NAlex.Selling.DAL;

namespace NAlex.Selling.DAL.Units
{
    public interface ISalesUnit: IDisposable
    {
        IRepository<Customer, int> Customers { get; }
        IRepository<Product, int> Products { get; }
        IRepository<Manager, int> Managers { get; }
        IRepository<Sale, int> Sales { get; }
        IRepository<TempSale, Guid> TempSales { get; }

        SpResult CopyTempSalesToSales(Guid sessionId);
        bool SaveChanges();
    }
}
