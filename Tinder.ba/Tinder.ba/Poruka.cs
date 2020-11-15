using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ba
{
    public class Poruka
    {
        #region Atributi

        Korisnik posiljalac, primalac;
        string sadrzaj;

        #endregion

        #region Properties

        public Korisnik Posiljalac
        {
            get => posiljalac;
        }
        public Korisnik Primalac
        {
            get => primalac;
        }
        public string Sadrzaj
        {
            get => sadrzaj;
            set
            {
                if (String.IsNullOrWhiteSpace(value) || value.Contains("pogrdna riječ"))
                    throw new InvalidOperationException("Neispravan sadržaj poruke!");

                sadrzaj = value;
            }
        }

        #endregion

        #region Konstruktor

        public Poruka(Korisnik sender, Korisnik receiver, string content)
        {
            if (sender == null || receiver == null)
                throw new ArgumentNullException("Nedefinisan pošiljalac/primalac!");

            posiljalac = sender;
            primalac = receiver;
            Sadrzaj = content;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda za izračunavanje potencijala poruke.
        /// Maksimalni potencijal je 100, a minimalni 0.
        /// Ukoliko poruka sadrži riječi "bježi", "neću", "oženjen", "udata" ili "neistina" potencijal se smanjuje za 20 po prisutnoj riječi.
        /// Ukoliko poruka sadrži riječi "volim", "ljubav", "slobodan", "slobodna" ili "hoću" potencijal se povećava za 20 po prisutnoj riječi.
        /// </summary>
        /// <returns></returns>
        public int IzračunajPotencijalPoruke()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
