using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public class Proizvod
    {
        #region Atributi

        string ime, opis, vrsta;
        Zivotinja proizvođač;
        DateTime datumProizvodnje, rokTrajanja;
        int količinaNaStanju;

        #endregion

        #region Properties

        public string Ime { get => ime; set => ime = value; }
        public string Opis { get => opis; set => opis = value; }
        public string Vrsta 
        { 
            get => vrsta;
            set
            {
                List<string> podržaneVrste = new List<string>()
                { "Mlijeko", "Jaja", "Vuna", "Sir" };
                if (!podržaneVrste.Contains(value))
                    throw new InvalidOperationException("Unijeli ste vrstu proizvoda koja ne postoji!");
                vrsta = value;
            }
        }
        public Zivotinja Proizvođač 
        { 
            get => proizvođač;
            set
            {
                bool proizvođačMlijeka = value.Vrsta == ZivotinjskaVrsta.Krava || value.Vrsta == ZivotinjskaVrsta.Ovca
                    || value.Vrsta == ZivotinjskaVrsta.Koza || value.Vrsta == ZivotinjskaVrsta.Magarac;
                bool proizvođačJaja = value.Vrsta == ZivotinjskaVrsta.Kokoška || value.Vrsta == ZivotinjskaVrsta.Patka
                    || value.Vrsta == ZivotinjskaVrsta.Guska;
                bool proizvođačVune = value.Vrsta == ZivotinjskaVrsta.Ovca;
                bool proizvođačSira = value.Vrsta == ZivotinjskaVrsta.Krava || value.Vrsta == ZivotinjskaVrsta.Ovca
                    || value.Vrsta == ZivotinjskaVrsta.Koza;
                if ((vrsta == "Mlijeko" && !proizvođačMlijeka) || (vrsta == "Jaja" && !proizvođačJaja)
                    || (vrsta == "Vuna" && !proizvođačVune) || (vrsta == "Sir" && !proizvođačSira))
                    throw new InvalidOperationException("Odabrana životinja ne može proizvoditi željenu vrstu proizvoda!");

                proizvođač = value;
            }
        }
        public DateTime DatumProizvodnje 
        { 
            get => datumProizvodnje;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum proizvodnje proizvoda ne može biti u budućnosti!");
                datumProizvodnje = value; 
            }
        }
        public DateTime RokTrajanja 
        { 
            get => rokTrajanja;
            set
            {
                if (value <= datumProizvodnje)
                    throw new InvalidOperationException("Rok trajanja mora biti nakon datuma proizvodnje!");
                rokTrajanja = value;
            }
        }
        public int KoličinaNaStanju
        { 
            get => količinaNaStanju;
            set
            {
                if (value < 1)
                    throw new InvalidOperationException("Količina ne smije biti manja od 1!");
                količinaNaStanju = value;
            }
        }

        #endregion

        #region Konstruktor

        public Proizvod(string ime, string opis, string vrsta, Zivotinja proizvođač, DateTime proizvodnja, DateTime rok, int kol)
        {
            Ime = ime;
            Opis = opis;
            Vrsta = vrsta;
            Proizvođač = proizvođač;
            DatumProizvodnje = proizvodnja;
            RokTrajanja = rok;
            KoličinaNaStanju = kol;
        }

        #endregion
    }
}
