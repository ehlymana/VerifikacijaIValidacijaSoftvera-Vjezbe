using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary.Exceptions
{
    class NoIssuedBookFoundException:Exception
    {

        public NoIssuedBookFoundException(string message)
            : base(message)
        {
        }
    }
}
