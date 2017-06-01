using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;

namespace TempTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SalesContext context = new SalesContext())
            {
                context.Products.ToList().ForEach(p => Console.WriteLine("{0,-8}{1,-50}{2,10}", p.Id, p.ProductName, p.Price));
                
            }

            Console.ReadKey();
        }
    }
}
