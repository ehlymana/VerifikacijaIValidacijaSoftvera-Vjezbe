using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ba
{
    public class GrupniChat : Chat
    {
        #region Konstruktor

        public GrupniChat(List<Korisnik> users)
            : base()
        {
            if (users == null)
                korisnici = new List<Korisnik>();

            else
                korisnici = users;

            poruke = new List<Poruka>();
            PocetakChata = DateTime.Now;
        }

        #endregion

        #region Metode

        public void PosaljiPorukuViseKorisnika(Korisnik sender, List<Korisnik> receivers, string content)
        {
            if (sender == null || receivers == null || receivers.Count < 2 || String.IsNullOrEmpty(content))
                throw new FormatException("Neispravni parametri!");

            foreach (Korisnik k in receivers)
            {
                Poruke.Add(new Poruka(sender, k, content));
                NajnovijaPoruka = DateTime.Now;
            }
        }

        #endregion
    }
}
