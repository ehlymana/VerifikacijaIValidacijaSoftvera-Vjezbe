using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Pregled
    {
        string misljenje, rezultat, garancija;
        string terapija, blokOpis;
        bool dugorocnaTerapija, blokirana;
        DateTime datumPropisivanja;

        public string Misljenje
        {
            get
            {
                return misljenje;
            }

            set
            {
                misljenje = value;
            }
        }

        public string Rezultat
        {
            get
            {
                return rezultat;
            }

            set
            {
                rezultat = value;
            }
        }

        public string Garancija
        {
            get
            {
                return garancija;
            }

            set
            {
                garancija = value;
            }
        }

        public string Terapija
        {
            get
            {
                return terapija;
            }

            set
            {
                terapija = value;
            }
        }

        public bool DugorocnaTerapija
        {
            get
            {
                return dugorocnaTerapija;
            }

            set
            {
                dugorocnaTerapija = value;
            }
        }

        public bool Blokirana
        {
            get
            {
                return blokirana;
            }

            set
            {
                blokirana = value;
            }
        }

        public DateTime DatumPropisivanja
        {
            get
            {
                return datumPropisivanja;
            }

            set
            {
                datumPropisivanja = value;
            }
        }

        public string BlokOpis
        {
            get
            {
                return blokOpis;
            }

            set
            {
                blokOpis = value;
            }
        }

        public void blokirajTerapiju(string opis)
        {
            Blokirana = true;
            BlokOpis = opis;
        }
        public Pregled(string m, string r, string t, string g, bool d, DateTime dat)
        {
            Misljenje = m;
            Rezultat = r;
            Terapija = t;
            Garancija = g;
            DugorocnaTerapija = d;
            DatumPropisivanja = dat;
            Blokirana = false;
        }
    }
}
