using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary.Exceptions
{
    public class BookNotAvailableException : Exception
    {

        public BookNotAvailableException(string p)
            : base(p)
        {

        }


    }
}
