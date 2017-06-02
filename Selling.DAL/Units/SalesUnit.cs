﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NAlex.DataModel.Entities;
using NAlex.Selling.DAL.Repositories;

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
            get { throw new NotImplementedException(); }
        }

        public IRepository<Product, int> Products
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<Manager, int> Managers
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<Sale, int> Sales
        {
            get { throw new NotImplementedException(); }
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
                _context.SaveChanges();
                _context.Dispose();
            }

            _disposed = true;
        }

        
    }
}