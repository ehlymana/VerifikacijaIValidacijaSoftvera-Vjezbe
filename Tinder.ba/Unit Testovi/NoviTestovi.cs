using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tinder.ba;

namespace Unit_Testovi
{
    [TestClass]
    public class NoviTestovi
    {
        #region Zamjenski Objekti

        public class Stub : IRecenzija
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

            Chat chat = new Chat(k1, k2);
            chat.Poruke.Add(new Poruka(k1, k2, "volim te, slobodan sam i slobodna si! hoću ljubav!"));
            IRecenzija r = new Stub();
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            bool uspješnost = t.DaLiJeSpajanjeUspjesno(chat, r);

            Assert.IsTrue(uspješnost);
        }

        #endregion
        #region Testovi za klasu Tinder

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
            int potencijal = t.PotencijalChata(chat);

            Assert.AreEqual(potencijal, 20);
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void RadSaKorisnikomTinderIzuzetakKorisnikNePostoji()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            t.RadSaKorisnikom(k1, 0);
            t.RadSaKorisnikom(k2, 0);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RadSaKorisnikomTinderIzuzetakKorisnikPostoji()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.RadSaKorisnikom(k2, 1);

        }

        [TestMethod]
        public void RadSaKorisnikomTinderBrisanjeKorisnika()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.RadSaKorisnikom(k1, 1);
            Assert.AreEqual(t.Korisnici.Count, 0);
        }

        [TestMethod]
        public void RadSaKorisnikomTinderBrisanjeChata()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "slobodan sam i volim te"));
            chat.Poruke.Add(new Poruka(k2, k1, "udata sam i bježi"));
            t.RadSaKorisnikom(k1, 1);
            Assert.AreEqual(t.Razgovori.Count, 0);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DodavanjeRazgovoraTinderIzuzetak1()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.DodavanjeRazgovora(t.Korisnici, false);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DodavanjeRazgovoraTinderIzuzetak2()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Korisnik k3 = new Korisnik("user14", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            t.Korisnici.Add(k3);
            t.DodavanjeRazgovora(t.Korisnici, false);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DaLiJeSpajanjeUspjesnoTinderIzuzetak()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Korisnik k3 = new Korisnik("user14", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, true);
            GrupniChat chat = (GrupniChat)t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "slobodan sam i volim te"));
            chat.Poruke.Add(new Poruka(k2, k1, "udata sam i bježi"));

            t.DaLiJeSpajanjeUspjesno(chat, new Stub());
        }
        #endregion
        #region Testovi za klasu Chat
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestSadržajaPorukeIzuzetak()
        {
            Korisnik k1 = new Korisnik("user2", "user2*+", Lokacija.Mostar, Lokacija.Bihać, 23, false);
            Korisnik k2 = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 25, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "pogrdna riječ"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestImaLiPorukaPošiljaoca()
        {
            Korisnik k1 = new Korisnik("user2", "user2*+", Lokacija.Mostar, Lokacija.Bihać, 23, false);
            Korisnik k2 = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 25, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(null, k2, "volim te!"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void TestImaLiPorukaPrimaoca()
        {
            Korisnik k1 = new Korisnik("user2", "user2*+", Lokacija.Mostar, Lokacija.Bihać, 23, false);
            Korisnik k2 = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 25, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, null, "bježi"));
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

        [TestMethod]
        public void TestDajSvePorukeOdKorisnika1()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, necu!"));
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
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, necu!"));
            Assert.AreEqual("volim te!", chat.DajSvePorukeOdKorisnika(k1)[0].Sadrzaj);
            Assert.AreEqual("bježi, necu!", chat.DajSvePorukeOdKorisnika(k2)[0].Sadrzaj);
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
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, necu!"));
            chat.Poruke.Add(new Poruka(k1, k2, "zbog cega me ne želiš?"));
            Assert.AreEqual(2, chat.DajSvePorukeOdKorisnika(k1).Count);
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k2).Count);

        }
        #endregion
        #region Testovi za klasu Poruka
        [TestMethod]
        public void TestIzračunajPotencijalPoruke()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Mostar, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "bježi,udata!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(0, potencijal);
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

            Poruka poruka = new Poruka(k1, k2, "bježi, necu tebe jer sam oženjen!");
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
        #endregion
        #region Testovi za klasu Korisnik
        [TestMethod]
        public void TestRazvediSe()
        {
            Korisnik k = new Korisnik("user1", "user1*+", Lokacija.Trebinje, Lokacija.Trebinje, 25, false);
            k.RazvediSe();
            Assert.AreEqual(k.ZeljenaLokacija, Lokacija.Mostar);
            Assert.AreEqual(k.Razvod, true);
            Assert.AreEqual(k.ZeljeniMaxGodina, 29);
            Assert.AreEqual(k.ZeljeniMinGodina, 21);
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
            Assert.AreEqual(24, k.ZeljeniMaxGodina);
            Assert.AreNotEqual(23, k.ZeljeniMaxGodina);
            Assert.AreEqual(41, k2.ZeljeniMinGodina);
            Assert.AreNotEqual(23, k.ZeljeniMinGodina);
            Assert.AreEqual(60, k3.ZeljeniMaxGodina);
            Assert.AreNotEqual(50, k3.ZeljeniMinGodina);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravnoime()
        {
            Korisnik k = new Korisnik("", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravanPassword()
        {
            Korisnik k = new Korisnik("Niko", "pass", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneGodine()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 16, false);
            int god = k.Godine;
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMinGodina()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMinGodina = k.Godine + 6;
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMinGodina1()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMinGodina = k.Godine - 11;
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMaxGodina()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMaxGodina = k.Godine - 6;
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMaxGodina1()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMaxGodina = k.Godine + 11;
        }

        #endregion
        #region Testovi za klasu Grupni chat
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void TestPosaljiPorukuViseKorisnika()
        {
            Korisnik k = new Korisnik();
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici.Add(k1);
            korisnici.Add(k2);
            GrupniChat g = new GrupniChat(korisnici);

            g.PosaljiPorukuViseKorisnika(k, korisnici, "");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PosaljiPorukuViseKorisnikaIzuzetak()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Korisnik k3 = new Korisnik("user14", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            GrupniChat chat = new GrupniChat(t.Korisnici);
            chat.PosaljiPorukuViseKorisnika(k3, t.Korisnici, "Poruka koja se salje");
        }

        [TestMethod]
        public void PosaljiPorukaViseKorisnikaTest()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Korisnik k3 = new Korisnik("user14", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            t.Korisnici.Add(k3);
            GrupniChat chat = new GrupniChat(t.Korisnici);
            chat.PosaljiPorukuViseKorisnika(k1, t.Korisnici, "Poruka koja se salje");
            Assert.AreEqual(chat.Poruke.Count, 3);
        }

        #endregion
        #region Testovi za klasu Recenzija
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestBacaLiRecenzijaIzuzetak()
        {
            Recenzija r = new Recenzija();
            r.DajUtisak();
        }
        #endregion
    }
}

