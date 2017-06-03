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
                SqlParameter input = new SqlParameter("@Input", System.Data.SqlDbType.Int);
                input.Value = 111;
                SqlParameter output = new SqlParameter("@OutStr", System.Data.SqlDbType.VarChar, 100);
                output.Direction = System.Data.ParameterDirection.Output;
                SqlParameter outInt = new SqlParameter("@OutInt", System.Data.SqlDbType.Int);
                outInt.Direction = System.Data.ParameterDirection.Output;
                var res = context.Database.SqlQuery<Result>("exec TestRes @Input, @OutStr output, @OutInt output", input, output, outInt).SingleOrDefault();
                Console.WriteLine((string)output.Value);
                Console.WriteLine((int)outInt.Value);
                Console.WriteLine(res.Id);
                Console.WriteLine(res.OutMsg);
            }

                Console.ReadKey();
        }
    }
}
