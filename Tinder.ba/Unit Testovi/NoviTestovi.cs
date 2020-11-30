using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tinder.ba;

namespace Unit_Testovi
{
    [TestClass]
    public class NoviTestovi
    {
        Recenzija r;
        #region Zamjenski Objekti
      
        public class  Stub: IRecenzija
        {
            public string DajUtisak()
            {
                return "Pozitivan";
            }
        }

        [TestMethod]
        public void TestZamjenskiObjekti()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Chat chat = new Chat(k1, k2);;
            chat.Poruke.Add(new Poruka(k1, k2, "volim te, slobodan sam i slobodna si! hoću ljubav!"));
            IRecenzija r = new Stub();
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            bool uspješnost = t.DaLiJeSpajanjeUspjesno(chat, new Stub());

            Assert.IsTrue(uspješnost);
        }

        #endregion

        #region TDD

        [TestMethod]
        public void TestPotencijalChataVeliki()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "slobodna sam, volim i ja tebe!"));

            int potencijal = t.PotencijalChata(chat);

            Assert.AreEqual(potencijal, 60);
        }

        [TestMethod]
        public void TestPotencijalChataMali()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));
            chat.DajSvePorukeOdKorisnika(k1);
            Assert.AreEqual(chat.DajSvePorukeOdKorisnika(k3).Count, 0);
            int potencijal = t.PotencijalChata(chat);

            Assert.AreEqual(potencijal, 20);
        }
        [TestMethod]
        public void TestZaTinder()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 28, false, 19, 23);
            //Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            Assert.AreEqual(1, t.DajSveKompatibilneKorisnike().Count);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestPotencijalChataIzuzetak()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, true);
            GrupniChat chat = (GrupniChat)t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));

            int potencijal = t.PotencijalChata(chat);
        }

        [TestMethod]
        public void TestPotencijalaPoruke1()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "bježi,udata sam!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(0, potencijal);
        }
        [TestMethod]
        public void TestPotencijalaPoruke2()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "slobodna sam i volim te, ali ti si oženjen!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(20, potencijal);
        }

        [TestMethod]
        public void TestPotencijalaPoruke3()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "bježi, neću tebe jer sam oženjen!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(0, potencijal);
        }

        [TestMethod]
        public void TestPotencijalaPoruke4()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "slobodna sam, a i ti si slobodan, pa te hoću jer je to ljubav i volim te.!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(100, potencijal);
        }

        [TestMethod]
        public void TestDajSvePorukeOdKorisnika1()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k1).Count);
        }
        [TestMethod]
        public void TestDajSvePorukeOdKorisnikaSadrzajPoruke()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));
            Assert.AreEqual("volim te!", chat.DajSvePorukeOdKorisnika(k1)[0].Sadrzaj);
            Assert.AreEqual("bježi, neću!", chat.DajSvePorukeOdKorisnika(k2)[0].Sadrzaj);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDajSvePorukeOdKorisnikaIzuzetak()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, true);
            GrupniChat chat = (GrupniChat)t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.DajSvePorukeOdKorisnika(k2);
        }

        [TestMethod]
        public void TestDajSvePorukeOdKorisnikaVelicinaListe()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));
            chat.Poruke.Add(new Poruka(k1, k2, "zbog čega me ne želiš?"));
            Assert.AreEqual(2, chat.DajSvePorukeOdKorisnika(k1).Count);
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k2).Count);

        }

        [TestMethod]
        public void TestZaTinderDaLiJeSadrzajListeDobar()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 28, false, 19, 23);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();

            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);

            var par = Tuple.Create(k1, k2);
            var lista = t.DajSveKompatibilneKorisnike();
            Assert.IsTrue(lista[0].Item1 == par.Item2 && lista[0].Item2 == par.Item1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestZaTinderBBacanjeIzuzetka()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Tuzla, Lokacija.Mostar, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Banja_Luka, Lokacija.Bihać, 28, false, 19, 23);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();

            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);

            t.DajSveKompatibilneKorisnike();
        }
        [TestMethod]
        public void TestZaTinderDuzinaListe2()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Banja_Luka, 28, false, 19, 23);
            Korisnik k3 = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 35, false, 38, 40);
            Korisnik k4 = new Korisnik("user4", "user4*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 28, false, 19, 23);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            t.Korisnici.Add(k3);
            t.Korisnici.Add(k4);

            Assert.AreEqual(1, t.DajSveKompatibilneKorisnike().Count);
        }

        [TestMethod]
        public void TestRazvediSe()
        {
            Korisnik k = new Korisnik("user1", "user1*+", Lokacija.Trebinje, Lokacija.Trebinje, 25, false);
            k.RazvediSe();
            Assert.AreEqual(Lokacija.Mostar, k.ZeljenaLokacija);
            Assert.AreEqual(true, k.Razvod);
            Assert.AreEqual(29, k.ZeljeniMaxGodina);
            Assert.AreEqual(21, k.ZeljeniMinGodina);
        }

        [TestMethod]
        public void TestDaLiJeRazveden()
        {
            Korisnik k1 = new Korisnik("user8", "user8*+", Lokacija.Sarajevo, Lokacija.Zenica, 39, true);
            Korisnik k2 = new Korisnik("user9", "user9*+", Lokacija.Tuzla, Lokacija.Mostar, 27, false);
            k1.RazvediSe();
            Assert.IsTrue(k1.Razvod);
            Assert.IsFalse(k2.Razvod);

        }
        [TestMethod]
        public void TestLokacijeRazvedenog()
        {
            Korisnik k = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 31, true);
            k.RazvediSe();
            Assert.AreNotEqual(Lokacija.Sarajevo, k.ZeljenaLokacija);
            Assert.AreEqual(Lokacija.Banja_Luka, k.ZeljenaLokacija);
        }
        [TestMethod]
        public void TestŽeljenogBrojaGodina()
        {
            Korisnik k = new Korisnik("user4", "user4*+", Lokacija.Bihać, Lokacija.Bihać, 20, false);
            Korisnik k2 = new Korisnik("user5", "user5*+", Lokacija.Tuzla, Lokacija.Mostar, 45, true);
            Korisnik k3 = new Korisnik("user6", "user6*+", Lokacija.Banja_Luka, Lokacija.Sarajevo, 56, false);
            k.RazvediSe();
            k2.RazvediSe();
            k3.RazvediSe();
            Assert.AreEqual(24,k.ZeljeniMaxGodina);
            Assert.AreNotEqual(23,k.ZeljeniMaxGodina);
            Assert.AreEqual(41,k2.ZeljeniMinGodina);
            Assert.AreNotEqual(23,k.ZeljeniMinGodina);
            Assert.AreEqual(60, k3.ZeljeniMaxGodina);
            Assert.AreNotEqual(50, k3.ZeljeniMinGodina);
        }

        [TestMethod]
        public void TestDajSvePorukeOdKorisnikaBrojPoruka()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "slobodan sam i volim te"));
            chat.Poruke.Add(new Poruka(k2, k1, "udata sam i bježi"));
            chat.DajSvePorukeOdKorisnika(k1);
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k1).Count);
        }

        #endregion
    }

}
