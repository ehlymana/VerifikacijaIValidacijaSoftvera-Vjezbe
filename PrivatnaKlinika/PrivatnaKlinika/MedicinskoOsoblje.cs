using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class MedicinskoOsoblje : Osoblje
    {
        int posjete = 0;
        double plata=1000, bonus = 0.01;
        Ordinacija ordinacija;
        public int Posjete
        {
            get
            {
                return posjete;
            }

            set
            {
                posjete = value;
            }
        }

        public double Plata
        {
            get
            {
                return plata;
            }

            set
            {
                plata = value;
            }
        }

        public Ordinacija Ordinacija
        {
            get
            {
                return ordinacija;
            }

            set
            {
                ordinacija = value;
            }
        }

        public MedicinskoOsoblje (string i, string p, string u, string pa, Ordinacija o) : base(i, p, u, pa) { Ordinacija = o; }

        public MedicinskoOsoblje(string i, string p, string u, string pa) : base(i, p, u, pa) { }
        public void obracunajPlatu ()
        {
            int posjete2 = 0;
            if (Posjete > 20) posjete2 = 20;
            else posjete2 = Posjete;
            if (Password[Password.Length-1]<'0' || Password[Password.Length - 1] > '9') Plata += bonus * posjete2;
        }
    }
}
