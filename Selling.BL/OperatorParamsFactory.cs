using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.BL.Reader;
using NAlex.Selling.DAL.Units;

namespace NAlex.Selling.BL
{
    public class OperatorParamsFactory: IOperatorParamsFactory
    {
        public ISalesReader CreateReader()
        {
            return new SalesReader();
        }

        public ISalesUnit CreateUnit()
        {
            return new SalesUnit();
        }


        public int RecordsPerPass
        {
            get;
            protected set;
        }

        public OperatorParamsFactory(int recordsPerPass = 0)
        {
            RecordsPerPass = recordsPerPass;
        }
    }
}
