using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DAL.Units;
using NAlex.Selling.DAL.Repositories;
using System.Data.SqlClient;

namespace TempTest
{
    class Result
    {
        public int Id { get; set; }
        public string OutMsg { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (ISalesUnit unit = new SalesUnit())
            {
                unit.Products.GetAll().ToList().ForEach(p => Console.WriteLine("{0,-8}{1,-50}{2,10}", p.Id, p.ProductName, p.Price));                

            }

            using (DbContext context = new SalesContext())
            {
                Customer cust = new Customer()
                {
                    Id = 1,
                    CustomerName = "One more Customer!"
                };

                context.Set<Customer>().Add(cust);
                context.SaveChanges();
            }

                Console.ReadKey();
        }
    }
}
