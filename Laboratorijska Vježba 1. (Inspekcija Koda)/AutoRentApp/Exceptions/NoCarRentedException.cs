using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRentApp.Exceptions
{
    public class NoCarRentedException : Exception
    {
        public NoCarRentedException(string p)
            : base(p)
        {

        }
    }
}
