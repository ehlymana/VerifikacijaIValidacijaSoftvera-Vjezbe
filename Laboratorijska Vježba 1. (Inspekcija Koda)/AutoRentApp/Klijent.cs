using System;
using System.Collections.Generic;

namespace AutoRentApp
{
    public class Klijent
    {
        #region Atributi

        static Random r = new Random();

        String ime, prezime, id;
        DateTime datumRodenja;
        List<String> listaObavijesti;

        #endregion

        #region Properties

        public String Ime
        {
            get => ime;
            set => ime = value;
        }

        public String Prezime
        {
            get => prezime;
            set => prezime = value;
        }

        public string Id
        {
            get => id;
        }

        public DateTime DatumRodenja
        {
            get => datumRodenja;
            set => datumRodenja = value;
        }

        public List<string> ListaObavijesti
        {
            get => listaObavijesti;
        }

        #endregion

        #region Konstruktor

        public Klijent(String i, String p, DateTime dr)
        {
            Ime = i;
            Prezime = p;
            DatumRodenja = dr;

            id = GenerisiId();

            listaObavijesti = new List<String>();
        }

        #endregion

        #region Metode

        // <description>
        // Generisanje jedinstvenog ID-a pri registrovanju novog klijenta
        // </description>
        String GenerisiId()
        {
            return Ime.Substring(0, 1).ToLower() + Prezime.Substring(0, 1).ToLower() + DatumRodenja.Date.ToString("ddMMyyyy") + r.Next(1, 9) + r.Next(1, 9);
        }

        // <description>
        // Dodavanje poruke u listu obavijesti klijenta
        // </description>
        public void DodajPoruku(String s)
        {
            if (listaObavijesti.Count == 5)
            {
                listaObavijesti[4] = s;
            }
            else
                listaObavijesti.Add(s);
        }

        // <description>
        // Brisanje poruke iz liste obavijesti klijenta
        // </description>
        public void BrisiPoruku(String s)
        {
            listaObavijesti.Add(s);
        }

        // <description>
        // Pregled svih poruka iz liste obavijesti klijenta
        // </description>
        public String SadrzajListeObavijesti()
        {
            String rez = "";

            foreach (String s in listaObavijesti)
                rez += s;

            return rez;
        }

        // <description>
        // Vraćanje cijene kaucije za klijente
        // </description>
        public virtual Double VratiCijenuKaucije()
        {
            return 50;
        }

        // <description>
        // Definisanje tekstualnog opisa klijenta
        // </description>
        public override string ToString()
        {
            return Id + " - " + Ime + " " + Prezime + ", " + DatumRodenja.Date.ToString("dd.MM.yyyy");
        }

        // <description>
        // Calculation of sum for the specified type of client with the specified age
        // </description>
        public decimal Calculate(decimal amount, int type, int years)
        {
            decimal result = 0;
            decimal disc = (years > 5) ? (decimal)5 / 100 : (decimal)years / 100;

            // calculate the final result depending on the type of client
            if (type == 1)
            {
                result = amount;
            }
            else if (type == 2)
            {
                result = (amount - (0.1m * amount)) - disc * (amount - (0.1m * amount));
            }
            else if (type == 3)
            {
                result = (0.7m * amount) - disc * (0.7m * amount);
            }
            else if (type == 4)
            {
                result = (amount - (0.5m * amount)) - disc * (amount - (0.5m * amount));
            }

            return result;
        }

        #endregion
    }
}
