using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using NAlex.Selling.DTO.Classes;
using NAlex.Selling.DTO.Interfaces;

namespace NAlex.Selling.DAL
{
    // пока тут, там посмотрим, куда его запихнуть, или в BL, или тут репозитории переписать..
    public static class SellingMapper
    {
        public static ICustomer EntityToDTO(ICustomer entity)
        {
            return new CustomerDTO()
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName
            };
        }

        public static IManager EntityToDTO(IManager entity)
        {
            return new ManagerDTO()
            {
                Id = entity.Id,
                LastName = entity.LastName
            };
        }

        public static IProduct EntityToDTO(IProduct entity)
        {
            return new ProductDTO()
            {
                Id = entity.Id,
                Price = entity.Price,
                ProductName = entity.ProductName
            };
        }

        public static ISale EntityToDTO(ISale entity)
        {
            return new SaleDTO()
            {
                Id = entity.Id,
                Customer = EntityToDTO(entity.Customer),
                Manager = EntityToDTO(entity.Manager),
                Product = EntityToDTO(entity.Product),
                SaleDate = entity.SaleDate,
                Total = entity.Total
            };
        }

        public static ITempSale EntityToDTO(ITempSale entity)
        {
            return new TempSaleDTO()
            {
                SessionId = entity.SessionId,
                CustomerName = entity.CustomerName,
                ManagerName = entity.ManagerName,
                ProductName = entity.ProductName,
                SaleDate = entity.SaleDate,
                Total = entity.Total
            };
        }

        public static ICustomer DtoToEntity(ICustomer dto)
        {
            return new Customer()
            {
                Id = dto.Id,
                CustomerName = dto.CustomerName
            };
        }

        public static IManager DtoToEntity(IManager dto)
        {
            return new Manager()
            {
                Id = dto.Id,
                LastName = dto.LastName                
            };
        }

        public static IProduct DtoToEntity(IProduct dto)
        {
            return new Product()
            {
                Id = dto.Id,
                Price = dto.Price,
                ProductName = dto.ProductName
            };
        }

        public static ISale DtoToEntity(ISale dto)
        {
            return new Sale()
            {
                Id = dto.Id,
                Customer = (Customer) DtoToEntity(dto.Customer),
                Manager = (Manager) DtoToEntity(dto.Manager),
                Product = (Product) DtoToEntity(dto.Product),
                SaleDate = dto.SaleDate,
                Total = dto.Total
                
            };
        }

        public static ITempSale DtoToEntity(ITempSale dto)
        {
            return new TempSale()
            {
                SessionId = dto.SessionId,
                SaleDate = dto.SaleDate,
                CustomerName = dto.CustomerName,
                ManagerName = dto.ManagerName,
                ProductName = dto.ProductName
            };
        }
    }
}
