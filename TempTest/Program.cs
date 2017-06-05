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
                //unit.Products.GetAll().ToList().ForEach(p => Console.WriteLine("{0,-8}{1,-50}{2,10}", p.Id, p.ProductName, p.Price));

                //var rep = unit.Customers;

                //var cust = rep.Get(c => c.CustomerName.Contains("ustomer")).ToList();
                //cust.ForEach(c =>
                //{
                //    Console.WriteLine(c.CustomerName);
                //    rep.Remove(c);
                //});

                //CustomerDTO newCust = new CustomerDTO()
                //{
                //    CustomerName = "Added"
                //};

                //rep.Add(newCust);

                //CustomerDTO edited = rep.Get(2);
                //edited.CustomerName += " Edited";
                //rep.Update(edited);

                ProductDTO prod = unit.Products.Get(2);
                CustomerDTO customer = unit.Customers.Get(3);
                ManagerDTO manager = new ManagerDTO()
                {
                    LastName = "Вася"
                };

                var addedMan = unit.Managers.Add(manager);
                unit.Managers.Add(manager);

                //SaleDTO sale = new SaleDTO()
                //{
                //    Customer = customer,
                //    Manager = manager,
                //    Product = prod,
                //    SaleDate = DateTime.Now,
                //    Total = 13.16
                //};

                //unit.Sales.Add(sale);

                unit.SaveChanges();
            }


            Console.ReadKey();
        }
    }
}
