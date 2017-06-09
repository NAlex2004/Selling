using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.DTO.Classes;

namespace NAlex.Selling.BL.Reader
{
    public interface ISalesReader: IDisposable
    {        
        TempSaleDTO ReadNext();
        void Close();
    }
}
