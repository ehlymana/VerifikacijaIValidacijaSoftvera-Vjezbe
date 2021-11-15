using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ZivotinjskaFarma;

namespace Zadatak_2
{
    [TestClass]
    public class TDD
    {
        #region Zamjenski objekat

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Lokacija l = new Lokacija(new List<string>()
            { "Lokacija", "Ulica", "2", "Sarajevo", "71000", "Bosna i Hercegovina" }, 100);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddDays(-1), 5, 50, l);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddDays(-365), 140, 152, l);
            Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddDays(-3650), 50, 88, l);
            Farma f = new Farma();
            f.RadSaZivotinjama("Dodavanje", z1);
            f.RadSaZivotinjama("Dodavanje", z2);
            f.RadSaZivotinjama("Dodavanje", z3);
            z1.Proizvođač = false;
            z2.Proizvođač = false;
            z3.Proizvođač = false;
            
            Veterinar veterinar = new Veterinar();
            f.ObaviVeterinarskiPregled(veterinar);

            Assert.AreEqual(f.Zivotinje[0].Proizvođač, true);
            Assert.AreEqual(f.Zivotinje[1].Pregledi.Count, 1);
            Assert.AreEqual(f.Zivotinje[2].Proizvođač, false);
        }

        #endregion

        #region TDD

        [TestMethod]
        public void SpecijalizacijaFarmaKrava()
        {
            Farma f = new Farma();
            Lokacija staraStala = new Lokacija(new List<string>() { "Štala", "Seoski put", "12", "Split", "21000", "Hrvatska" }, 25.22);
            Zivotinja kokoska = new Zivotinja(ZivotinjskaVrsta.Kokoška, DateTime.Parse("01/01/2021"), 5.2, 26.44, staraStala);
            f.DodavanjeNoveLokacije(staraStala);
            f.RadSaZivotinjama("Dodavanje", kokoska);
            Assert.AreEqual(f.Zivotinje.Count, 1);
            Assert.IsTrue(f.Zivotinje.Contains(kokoska));
            Assert.AreEqual(f.Lokacije.Count, 1);
            
            f.SpecijalizacijaFarme(ZivotinjskaVrsta.Krava, 100);

            Assert.AreEqual(f.Zivotinje.Count, 100);
            Assert.IsTrue(f.Zivotinje.FindAll(z => z.Vrsta == ZivotinjskaVrsta.Krava).Count == 100);
            Assert.IsFalse(f.Zivotinje.Contains(kokoska));
            int brojLokacija = f.Zivotinje.Count / 25;
            Assert.IsTrue(Math.Abs(f.Lokacije.Count - brojLokacija) < 0.1);
            Assert.IsTrue(f.Lokacije.FindAll(l => l.Naziv == "Velika štala" && l.Država == "Bosna i Hercegovina").Count == brojLokacija);
            Assert.IsFalse(f.Lokacije.Contains(staraStala));
        }

        [TestMethod]
        public void SpecijalizacijaMagarećaFarma()
        {
            Farma f = new Farma();
            Lokacija staraStala = new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Sarajevo", "71000", "Bosna i Hercegovina" }, 181.22);
            Zivotinja magarac = new Zivotinja(ZivotinjskaVrsta.Magarac, DateTime.Parse("01/01/2021"), 40.1, 74.11, staraStala);
            f.DodavanjeNoveLokacije(staraStala);
            f.RadSaZivotinjama("Dodavanje", magarac);
            Assert.AreEqual(f.Zivotinje.Count, 1);
            Assert.IsTrue(f.Zivotinje.Contains(magarac));
            Assert.AreEqual(f.Lokacije.Count, 1);
            
            f.SpecijalizacijaFarme(ZivotinjskaVrsta.Magarac, 100);

            Assert.AreEqual(f.Zivotinje.Count, 100);
            Assert.IsTrue(f.Zivotinje.FindAll(z => z.Vrsta == ZivotinjskaVrsta.Magarac).Count == 100);
            Assert.IsTrue(f.Zivotinje.Contains(magarac));
            int brojLokacija = f.Zivotinje.Count / 25;
            Assert.IsTrue(Math.Abs(f.Lokacije.Count - brojLokacija) < 0.1);
            Assert.IsTrue(f.Lokacije.FindAll(l => l.Naziv == "Velika štala" && l.Država == "Bosna i Hercegovina").Count == brojLokacija);
            Assert.IsTrue(f.Lokacije.Contains(staraStala));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SpecijalizacijaFarmaNepodrzanaVrsta()
        {
            Farma f = new Farma();
            Lokacija staraStala = new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Sarajevo", "71000", "Bosna i Hercegovina" }, 181.22);
            Zivotinja magarac = new Zivotinja(ZivotinjskaVrsta.Magarac, DateTime.Parse("01/01/2021"), 40.1, 74.11, staraStala);
            f.DodavanjeNoveLokacije(staraStala);
            f.RadSaZivotinjama("Dodavanje", magarac);
            Assert.AreEqual(f.Zivotinje.Count, 1);
            Assert.IsTrue(f.Zivotinje.Contains(magarac));
            Assert.AreEqual(f.Lokacije.Count, 1);
            
            f.SpecijalizacijaFarme(ZivotinjskaVrsta.Koza, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SpecijalizacijaFarmaPreviseGrla()
        {
            Farma f = new Farma();
            Lokacija staraStala = new Lokacija(new List<string>() { "Štala", "Seoski put", "12", "Split", "21000", "Hrvatska" }, 25.22);
            Zivotinja kokoska = new Zivotinja(ZivotinjskaVrsta.Kokoška, DateTime.Parse("01/01/2021"), 5.2, 26.44, staraStala);
            f.DodavanjeNoveLokacije(staraStala);
            f.RadSaZivotinjama("Dodavanje", kokoska);
            Assert.AreEqual(f.Zivotinje.Count, 1);
            Assert.IsTrue(f.Zivotinje.Contains(kokoska));
            Assert.AreEqual(f.Lokacije.Count, 1);

            f.SpecijalizacijaFarme(ZivotinjskaVrsta.Krava, 1000);
        }

        #endregion
    }
}
