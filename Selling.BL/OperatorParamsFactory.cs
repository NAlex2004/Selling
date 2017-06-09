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
        private string _filePath;

        public ISalesReader CreateReader()
        {
            return new SalesReader(_filePath);
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

        public OperatorParamsFactory(string filePath, int recordsPerPass = int.MaxValue)
        {
            _filePath = filePath;
            RecordsPerPass = recordsPerPass;
        }


        public string Source
        {
            get { return _filePath; }
        }
    }
}
