using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.BL.Reader;
using NAlex.Selling.DAL.Units;

namespace NAlex.Selling.BL
{
    public interface IOperatorParamsFactory
    {
        ISalesReader CreateReader();
        ISalesUnit CreateUnit();
        int RecordsPerPass { get; }
    }
}
