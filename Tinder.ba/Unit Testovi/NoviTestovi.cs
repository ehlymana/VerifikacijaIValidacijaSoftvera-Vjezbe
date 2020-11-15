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

        [TestMethod]
        public void TestZamjenskiObjekti()
        {
            Korisnik k1 = new Korisnik("user1", "user1*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);
            Korisnik k2 = new Korisnik("user2", "user2*+", Lokacija.Sarajevo, Lokacija.Sarajevo, 20, false);

            Chat chat = new Chat(k1, k2);
            chat.Poruke.Add(new Poruka(k1, k2, "volim te, slobodan sam i slobodna si! hoću ljubav!"));
            IRecenzija r = new Recenzija();

            Tinder.ba.Tinder t = new Tinder.ba.Tinder();
            bool uspješnost = t.DaLiJeSpajanjeUspjesno(chat, r);

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

        #endregion
    }
}
