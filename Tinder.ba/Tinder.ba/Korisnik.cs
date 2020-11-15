using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ba
{
    public enum Lokacija
    {
        Sarajevo,
        Zenica,
        Mostar,
        Tuzla,
        Bihać,
        Trebinje,
        Banja_Luka
    }

    public class Korisnik
    {
        #region Atributi

        string ime, password;
        Lokacija lokacija, zeljenaLokacija;
        int godine, zeljeniMinGodina, zeljeniMaxGodina;
        bool razvod;

        #endregion

        #region Properties

        public string Ime
        {
            get => ime;
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length > 20)
                    throw new FormatException("Neispravno ime!");

                ime = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (String.IsNullOrEmpty(value) || value.Length < 5
                    || !value.Contains("*") || !value.Contains("+") || value.Contains("etf"))
                    throw new FormatException("Neispravan password!");

                password = value;
            }
        }

        public Lokacija Lokacija
        {
            get => lokacija;
            set => lokacija = value;
        }

        public Lokacija ZeljenaLokacija
        {
            get => zeljenaLokacija;
            set => zeljenaLokacija = value;
        }

        public int Godine
        {
            get => godine;
            set
            {
                if (value < 18)
                    throw new FormatException("Neispravne godine!");

                godine = value;
            }
        }

        public int ZeljeniMinGodina
        {
            get => zeljeniMinGodina;
            set
            {
                if (value < godine - 10 || value > godine + 5)
                    throw new FormatException("Neispravni željeni minimum godina!");

                zeljeniMinGodina = value;
            }
        }

        public int ZeljeniMaxGodina
        {
            get => zeljeniMaxGodina;
            set
            {
                if (value < godine - 5 || value > godine + 10)
                    throw new FormatException("Neispravni željeni maksimum godina!");

                zeljeniMaxGodina = value;
            }
        }

        public bool Razvod
        {
            get => razvod;
            set => razvod = value;
        }

        #endregion

        #region Konstruktor

        public Korisnik(string name, string pass, Lokacija location, Lokacija desiredLoc, int age, bool divorced, int minDesiredAge = 0, int maxDesiredAge = 0)
        {
            Ime = name;
            Password = pass;
            Lokacija = location;
            ZeljenaLokacija = desiredLoc;
            Godine = age;

            if (minDesiredAge < 18)
                ZeljeniMinGodina = age;
            else
                ZeljeniMinGodina = minDesiredAge;

            if (maxDesiredAge < 18)
                ZeljeniMaxGodina = age;
            else
                ZeljeniMaxGodina = maxDesiredAge;

            Razvod = divorced;
        }

        public Korisnik()
        {

        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda kojom se registruje da se korisnik razveo.
        /// Osim postavljanja parametra za razvod, minimalni broj željenih godina se postavlja na godine - 4,
        /// a maksimalni broj željenih godina na godine + 4.
        /// Željena lokacija se postavlja na Mostar za sve gradove iz Hercegovine, a na Banja Luku za sve gradove
        /// iz Bosne.
        /// </summary>
        public void RazvediSe()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
