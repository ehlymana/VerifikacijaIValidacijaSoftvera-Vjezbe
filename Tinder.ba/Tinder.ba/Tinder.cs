using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ba
{
    public class Tinder
    {
        #region Atributi

        List<Korisnik> korisnici;
        List<Chat> razgovori;

        #endregion

        #region Properties

        public List<Korisnik> Korisnici
        {
            get => korisnici;
        }

        public List<Chat> Razgovori
        {
            get => razgovori;
        }

        #endregion

        #region Konstruktor

        public Tinder()
        {
            korisnici = new List<Korisnik>();
            razgovori = new List<Chat>();
        }

        #endregion

        #region Metode

        public void RadSaKorisnikom(Korisnik k, int opcija)
        {
            if (opcija == 0)
            {
                Korisnik postojeci = korisnici.Find(korisnik => korisnik.Ime == k.Ime);
                if (postojeci != null)
                    throw new InvalidOperationException("Korisnik već postoji!");

                korisnici.Add(k);
            }
            else if (opcija == 1)
            {
                Korisnik postojeci = korisnici.Find(korisnik => korisnik.Ime == k.Ime);
                if (postojeci == null)
                    throw new InvalidOperationException("Korisnik ne postoji!");

                korisnici.Remove(k);

                List<Chat> razgovoriZaBrisanje = new List<Chat>();
                foreach (Chat c in razgovori)
                {
                    if (c.Korisnici.Find(korisnik => korisnik.Ime == k.Ime) != null)
                        razgovoriZaBrisanje.Add(c);
                }

                foreach (Chat brisanje in razgovoriZaBrisanje)
                    razgovori.Remove(brisanje);
            }
        }

        public void DodavanjeRazgovora(List<Korisnik> korisnici, bool grupniChat)
        {
            if (korisnici == null || korisnici.Count < 2 || (!grupniChat && korisnici.Count > 2))
                throw new ArgumentException("Nemoguće dodati razgovor!");

            if (grupniChat)
                razgovori.Add(new GrupniChat(korisnici));

            else
                razgovori.Add(new Chat(korisnici[0], korisnici[1]));
        }

        /// <summary>
        /// Metoda u kojoj se određuju i vraćaju svi kompatibilni korisnici u listi parova.
        /// Parovi ne smiju imati duplikate - dva para ne smiju imati ista dva korisnika.
        /// Ukoliko nema nijednog korisnika, baca se izuzetak.
        /// Korisnici su kompatibilni ako lokacija k1 odgovara željenoj lokaciji k2 i obrnuto
        /// i ukoliko se godine k1 nalaze između minimalnih i maksimalnih željenih godina k2 i obrnuto.
        /// </summary>
        /// <returns></returns>
        //Amira Kurtagić
        public List<Tuple<Korisnik, Korisnik>> DajSveKompatibilneKorisnike()
        {
            List<Tuple<Korisnik, Korisnik>> lista = new List<Tuple<Korisnik, Korisnik>>();
            foreach (Korisnik x in korisnici)
            {
                for(int i = 0; i < korisnici.Count; i++)
                {
                    if (x.Lokacija.Equals(korisnici[i].ZeljenaLokacija) && korisnici[i].Lokacija.Equals(x.ZeljenaLokacija)
                        && ((x.Godine > korisnici[i].ZeljeniMinGodina && x.Godine < korisnici[i].ZeljeniMaxGodina) && (korisnici[i].Godine > x.ZeljeniMinGodina && korisnici[i].Godine < x.ZeljeniMaxGodina)))
                        if (x.Ime.Equals(korisnici[i].Ime) && x.Password.Equals(korisnici[i].Password) && x.Godine.Equals(korisnici[i].Godine)) continue;  
                       else lista.Add(Tuple.Create(x, korisnici[i]));
                }
            }
            if (lista.Count > 0)
            {
                foreach (Tuple<Korisnik, Korisnik> x in lista.ToArray())
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (x.Item1 == lista[i].Item2 && x.Item2 == lista[i].Item1) lista.Remove(x);
                    }
                }
            }
            if (lista.Count == 0) throw new Exception();
            else return lista;
        }

        public bool DaLiJeSpajanjeUspjesno(Chat c, IRecenzija r)
        {
            if (c is GrupniChat)
                throw new InvalidOperationException("Grupni chatovi nisu podržani!");

            if (c.Poruke.Find(poruka => poruka.IzračunajPotencijalPoruke() == 100) != null
                && r.DajUtisak() == "Pozitivan")
                return true;

            else
                return false;
        }

        //Arijana Čolak, Ajla Habib, Amira Kurtagić
        //prolazi kroz sve poruke iz chata i na potencijal chata dodaje potencijale poruka posebno
        public int PotencijalChata(Chat c)
        {
            if (c is GrupniChat) throw new InvalidOperationException();
            int potencijal = 0;
            foreach(Poruka x in c.Poruke)
            {
                potencijal += x.IzračunajPotencijalPoruke();
            }
            if (potencijal > 100) potencijal = 100;
            else if (potencijal < 0) potencijal = 0;
            return potencijal;
        }
        #endregion
    }
}
