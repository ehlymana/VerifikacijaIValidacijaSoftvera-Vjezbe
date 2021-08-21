using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public class StubNiskaTemperatura : ITermostat
    {
        public double dajTemperaturu()
        {
            return 10;
        }
    }

    public class StubSrednjaTemperatura : ITermostat
    {
        public double dajTemperaturu()
        {
            return 25;
        }
    }

    public class StubVisokaTemperatura : ITermostat
    {
        public double dajTemperaturu()
        {
            return 40;
        }
    }
}
