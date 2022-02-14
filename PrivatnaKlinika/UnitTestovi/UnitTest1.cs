using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrivatnaKlinika;
using System;

namespace UnitTestovi
{
    [TestClass]
    public class UnitTest1
    {
        static Ordinacija o;
        static Pacijent p;

        /// <summary>
        /// Inicijalizacija podataka koja se vrši samo jednom
        /// </summary>
        [ClassInitialize]
        public static void PočetnaInicijalizacija(TestContext context)
        {
            o = new Ordinacija("Opća ordinacija");
        }

        /// <summary>
        /// Inicijalizacija podataka koja se vrši prije svakog testa
        /// </summary>
        [TestInitialize]
        public void InicijalizacijaPrijeSvakogTesta()
        {
            p = new Pacijent("Ime", "Prezime", DateTime.Parse("01/01/1996"), "0101996170001", "Muško", "Adresa", "Slobodan",
                "Sistematski pregled", DateTime.Now, "ZD-001");
        }

        /// <summary>
        /// Test dodavanja pacijenta u red ordinacije
        /// </summary>
        [TestMethod]
        public void TestDodavanjePacijentaURed()
        {
            // izvršavanje funkcionalnosti
            o.dodajPacijenta(p);

            // provjera očekivanih rezultata 
            Assert.IsTrue(o.Zauzetost > 0);
            Assert.IsTrue(o.PacijentiURedu.Count == 1);
        }

        /// <summary>
        /// Test obrade pacijenta kada je već u redu ordinacije
        /// </summary>
        [TestMethod]
        public void TestObradePacijenta()
        { 
            // izvršavanje funkcionalnosti
            Pacijent p2 = o.sljedeci();
            
            // provjera očekivanih rezultata
            Assert.AreEqual(o.Zauzetost, 0);
            Assert.AreEqual(o.PacijentiURedu.Count, 0);
            Assert.IsTrue(p.Maticni == p2.Maticni);
            Assert.AreNotSame(p, p2);
        }

        /// <summary>
        /// Test pokušaja obrade pacijenta kada je ordinacija prazna
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestObradePacijentaIzuzetak()
        {
            Ordinacija praznaOrdinacija = new Ordinacija("Ambulanta");
            praznaOrdinacija.sljedeci();
        }
    }
}
