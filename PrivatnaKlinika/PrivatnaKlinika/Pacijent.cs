using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Pacijent
    {
        string ime, prezime, maticni, spol, adresa, bracnoStanje, zdravstvenaKnjizica;
        DateTime rodenje, prijem;
        Karton karton;
        int brojPosjeta;
        string zeljeniPregled;

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                if (value.Length < 2)
                    throw new InvalidOperationException("Neispravna dužina imena!");
                ime = value;
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }

            set
            {
                if (value.Length < 2)
                    throw new InvalidOperationException("Neispravna dužina prezimena!");
                prezime = value;
            }
        }

        public string Maticni
        {
            get
            {
                return maticni;
            }

            set
            {
                int dan = Int32.Parse(value.Substring(0, 2)),
                    mjesec = Int32.Parse(value.Substring(2, 2)),
                    godina = Int32.Parse(value.Substring(4, 3));
                if (dan < 1 || dan > 31 || mjesec < 1 || mjesec > 12 || godina < 0 || godina > DateTime.Now.Year - 1000)
                    throw new InvalidOperationException("Neispravan matični broj!");
                maticni = value;
            }
        }

        public string Spol
        {
            get
            {
                return spol;
            }

            set
            {
                if (value != "M" && value != "Ž")
                    throw new InvalidOperationException("Neispravna oznaka za spol!");
                spol = value;
            }
        }

        public string Adresa
        {
            get
            {
                return adresa;
            }

            set
            {
                adresa = value;
            }
        }

        public string BracnoStanje
        {
            get
            {
                return bracnoStanje;
            }

            set
            {
                bracnoStanje = value;
            }
        }

        public string ZdravstvenaKnjizica
        {
            get
            {
                return zdravstvenaKnjizica;
            }

            set
            {
                if (!value.StartsWith("ZD-"))
                    throw new InvalidOperationException("Neispravan format zdravstvene knjižice!");
                zdravstvenaKnjizica = value;
            }
        }

        public DateTime Rodenje
        {
            get
            {
                return rodenje;
            }

            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum rođenja ne može biti u budućnosti!");
                rodenje = value;
            }
        }

        public DateTime Prijem
        {
            get
            {
                return prijem;
            }

            set
            {
                prijem = value;
            }
        }

        public Karton Karton
        {
            get
            {
                return karton;
            }

            set
            {
                karton = value;
            }
        }

        public int BrojPosjeta
        {
            get
            {
                return brojPosjeta;
            }

            set
            {
                brojPosjeta = value;
            }
        }

        public string ZeljeniPregled
        {
            get
            {
                return zeljeniPregled;
            }

            set
            {
                zeljeniPregled = value;
            }
        }

        public Pacijent(string i, string p, DateTime r, string m, string s, string a, string b, string z, DateTime pr, string zdr)
        {
            Ime = i;
            Prezime = p;
            Rodenje = r;
            Maticni = m;
            Spol = s;
            Adresa = a;
            BracnoStanje = b;
            ZeljeniPregled = z;
            Prijem = pr;
            BrojPosjeta = 1;
            ZdravstvenaKnjizica = zdr;
        }
    }
}
