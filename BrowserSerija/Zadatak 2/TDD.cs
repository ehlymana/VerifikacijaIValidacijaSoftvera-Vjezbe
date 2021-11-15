using BrowserSerija;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Zadatak_2
{
    [TestClass]
    public class TDD
    {
        #region Zamjenski objekat

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Glumac g1 = new Glumac("Will Smith", "SAD", DateTime.Parse("25/09/1968"));
            Glumac g2 = new Glumac("Neki lažni glumac", "Neka lažna zemlja", DateTime.Now.AddDays(-1));
            Serija s = new Serija("The Fresh Prince of Bel Air", "Serija iz SAD za sve generacije!", Žanr.Američka, new List<Glumac>() { g1, g2 });
            Browser browser = new Browser();
            browser.RadSaSerijama(s, 1);
            
            BazaFilmova baza = new BazaFilmova();
            browser.PrekontrolišiSerije(baza);

            Assert.AreEqual(browser.Serije[0].Glumci.Count, 1);
            Assert.IsTrue(browser.Serije[0].Glumci.Contains(g1));
            Assert.IsFalse(browser.Serije[0].Glumci.Contains(g2));
        }

        #endregion

        #region TDD

        [TestMethod]
        public void AutomatskoDodavanjeSvihSerija()
        {
            Serija s1 = new Serija("Ljubav u New Delhiju", "Cijela Indija gleda najpopularniju romantičnu seriju", Žanr.Indijska);
            Serija s2 = new Serija("Ljubav u New Yorku", "Pridružite se cijeloj SAD koja već peti put gleda najbolju seriju svih vremena", Žanr.Američka);
            Browser browser = new Browser();
            browser.RadSaSerijama(s1, 1);
            browser.RadSaSerijama(s2, 1);
            browser.DodajGledanostEpizode(s1, false, 10000);
            browser.DodajGledanostEpizode(s2, false, 1000);
            Assert.AreEqual(browser.Serije.Count, 2);
            Assert.AreEqual(browser.Rasporedi.Count, 0);
            Assert.IsTrue(Math.Abs(browser.Serije[0].PopularnostSerije - 10) < 0.1);
            Assert.IsTrue(Math.Abs(browser.Serije[1].PopularnostSerije - 1) < 0.1);
            
            browser.AutomatskoDodavanjeRasporeda();
            
            Assert.AreEqual(browser.Rasporedi.Count, 1);
            Assert.IsTrue(browser.Rasporedi[0].Serije.Contains(s1));
            Assert.IsTrue(browser.Rasporedi[0].Serije.Contains(s2));
        }

        [TestMethod]
        public void AutomatskoDodavanjeSamoPopularnihSerija()
        {
            Serija s1 = new Serija("Ljubav u New Delhiju", "Cijela Indija gleda najpopularniju romantičnu seriju", Žanr.Indijska);
            Serija s2 = new Serija("Ljubav u New Yorku", "Pridružite se cijeloj SAD koja već peti put gleda najbolju seriju svih vremena", Žanr.Američka);
            Browser browser = new Browser();
            browser.RadSaSerijama(s1, 1);
            browser.RadSaSerijama(s2, 1);
            browser.DodajGledanostEpizode(s1, false, 10000);
            browser.DodajGledanostEpizode(s2, false, 1000);
            Assert.AreEqual(browser.Serije.Count, 2);
            Assert.AreEqual(browser.Rasporedi.Count, 0);
            Assert.IsTrue(Math.Abs(browser.Serije[0].PopularnostSerije - 10) < 0.1);
            Assert.IsTrue(Math.Abs(browser.Serije[1].PopularnostSerije - 1) < 0.1);

            browser.AutomatskoDodavanjeRasporeda(true);

            Assert.AreEqual(browser.Rasporedi.Count, 1);
            Assert.IsTrue(browser.Rasporedi[0].Serije.Contains(s1));
            Assert.IsFalse(browser.Rasporedi[0].Serije.Contains(s2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NemogućeDodavanjeRasporeda()
        {
            Serija s1 = new Serija("Ljubav u New Delhiju", "Cijela Indija gleda najpopularniju romantičnu seriju", Žanr.Indijska);
            Serija s2 = new Serija("Ljubav u New Yorku", "Pridružite se cijeloj SAD koja već peti put gleda najbolju seriju svih vremena", Žanr.Američka);
            Browser browser = new Browser();

            browser.AutomatskoDodavanjeRasporeda();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NemogućeDodavanjePopularnihSerija()
        {
            Serija s1 = new Serija("Ljubav u New Delhiju", "Cijela Indija gleda najpopularniju romantičnu seriju", Žanr.Indijska);
            Serija s2 = new Serija("Ljubav u New Yorku", "Pridružite se cijeloj SAD koja već peti put gleda najbolju seriju svih vremena", Žanr.Američka);
            Browser browser = new Browser();
            browser.RadSaSerijama(s1, 1);
            browser.RadSaSerijama(s2, 1);
            browser.DodajGledanostEpizode(s1, false, 10000);
            browser.DodajGledanostEpizode(s2, false, 1000);
            browser.RadSaSerijama(s1, 3);

            browser.AutomatskoDodavanjeRasporeda(true);
        }

        #endregion
    }
}
