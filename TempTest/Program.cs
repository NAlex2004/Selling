using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DAL.Units;
using NAlex.Selling.DAL.Repositories;
using System.Data.SqlClient;
using NAlex.Selling.DTO.Classes;

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

                var rep = unit.Customers;

                var cust = rep.Get(c => c.CustomerName.Contains("ustomer")).ToList();
                cust.ForEach(c =>
                {
                    Console.WriteLine(c.CustomerName);
                    rep.Remove(c);
                });

                CustomerDTO newCust = new CustomerDTO()
                {
                    CustomerName = "Added"
                };

                rep.Add(newCust);

                CustomerDTO edited = rep.Get(2);
                edited.CustomerName += " Edited";
                rep.Update(edited);

                unit.SaveChanges();
            }


            Console.ReadKey();
        }
    }
}
