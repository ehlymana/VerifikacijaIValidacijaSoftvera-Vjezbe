using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public class KontrolerGrijanja
    {
        public static string IndikatorVode { get; set; }
        public bool Aktivna { get; set; }
        public double JačinaGrijanja { get; set; }

        public void AutomatskaKontrola(ITermostat termostat, bool aktiviraj)
        {
            if (termostat == null)
                throw new Exception("Nije moguće uspostaviti konekciju!");

            if (!aktiviraj)
            {
                Aktivna = false;
                return;
            }

            Aktivna = true;
            if (termostat.dajTemperaturu() < 20)
                JačinaGrijanja = 1;
            else if (termostat.dajTemperaturu() > 25)
                JačinaGrijanja = 0;
            else
                JačinaGrijanja = 0.5;

            bool defrost = true;
            for (int i = 0; i < 10; i++)
                if (termostat.dajTemperaturu() > 7)
                    defrost = false;
            if (defrost)
                JačinaGrijanja = 0.1;
        }

        public void Usrednjavanje(ITermostat t)
        {
            double srednja = t.Temperature.Average();
            JačinaGrijanja = Math.Abs(30 - srednja) / srednja;
        }

        public static string DefrostIndikator()
        {
            if (IndikatorVode == "OK")
                return "KOD-01";
            else
                return "KOD-02-ERROR";
        }
    }
}
