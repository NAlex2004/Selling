using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DTO.Classes;
using AutoMapper;

namespace NAlex.Selling.DAL.Repositories
{
    public class SalesRepository: DtoRepository<Sale, SaleDTO, int>
    {
        public SalesRepository(DbContext context): base(context)
        {
        }

        public override SaleDTO Add(SaleDTO entity)
        {
            try
            {
                var customer = _context.Set<Customer>().Find(entity.Customer.Id);
                var product = _context.Set<Product>().Find(entity.Product.Id);
                var manager = _context.Set<Manager>().Find(entity.Manager.Id);

                var sale = Mapper.Map<Sale>(entity);
                if (customer != null)
                {
                    sale.CustomerId = customer.Id;
                    sale.Customer = null;
                    _context.Entry(customer).State = EntityState.Modified;
                }

                if (product != null)
                {
                    sale.ProductId = product.Id;
                    sale.Product = null;
                    _context.Entry(product).State = EntityState.Modified;
                }

                if (manager != null)
                {
                    sale.ManagerId = manager.Id;
                    sale.Manager = null;
                    _context.Entry(manager).State = EntityState.Modified;
                }

                return Mapper.Map<SaleDTO>(_context.Set<Sale>().Add(sale));
            }
            catch
            {
                return null;
            }
            
        }
    }
}
