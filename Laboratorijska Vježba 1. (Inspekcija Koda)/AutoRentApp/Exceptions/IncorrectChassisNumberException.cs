using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRentApp.Exceptions
{
    public class IncorrectChassisNumberException : Exception
    {
        public IncorrectChassisNumberException(string p)
            : base(p)
        {

        }
    }
}
