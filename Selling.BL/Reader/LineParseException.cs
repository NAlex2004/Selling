using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Selling.BL.Reader
{
    public class LineParseException: Exception
    {
        public string Line { get; protected set; }
        public LineParseException(string line): base("Line cannot be parsed..")
        {
            Line = line;
        }
    }
}
