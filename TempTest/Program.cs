using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DAL.Units;
using NAlex.Selling.DAL.Repositories;

namespace TempTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ISalesUnit unit = new SalesUnit())
            {
                unit.Products.GetAll().ToList().ForEach(p => Console.WriteLine("{0,-8}{1,-50}{2,10}", p.Id, p.ProductName, p.Price));                
            }

            Console.ReadKey();
        }
    }
}
