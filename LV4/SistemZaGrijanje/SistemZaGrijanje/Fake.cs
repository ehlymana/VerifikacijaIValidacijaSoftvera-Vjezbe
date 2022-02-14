using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemZaGrijanje
{
    public class Fake : ITermostat
    {
        public List<double> Temperature { get; set; }

        public double DodajTestneTemperature()
        {
            Temperature = new List<double>()
            {
                22.5, 22, 22.1, 22.7, 22.8, 22.6, 22.4
            };
            return Math.Abs(30 - Temperature.Average()) / Temperature.Average();
        }
    }
}
