using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DTO.Classes;

namespace NAlex.Selling.DAL.Repositories
{
    public class SalesRepository: DtoRepository<Sale, SaleDTO, int>
    {
        public SalesRepository(DbContext context): base(context)
        {
        }
    }
}
