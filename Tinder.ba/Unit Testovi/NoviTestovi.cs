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

        //Amira Kurtagic: definisala zamjenski objekat
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
        public void TestPotencijalChataIspodNule()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "udata sam!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, neću!"));
            int potencijal = t.PotencijalChata(chat);

            Assert.AreEqual(potencijal, 0);
        }

        [TestMethod]
        public void TestPotencijalChataIznadSto()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k3 = new Korisnik("user3", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "volim i ja tebe, to mora da je ljubav!"));
            chat.Poruke.Add(new Poruka(k1, k2, "slobodan sam, jesi li ti slobodna?"));
            chat.Poruke.Add(new Poruka(k2, k1, "slobodna sam i hoću tebe"));
            int potencijal = t.PotencijalChata(chat);

            Assert.AreEqual(potencijal, 100);
        }



        //Ajla Habib
        //ispituje velicinu liste(bez duplikata) ukoliko su dati kompatibilni korisnici
        [TestMethod]
        public void TestZaTinder()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 28, false, 19, 23);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);
            Assert.AreEqual(1, t.DajSveKompatibilneKorisnike().Count);

        }
        //Arijana Čolak
        //ispituje sadrzaj liste za kompatibilne korisnike
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

        //Arijana Čolak
        //ispituje da li metoda baca izuzetak ukoliko posaljemo nekompatibilne korisnike
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestZaTinderBacanjeIzuzetka()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Tuzla, Lokacija.Mostar, 20, false, 24, 30);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Banja_Luka, Lokacija.Bihać, 28, false, 19, 23);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();

            t.Korisnici.Add(k1);
            t.Korisnici.Add(k2);

            t.DajSveKompatibilneKorisnike();
        }

        //Ajla Habib
        //provjerava da li metoda vraca duplikate
        [TestMethod]
        public void TestZaTinderProvjeraDuplikata()
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

        //Amira Kurtagic
        //provjerava ponasanje metode prilikom pokusaja dodavanja korisnika koji vec postoji
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

        //Amira Kurtagic
        //provjera ponasanja metode kada se proslijedi korisnik koji ne postoji u listi (brisanje korisnika)
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

        //Amira Kurtagic
        //provjerava da li metoda ispravno brise korisnika
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

        //Amira Kurtagic
        //provjerava ponasanje metode ukoliko se obrise korisnik, da li se brise chat
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
        public void TestRadSaKorisnikomDodavanjeKorisnika()
        {
            Korisnik k1 = new Korisnik("user12", "user12*+", Lokacija.Bihać, Lokacija.Mostar, 23, false);
            Korisnik k2 = new Korisnik("user13", "user13*+", Lokacija.Zenica, Lokacija.Tuzla, 28, true);
            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.Korisnici.Add(k1);
            t.RadSaKorisnikom(k2, 0);
            Assert.AreEqual(t.Korisnici.Count, 2);
        }

        //Arijana Čolak
        //provjeravamo da li ce baciti izuzetak ukoliko posaljemo listu manju od 2 elementa
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
        //Arijana Čolak
        //provjerava da li ce metoda baciti izuzetak ukoliko posaljemo vrijednost za grupni chat: false
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
        //Ajla Habib
        //provjerava da li ce metoda baciti izuzetak ako se proslijedi grupni chat
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
        public void TestZaDatumPocetakChataIzuzetak()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "volim i ja tebe, to mora da je ljubav!"));
            DateTime value = new DateTime(2020, 12, 31);
            chat.PocetakChata = value;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestZaDatumNajnovijaPorukaIzuzetak()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "bježi, to je neistina"));
            DateTime value = new DateTime(2021, 03, 03);
            chat.NajnovijaPoruka = value;
        }
        //Amira Kurtagic
        //provjerava velicinu liste ukoliko ima jedna poruka gdje je posiljalac k1
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
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k1).Count);
        }

       //Arijana Čolak
       //provjerava da li je ispravan sadrzaj poruke
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
        //Ajla Habib
        //provjerava da li ce baciti izuzetak ukoliko se proslijedi korisnik koji nije posiljalac
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

        //Ajla Habib
        //provjerava velicinu liste poruka
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
            chat.Poruke.Add(new Poruka(k1, k2, "zbog čega me ne želiš?"));
            Assert.AreEqual(2, chat.DajSvePorukeOdKorisnika(k1).Count);
            Assert.AreEqual(1, chat.DajSvePorukeOdKorisnika(k2).Count);

        }
        #endregion
        #region Testovi za klasu Poruka
        //Ajla Habib
        //provjerava da li metoda baca izuzetak ako se proslijedi pogrdna rijec
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void TestSadrzajaPorukeIzuzetak()
        {
            Korisnik k1 = new Korisnik("user2", "user2*+", Lokacija.Mostar, Lokacija.Bihać, 23, false);
            Korisnik k2 = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 25, false);

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            t.DodavanjeRazgovora(new List<Korisnik>() { k1, k2 }, false);
            Chat chat = t.Razgovori[0];
            chat.Poruke.Add(new Poruka(k1, k2, "volim te!"));
            chat.Poruke.Add(new Poruka(k2, k1, "pogrdna riječ"));
        }
        //Ajla Habib
        //provjerava da li metoda baca izuzetak ukoliko proslijedimo vrijednost null za posiljaoca
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
        //Ajla Habib
        //provjerava rad metode ukoliko se roslijedi vrijednost null za primaoca
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

        //Amira Kurtagic
        //provjerava da li ce potencijal biti 0 koliko se posalju "negativne" rijeci
        [TestMethod]
        public void TestIzracunajPotencijalPoruke()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Mostar, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "bježi,udata!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(0, potencijal);
        }
       
        //Amira Kurtagic
        //provjerava potencijal ukoliko se posalju 2 "pozitivne" i jedna "negativna" rijec
        [TestMethod]
        public void TestPotencijalaPoruke2()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "slobodna sam i volim te, ali ti si oženjen!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(20, potencijal);
        }

        //Arijana Čolak
        //provjerava potencijal poruke ukoliko vrijednost potencijala ode u minus
        [TestMethod]
        public void TestPotencijalaPoruke3()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "bježi, neću tebe jer sam oženjen!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(0, potencijal);
        }
        //Arijana Čolak
        //provjerava da li je potencijal max
        [TestMethod]
        public void TestPotencijalaPoruke4()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Poruka poruka = new Poruka(k1, k2, "slobodna sam, a i ti si slobodan, pa te hoću jer je to ljubav i volim te!");
            int potencijal = poruka.IzračunajPotencijalPoruke();
            Assert.AreEqual(100, potencijal);
        }
        #endregion
        #region Testovi za klasu Korisnik
        //Ajla Habib
        //provjerava funkcionalnost metode
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
        //Amira Kurtagic
        //provjerava da li je korisnik razveden
        [TestMethod]
        public void TestDaLiJeRazveden()
        {
            Korisnik k1 = new Korisnik("user8", "user8*+", Lokacija.Sarajevo, Lokacija.Zenica, 39, false);
            k1.RazvediSe();
            Assert.IsTrue(k1.Razvod);

        }
        //Amira Kurtagic
        //provjerava da li vraca ispravnu zeljenu lokaciju ukoliko je lokacija iz Bosne
        [TestMethod]
        public void TestLokacijeRazvedenog()
        {
            Korisnik k = new Korisnik("user3", "user3*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 31, true);
            k.RazvediSe();
            Assert.AreNotEqual(Lokacija.Sarajevo, k.ZeljenaLokacija);
            Assert.AreEqual(Lokacija.Banja_Luka, k.ZeljenaLokacija);
        }
        //Amira Kurtagic
        //provjerava da li su zeljene godine u dozvoljenom opsegu
        [TestMethod]
        public void TestZeljenogBrojaGodina()
        {
            Korisnik k = new Korisnik("user4", "user4*+", Lokacija.Bihać, Lokacija.Bihać, 20, false);
            Korisnik k2 = new Korisnik("user5", "user5*+", Lokacija.Tuzla, Lokacija.Mostar, 45, false);
            Korisnik k3 = new Korisnik("user6", "user6*+", Lokacija.Banja_Luka, Lokacija.Sarajevo, 56, false);
            k.RazvediSe();
            k2.RazvediSe();
            k3.RazvediSe();
            Assert.AreNotEqual(23, k.ZeljeniMaxGodina);
            Assert.AreEqual(41, k2.ZeljeniMinGodina);
            Assert.AreNotEqual(23, k.ZeljeniMinGodina);
            Assert.AreEqual(60, k3.ZeljeniMaxGodina);
            Assert.AreNotEqual(50, k3.ZeljeniMinGodina);
        }

        //Arijana Čolak
        //provjerava ispravnost imena
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravnoime()
        {
            Korisnik k = new Korisnik("", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
        }
        //Arijana Čolak
        //provjerava ispravnost passworda
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravanPassword()
        {
            Korisnik k = new Korisnik("Niko", "pass", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
        }
        //Arijana Čolak
        //provjerava ispravnost godina
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneGodine()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 16, false);
            int god = k.Godine;
        }
        //Ajla Habib
        //provjerava ispravnost zeljenih min godina
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMinGodina()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMinGodina = k.Godine + 6;
        }
        //Ajla Habib
        //provjerava ispravnost zeljenih min godina
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMinGodina1()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMinGodina = k.Godine - 11;
        }
        //Ajla Habib
        //provjerava ispravnost max godina
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestKorisnikNeispravneZeljeniMaxGodina()
        {
            Korisnik k = new Korisnik("Niko", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            k.ZeljeniMaxGodina = k.Godine - 6;
        }
        //Ajla Habib
        //provjerava ispravnost zeljenih max godina
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
        public void TestZaKonstruktor()
        {
            GrupniChat chat = new GrupniChat(null);
            Assert.AreEqual(chat.Korisnici.Count, 0);
  
        }
        //Arijana Čolak
        //provjerava bacanje izuzetka ukoliko se proslijedi prazan string
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
        //Amira Kurtagic
        //provjerava da li ce doci do izuzetka ukoliko se posalje lista ciji je br elemenata manji od 2 
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

        //Arijana Čolak
        //provjerava listu poruka ukoliko metoda uspjesno izvrsi funkcionalnost
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
        //Arijana Čolak
        //provjerava bacanje izuzetka - metoda nije implementirana (nije implementirana zbog 2 zadatka)
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

