using System;

namespace AutoRentApp
{
    public class StraniKlijent: Klijent
    {
        #region Atributi

        const Double CIJENA_KAUCIJE = 100; 

        String adresa, grad, drzava;

        #endregion

        #region Properties

        public String Adresa
        {
            get => adresa;
            set => adresa = value;
        }

        public String Grad
        {
            get => grad;
            set => grad = value;
        }

        public String Drzava
        {
            get => drzava;
            set => drzava = value;
        }

        #endregion

        #region Konstruktor

        public StraniKlijent(String i, String p, DateTime dr, String a, String g, String d): base(i, p, dr)
        { 
            Adresa = a;
            Grad = g;
            Drzava = d;
        }

        #endregion

        #region Metode

        // <description>
        // Vraćanje cijene kaucije za stranog klijenta
        // </description>
        public override Double VratiCijenuKaucije()
        {
            return this.DatumRodenja.Day;
        }

        // <description>
        // Definisanje tekstualnog opisa stranog klijenta
        // </description>
        public override string ToString()
        {
            return Id + " - " + Ime + " " + Prezime + ", " + DatumRodenja.Date.ToString("dd.MM.yyyy") + " - " + Adresa + ", " + Grad + ", " + Drzava; 
        }

        #endregion
    }
}
