using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserSerija
{
    public class Raspored
    {
        #region Atributi

        DateTime pocetak, kraj;
        List<Serija> serije;
        List<DateTime> vremenaPrikazivanja;

        #endregion

        #region Properties

        public DateTime Pocetak { get => pocetak; set => pocetak = value; }
        public DateTime Kraj { get => kraj; set => kraj = value; }
        public List<Serija> Serije { get => serije; set => serije = value; }
        public List<DateTime> VremenaPrikazivanja { get => vremenaPrikazivanja; set => vremenaPrikazivanja = value; }

        #endregion

        #region Konstruktor

        public Raspored(DateTime pocetniDan, DateTime krajnjiDan, List<Serija> sveSerije, List<DateTime> svaPrikazivanja)
        {
            if (pocetniDan > krajnjiDan || krajnjiDan < pocetniDan)
                throw new FormatException("Neispravno unesena sedmica rasporeda!");

            Pocetak = pocetniDan;
            Kraj = krajnjiDan;
            Serije = sveSerije;
            VremenaPrikazivanja = svaPrikazivanja;
        }

        #endregion
    }
}
