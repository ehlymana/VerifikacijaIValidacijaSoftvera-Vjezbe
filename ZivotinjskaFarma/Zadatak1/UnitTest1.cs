using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZivotinjskaFarma;
using System;
using System.Collections.Generic;

namespace Zadatak1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1VerfikacijaKupovine()
        {
            List<string> lista = new List<string>() {"Naziv Farme", "Adresa Farme", "31", "Sarajevo", "71000", "Bosna i Hercegovina"};
            Lokacija lokacija = new Lokacija(lista, 50);

            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddYears(-10), 10, 50, lokacija);
            Proizvod proizvod1 = new Proizvod("Proizvod", "Opis", "Mlijeko", z1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 3);
            Kupovina kupovina1 = new Kupovina("3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2), proizvod1, 10, false);
            Assert.IsFalse(kupovina1.VerificirajKupovinu());


            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Kokoška, DateTime.Now.AddDays(-1), 5, 50, lokacija);
            Proizvod proizvod2 = new Proizvod("Proizvod", "Opis", "Jaja", z2, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(80), 3);
            Kupovina kupovina2 = new Kupovina("3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(9), proizvod2, 1, false);
            Assert.IsFalse(kupovina2.VerificirajKupovinu());

            Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-3650), 50, 88, lokacija);
            Proizvod proizvod3 = new Proizvod("Proizvod", "Opis", "Vuna", z3, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(25), 2);
            Kupovina kupovina3 = new Kupovina("3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2), proizvod3, 1, false);
            Assert.IsFalse(kupovina3.VerificirajKupovinu());

            Kupovina kupovina4 = new Kupovina("3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(80), proizvod3, 1, false);
            Assert.IsTrue(kupovina4.VerificirajKupovinu());



            //Zivotinja z4 = new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddDays(-3650), 50, 88, lokacija);
            //Proizvod proizvod4 = new Proizvod("Proizvod", "Opis", "Vuna", z4, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(25), 2);
            //Kupovina kupovina4 = new Kupovina("3", DateTime.Now.AddDays(-2), DateTime.Now.AddDays(80), proizvod4, 1, false);
            //Assert.IsTrue(kupovina4.VerificirajKupovinu());

        }
    }
}
