using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NAlex.Selling.DTO.Classes;

namespace NAlex.Selling.BL.Reader
{
    public class SalesReader: ISalesReader
    {
        private bool _disposed = false;        
        private StreamReader _reader;
        private string _managerName;

        struct Positions
        {
            public static int SaleDate { get { return 0; } }
            public static int Customer { get { return 1; } }
            public static int Product { get { return 2; } }
            public static int Sum { get { return 3; } }
        }

        public SalesReader(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("'{0}' is not a valid filename.", filePath);
            string[] parts = fileName.Trim().Split('_');
            if (parts.Length == 0)
                throw new ArgumentException("File name '{0}' is not compatible with pattern 'SecondName_DDMMYYYY'.", fileName);
            _managerName = parts[0];

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            _reader = new StreamReader(fs);
        }

        public TempSaleDTO ReadNext()
        {
            string line = _reader.ReadLine();

            if (line != null)
            {
                string[] splitted = line.Trim().Split(',');
                if (splitted.Length < 4)
                    throw new LineParseException(line);

                TempSaleDTO sale = new TempSaleDTO();
                
                DateTime saleDate;
                
                if (!DateTime.TryParse(splitted[Positions.SaleDate], out saleDate))
                    throw new LineParseException(line);

                double sum;

                if (!double.TryParse(splitted[Positions.Sum], out sum))
                    throw new LineParseException(line);

                sale.ManagerName = _managerName;
                sale.SaleDate = saleDate;
                sale.ProductName = splitted[Positions.Product];
                sale.CustomerName = splitted[Positions.Customer];
                sale.Total = sum;

                return sale;
            }
         
            return null;
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            
            if (disposing)
            {
                if (_reader != null)
                    _reader.Close();
            }

            _disposed = true;
        }

    }
}
