using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRentApp.Exceptions
{
    public class AdditionalPackageDoesNotExistException : Exception
    {
        public AdditionalPackageDoesNotExistException(string p)
            : base(p)
        {

        }
    }
}
