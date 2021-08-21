using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemZaGrijanje
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test deaktivacije grijanja koristeći dummy
        /// </summary>
        [TestMethod]
        public void TestDeaktivacijeGrijanja()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();
            ITermostat dummy = new Dummy();

            k.Aktivna = true;
            k.AutomatskaKontrola(dummy, false);
            Assert.IsFalse(k.Aktivna);
        }

        /// <summary>
        /// Test kontrole temperature koristeći tri stuba
        /// </summary>
        [TestMethod]
        public void TestKontroleGrijanja1()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();

            ITermostat stub1 = new StubNiskaTemperatura();
            k.AutomatskaKontrola(stub1, true);
            Assert.AreEqual(k.JačinaGrijanja, 1);

            ITermostat stub2 = new StubSrednjaTemperatura();
            k.AutomatskaKontrola(stub2, true);
            Assert.AreEqual(k.JačinaGrijanja, 0.5);

            ITermostat stub3 = new StubVisokaTemperatura();
            k.AutomatskaKontrola(stub3, true);
            Assert.AreEqual(k.JačinaGrijanja, 0);
        }

        /// <summary>
        /// Test kontrole temperature koristeći spy
        /// </summary>
        [TestMethod]
        public void TestKontroleGrijanja2()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();
            ITermostat spy = new Spy();

            ((Spy)spy).Opcija = 0;
            k.AutomatskaKontrola(spy, true);
            Assert.AreEqual(k.JačinaGrijanja, 1);

            ((Spy)spy).Opcija = 1;
            k.AutomatskaKontrola(spy, true);
            Assert.AreEqual(k.JačinaGrijanja, 0.5);

            ((Spy)spy).Opcija = 2;
            k.AutomatskaKontrola(spy, true);
            Assert.AreEqual(k.JačinaGrijanja, 0);
        }

        /// <summary>
        /// Test usrednjavanja jačine grijanja koristeći fake
        /// </summary>
        [TestMethod]
        public void TestUsrednjavanjaJačineGrijanja()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();
            ITermostat fake = new Fake();

            fake.Temperature = new List<double>()
            {
                20, 21, 22, 23, 24
            };
            k.Usrednjavanje(fake);
            double očekivaniRezultat = Math.Abs(30 - fake.Temperature.Average()) / fake.Temperature.Average();
            Assert.IsTrue(Math.Abs(k.JačinaGrijanja - očekivaniRezultat) < 0.01);

            očekivaniRezultat = ((Fake)fake).DodajTestneTemperature();
            k.Usrednjavanje(fake);
            Assert.IsTrue(Math.Abs(k.JačinaGrijanja - očekivaniRezultat) < 0.01);
        }

        /// <summary>
        /// Test defrost tehnologije (kada radi) koristeći mock
        /// </summary>
        [TestMethod]
        public void TestDefrostTehnologijeOK()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();
            ITermostat mock = new Mock();

            KontrolerGrijanja.IndikatorVode = "OK";
            k.AutomatskaKontrola(mock, true);
            Assert.AreEqual(k.JačinaGrijanja, 0.1);
        }

        /// <summary>
        /// Test defrost tehnologije (neočekivano ponašanje) koristeći mock
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDefrostTehnologijeNeočekivanoPonašanje()
        {
            KontrolerGrijanja k = new KontrolerGrijanja();
            ITermostat mock = new Mock();
            k.AutomatskaKontrola(mock, true);
        }
    }
}
