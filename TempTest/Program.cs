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
using NAlex.Selling.DAL;
using NAlex.Selling.BL.Reader;

namespace TempTest
{    
    class Program
    {
        static void TestReader(ISalesUnit unit)
        {
            Guid session = Guid.NewGuid();
            try
            {
                using (SalesReader reader = new SalesReader())
                {
                    reader.Open("NewManager_18121980.csv");
                    TempSaleDTO tempSale;
                    while ((tempSale = reader.ReadNext()) != null)
                    {
                        tempSale.SessionId = session;
                        unit.TempSales.Add(tempSale);                        
                    }
                }

                unit.SaveChanges();
            }
            catch
            {
                unit.TempSales.Remove(t => t.SessionId.Equals(session));

                unit.SaveChanges();
            }
        }

        static void Main(string[] args)
        {               
            using (ISalesUnit unit = new SalesUnit())
            {

                //TestReader(unit);
                //return;

                CustomerDTO cc = new CustomerDTO()
                {
                    CustomerName = "AAAA"
                };

                unit.Customers.Add(cc);

                ProductDTO pp = new ProductDTO()
                {
                    Price = 1.1,
                    ProductName = "2 PPPP"
                };

                unit.Products.Add(pp);

                ManagerDTO mm = new ManagerDTO()
                {
                    LastName = "MMMM"
                };

                unit.Managers.Add(mm);

                SaleDTO ss = new SaleDTO()
                {
                    Manager = mm,
                    Product = pp,
                    Customer = cc,
                    SaleDate = DateTime.Now,
                    Total = 23.11
                };

                unit.Sales.Add(ss);

                unit.SaveChanges();
                return;

                Guid guid = Guid.NewGuid();

                TempSaleDTO tmp = new TempSaleDTO()
                {
                    CustomerName = "Temp sale Customer 1",
                    ManagerName = "Temp manager 1",
                    ProductName = "Temp product 1",
                    SaleDate = DateTime.Now,
                    SessionId = guid,
                    Total = 15.22
                };

                TempSaleDTO tmp2 = new TempSaleDTO()
                {
                    CustomerName = "Temp sale Customer 2",
                    ManagerName = "Temp manager 2",
                    ProductName = "Temp product 2",
                    SaleDate = DateTime.Now,
                    SessionId = guid,
                    Total = 115.13
                };

                Guid guid2 = Guid.NewGuid();

                TempSaleDTO tmp3 = new TempSaleDTO()
                {
                    CustomerName = "Temp sale Customer 3",
                    ManagerName = "Temp manager 3",
                    ProductName = "Temp product 3",
                    SaleDate = DateTime.Now,
                    SessionId = guid2,
                    Total = 52.87
                };

                //unit.TempSales.Add(tmp);
                //unit.TempSales.Add(tmp2);
                //unit.TempSales.Add(tmp3);
                unit.TempSales.AddRange(new[] { tmp, tmp2, tmp3, tmp, tmp2 });

                //ProductDTO prod = new ProductDTO()
                //{
                //    ProductName = "Просто гвоздь",
                //    Price = 0.43
                //};
                //CustomerDTO customer = new CustomerDTO()
                //{
                //    CustomerName = "Новый покупатель"
                //};
                //ManagerDTO manager = new ManagerDTO()
                //{
                //    LastName = "Иванов"
                //};


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

                unit.DeleteTempSales(guid);
                //SpResult res = unit.CopyTempSalesToSales(guid);
                //Console.WriteLine("{0} : {1}", res.ErrorNumber, res.ErrorMessage);
                //res = unit.CopyTempSalesToSales(guid2);
                //Console.WriteLine("{0} : {1}", res.ErrorNumber, res.ErrorMessage);
            }


            Console.ReadKey();
        }
    }
}
