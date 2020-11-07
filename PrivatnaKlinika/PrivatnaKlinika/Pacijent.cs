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
                if (String.IsNullOrEmpty(value)) throw new FormatException("Ime ne smije biti prazno!");
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
                if (String.IsNullOrEmpty(value)) throw new FormatException("Prezime ne smije biti prazno!");
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
                if (String.IsNullOrEmpty(value) || value.Length != 13) throw new FormatException("Matični broj ima tačno 13 karaktera!");
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
                if (value != "M" && value != "Ž") throw new FormatException("Spol se unosi kao M ili Ž!");
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
                if (String.IsNullOrEmpty(value)) throw new FormatException("Adresa ne smije biti prazna!");
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
