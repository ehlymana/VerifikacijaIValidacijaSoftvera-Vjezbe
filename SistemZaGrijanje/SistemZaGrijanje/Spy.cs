using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public class Spy : ITermostat
    {
        public int Opcija { get; set; }
        public double dajTemperaturu()
        {
            if (Opcija == 0)
                return 10;
            else if (Opcija == 1)
                return 25;
            else
                return 40;
        }
    }
}
