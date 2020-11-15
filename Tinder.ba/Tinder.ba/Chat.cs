using System;
using System.Collections.Generic;

namespace Tinder.ba
{
    public class Chat
    {
        #region Atributi

        protected List<Korisnik> korisnici;
        protected List<Poruka> poruke;
        DateTime pocetakChata, najnovijaPoruka;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Poruka> Poruke
        {
            get => poruke;
        }

        public DateTime PocetakChata
        {
            get => pocetakChata;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum početka ne može biti u budućnosti!");

                pocetakChata = value;
            }
        }

        public DateTime NajnovijaPoruka
        {
            get => najnovijaPoruka;
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidOperationException("Datum najnovije poruke ne može biti u budućnosti!");

                najnovijaPoruka = value;
            }
        }

        #endregion

        #region Konstruktor

        public Chat(Korisnik k1, Korisnik k2)
        {
            korisnici = new List<Korisnik>() { k1, k2 };
            poruke = new List<Poruka>();
            PocetakChata = DateTime.Now;
        }

        public Chat()
        {
            korisnici = new List<Korisnik>() { };
            poruke = new List<Poruka>();
            PocetakChata = DateTime.Now;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda za filtriranje poruka prema pošiljaocu.
        /// Ukoliko u chatu nema nijedne poruke ili je korisnik neispravan, baca se izuzetak.
        /// U suprotnom, vraća se lista svih poruka čiji je pošiljalac korisnik naveden u parametru.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public List<Poruka> DajSvePorukeOdKorisnika(Korisnik k)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
