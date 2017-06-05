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
                    if (customer.Id > 0)
                    {
                        sale.CustomerId = customer.Id;
                        sale.Customer = null;
                        _context.Entry(customer).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Set<Customer>().Remove(customer);
                    }
                    
                }

                if (product != null)
                {
                    if (product.Id > 0)
                    {
                        sale.ProductId = product.Id;
                        sale.Product = null;
                        _context.Entry(product).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Set<Product>().Remove(product);
                    }
                    
                }

                if (manager != null)
                {
                    if (manager.Id > 0)
                    {
                        sale.ManagerId = manager.Id;
                        sale.Manager = null;
                        _context.Entry(manager).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Set<Manager>().Remove(manager);
                    }                    
                }

                var added = _context.Set<Sale>().Add(sale);
                
                return Mapper.Map<SaleDTO>(added);
            }
            catch
            {
                return null;
            }
            
        }
    }
}
