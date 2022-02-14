using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public class Mock : ITermostat
    {
        public double dajTemperaturu()
        {
            if (KontrolerGrijanja.DefrostIndikator() == "KOD-01")
                return 7;
            else
                throw new Exception("Termostat ne može raditi bez koda za ispravan rad vode!");
        }
    }
}
