using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer19
{
    public class Error
    {
        public string message;
        public int index;
        public int line;

        public Error(int index, int line, string message)
        {
            this.index = index;
            this.line = line;
            this.message = message;
        }

        public override string ToString()
        {
            return line.ToString().PadRight(8) + message;
        }
    }
}
