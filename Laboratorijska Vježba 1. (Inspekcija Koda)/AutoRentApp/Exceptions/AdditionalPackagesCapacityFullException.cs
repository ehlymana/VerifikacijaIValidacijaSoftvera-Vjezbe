using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRentApp.Exceptions
{
    public class AdditionalPackagesCapacityFullException : Exception
    {
        public AdditionalPackagesCapacityFullException(string p)
            : base(p)
        {

        }
    }
}
