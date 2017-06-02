﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;

namespace NAlex.Selling.DAL.Repositories
{
    public class SalesRepository: Repository<Sale, int>
    {
        public SalesRepository(DbContext context): base(context)
        {
        }
    }
}