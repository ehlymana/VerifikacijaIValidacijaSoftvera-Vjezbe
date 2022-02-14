using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priprema_za_Zadaću_3
{
public class Kino
{
    double cijenaKarte;

    public double obracunajCijenu(DateTime vrijemeProjekcije, bool student, string vrstaProjekcije)
    {
        // srijeda - cijena 4 KM za sve
        if (vrijemeProjekcije.DayOfWeek.ToString() == "Wednesday")
            cijenaKarte = 4;

        // ponedjeljak - cijena 4 KM za studente
        else if (vrijemeProjekcije.DayOfWeek.ToString() == "Monday" && student)
            cijenaKarte = 4;

        // ostali dani - cijena ovisi o vremenu projekcije
        else
        {
            if (vrijemeProjekcije.Hour < 15) 
                cijenaKarte = 5;
            else if (vrijemeProjekcije.Hour < 19) 
                cijenaKarte = 6;
            else 
                cijenaKarte = 7;
        }

        // dodatno plaćanje za 3D projekcije
        if (vrstaProjekcije == "3D") 
            cijenaKarte += 1.5;

        return cijenaKarte;
    }
}

    [TestClass]
    public class Primjer4
    {
        Kino kino = new Kino();

        [TestMethod]
        //srijeda, vrijeme projekcije nevažno, da li je student je nevažno, nije 3D projekcija
        public void Put1()
        {
            DateTime vrijemeProjekcije = new DateTime(2018, 10, 31, 15, 0, 0);
            double cijena = kino.obracunajCijenu(vrijemeProjekcije, false, "Obična"); Assert.AreEqual(cijena, 4);
        }

        [TestMethod]
        //ponedjeljak, vrijeme projekcije nevažno, student je, nije 3D projekcija
        public void Put2()
        {
            DateTime vrijemeProjekcije = new DateTime(2018, 10, 29, 15, 0, 0);
            double cijena = kino.obracunajCijenu(vrijemeProjekcije, true, "Obična"); Assert.AreEqual(cijena, 4);
        }

        [TestMethod]
        //nije ni ponedjeljak ni srijeda, vrijeme projekcije do 15:00, nije važno je li student, nije 3D projekcija
        public void Put3()
        {
            DateTime vrijemeProjekcije = new DateTime(2018, 10, 30, 14, 0, 0);
            double cijena = kino.obracunajCijenu(vrijemeProjekcije, false, "Obična"); Assert.AreEqual(cijena, 5);
        }

        [TestMethod]
        //nije ni ponedjeljak ni srijeda, vrijeme projekcije do 19:00, nije važno je li student, nije 3D projekcija
        public void Put4()
        {
            DateTime vrijemeProjekcije = new DateTime(2018, 10, 30, 18, 0, 0);
            double cijena = kino.obracunajCijenu(vrijemeProjekcije, false, "Obična"); Assert.AreEqual(cijena, 6);
        }

        [TestMethod]
        //nije ni ponedjeljak ni srijeda, vrijeme projekcije nakon 19:00, nije važno je li student, 3D projekcija je
        public void Put5()
        {
            DateTime vrijemeProjekcije = new DateTime(2018, 10, 30, 20, 0, 0);
            double cijena = kino.obracunajCijenu(vrijemeProjekcije, false, "3D"); Assert.AreEqual(cijena, 8.5);
        }
    }
}
