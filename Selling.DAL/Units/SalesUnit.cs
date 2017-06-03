﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NAlex.DataModel.Entities;
using NAlex.Selling.DAL.Repositories;
using System.Data.SqlClient;

namespace NAlex.Selling.DAL.Units
{
    public class SalesUnit: ISalesUnit
    {
        private bool _disposed = false;
        private DbContext _context;

        IRepository<Customer, int> _customers;
        IRepository<Product, int> _products;
        IRepository<Manager, int> _managers;
        IRepository<Sale, int> _sales;
        IRepository<TempSale, Guid> _tempSales;

        public SalesUnit()
        {
            _context = new SalesContext();

            _customers = new CustomersRepository(_context);
            _products = new ProductsRepository(_context);
            _managers = new ManagersRepository(_context);
            _sales = new SalesRepository(_context);
        }

        public IRepository<Customer, int> Customers
        {
            get { return _customers; }
        }

        public IRepository<Product, int> Products
        {
            get { return _products; }
        }

        public IRepository<Manager, int> Managers
        {
            get { return _managers; }
        }

        public IRepository<Sale, int> Sales
        {
            get { return _sales; }
        }

        public IRepository<TempSale, Guid> TempSales
        {
            get
            {
                return _tempSales;
            }
        }

        public SpResult CopyTempSalesToSales(Guid sessionId)
        {
            if (sessionId == Guid.Empty)
                return new SpResult() { ErrorNumber = -1, ErrorMessage = "sessionId cannot be empty." };

            SqlParameter sessionIdParam = new SqlParameter("@SessionId", System.Data.SqlDbType.UniqueIdentifier);
            sessionIdParam.Value = sessionId;
            return _context.Database.SqlQuery<SpResult>("exec Sales.dbo.CopyTempSales @SessionId").FirstOrDefault();
        }

        public bool SaveChanges()
        {
            //_context.Database.BeginTransaction()
            try
            {
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                SaveChanges();
                _context.Dispose();
            }

            _disposed = true;
        }
                
    }
}
