using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public class Lokacija
    {
        #region Atributi

        string naziv, adresa, grad, država;
        int brojUlice, poštanskiBroj;
        double površina;

        #endregion

        #region Properties

        public string Naziv 
        { 
            get => naziv;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Naziv ne smije biti prazan!");
                naziv = value;
            }
        }

        public string Adresa 
        { 
            get => adresa;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Adresa ne smije biti prazna!");

                adresa = value;
            }
        }

        public string Grad 
        { 
            get => grad;
            set
            {
                List<string> podrzaniGradovi = new List<string>()
                { "Sarajevo", "Zenica", "Bihać", "Tuzla", "Mostar", "Banja Luka", "Trebinje",
                  "Zagreb", "Split", "Zadar", "Rijeka", "Pula" };
                if (!podrzaniGradovi.Contains(value))
                    throw new ArgumentException("Unijeli ste grad koji trenutno nije podržan!");

                grad = value;
            }
        }
        public string Država 
        { 
            get => država;
            set
            {
                if (value != "Bosna i Hercegovina" && value != "Hrvatska")
                    throw new ArgumentException("Unijeli ste državu koja trenutno nije podržana!");

                država = value;
            }
        }
        public int BrojUlice 
        { 
            get => brojUlice;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Broj ulice ne može biti manji od 1!");

                brojUlice = value;
            }
        }
        public int PoštanskiBroj 
        { 
            get => poštanskiBroj;
            set
            {
                if (value < 10000 || value > 99999)
                    throw new ArgumentException("Unijeli ste pogrešan poštanski broj!");

                poštanskiBroj = value;
            }
        }
        public double Površina 
        { 
            get => površina;
            set
            {
                if (value < 0.01)
                    throw new ArgumentException("Površina zemljišta mora biti barem 0.01 m2!");

                površina = value;
            }
        }

        #endregion

        #region Konstruktor

        public Lokacija(List<string> parametri, double površina)
        {
            if (površina < 0.01)
                throw new ArgumentException("Površina zemljišta mora biti barem 0.01 m2!");
            else if (parametri.Any(p => p.Length < 1))
                throw new ArgumentException("Nijedan podatak o lokaciji ne smije biti prazan!");

            Površina = površina;
            Naziv = parametri.ElementAt(0);
            Adresa = parametri.ElementAt(1);

            int i = 2;
            if (parametri.Count == 6)
            {
                BrojUlice = Int32.Parse(parametri.ElementAt(i));
                i++;
            }
            else if(parametri.Count != 5)
                throw new ArgumentException("Neispravan broj parametara!");

            Grad = parametri.ElementAt(i);
            i++;
            PoštanskiBroj = Int32.Parse(parametri.ElementAt(i));
            i++;
            Država = parametri.ElementAt(i);
        }

        #endregion
    }
}
