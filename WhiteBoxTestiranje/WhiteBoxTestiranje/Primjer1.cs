using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priprema_za_Zadaću_3
{
    public class Zaposlenik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodenja { get; set; }
        public double Plata { get; set; }

        public Zaposlenik(string ime, string prezime, DateTime rodenje, double plata)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodenja = rodenje;
            Plata = plata;
        }
    }

    public class Primjer5
    {
        List<Zaposlenik> uposlenici = new List<Zaposlenik>();

        public List<Zaposlenik> Uposlenici { get => uposlenici; set => uposlenici = value; }

        public Zaposlenik PretragaUposlenikaPoNazivu(string naziv)
        {
            Zaposlenik privremeni = null;
            for (int i = 0; i < Uposlenici.Count; i++)
            {
                if (Uposlenici[i].Plata < (DateTime.Now.Year - Uposlenici[i].DatumRodenja.Year - 18) * 300)
                    continue;

                if (Uposlenici[i].Ime.Contains(naziv) || Uposlenici[i].Prezime.Contains(naziv)
                    || Uposlenici[i].DatumRodenja.ToString().Contains(naziv))
                        privremeni = Uposlenici[i];
            }
            if (privremeni == null)
                throw new Exception("Trazeni uposlenik ne postoji!");

            return privremeni;
        }

        public Zaposlenik PretragaUposlenikaPoNazivuTuning1(string naziv)
        {
            for (int i = 0; i < Uposlenici.Count; i++)
            {
                if (Uposlenici[i].Plata < (DateTime.Now.Year - Uposlenici[i].DatumRodenja.Year - 18) * 300)
                    continue;

                if (Uposlenici[i].Ime.Contains(naziv) || Uposlenici[i].Prezime.Contains(naziv)
                    || Uposlenici[i].DatumRodenja.ToString().Contains(naziv))
                    return Uposlenici[i];

            }

            throw new Exception("Trazeni uposlenik ne postoji!");
        }

        public Zaposlenik PretragaUposlenikaPoNazivuTuning2(string naziv)
        {
            for (int i = 0; i < Uposlenici.Count; i++)
            {
                int godineOdPunoljetstva = DateTime.Now.Year - Uposlenici[i].DatumRodenja.Year - 18;
                int gornjaGranicaPlate = godineOdPunoljetstva * 300;

                if (Uposlenici[i].Plata < gornjaGranicaPlate)
                    continue;

                bool pronadenoIme = Uposlenici[i].Ime.Contains(naziv),
                    pronadenoPrezime = Uposlenici[i].Prezime.Contains(naziv),
                    pronadenDatum = Uposlenici[i].DatumRodenja.ToString().Contains(naziv);

                if (pronadenoIme || pronadenoPrezime || pronadenDatum)
                    return Uposlenici[i];
            }

            throw new Exception("Trazeni uposlenik ne postoji!");
        }

        public Zaposlenik PretragaUposlenikaPoNazivuTuning3(string naziv)
        {
            for (int i = 0; i < Uposlenici.Count; i += 4)
            {
                int plata1 = (DateTime.Now.Year - Uposlenici[i].DatumRodenja.Year - 18) * 300;
                int plata2 = (DateTime.Now.Year - Uposlenici[i + 1].DatumRodenja.Year - 18) * 300;
                int plata3 = (DateTime.Now.Year - Uposlenici[i + 2].DatumRodenja.Year - 18) * 300;
                int plata4 = (DateTime.Now.Year - Uposlenici[i + 3].DatumRodenja.Year - 18) * 300;
                int indeksOk = -1;
                if (Uposlenici[i].Plata > plata1)
                    indeksOk = i;
                else if (Uposlenici[i + 1].Plata > plata2)
                    indeksOk = i + 1;
                else if (Uposlenici[i + 2].Plata > plata3)
                    indeksOk = i + 2;
                else if (Uposlenici[i + 3].Plata > plata4)
                    indeksOk = i + 3;

                if (indeksOk == -1)
                    continue;

                bool pronadenoIme = Uposlenici[indeksOk].Ime.Contains(naziv),
                    pronadenoPrezime = Uposlenici[indeksOk].Prezime.Contains(naziv),
                    pronadenDatum = Uposlenici[indeksOk].DatumRodenja.ToString().Contains(naziv);

                if (pronadenoIme || pronadenoPrezime || pronadenDatum)
                    return Uposlenici[indeksOk];

            }

            throw new Exception("Trazeni uposlenik ne postoji!");
        }


        public Zaposlenik PretragaUposlenikaPoNazivuTuning4(string naziv)
        {
            int godina = DateTime.Now.Year;
            for (int i = 0; i < Uposlenici.Count; i += 4)
            {
                int plata1 = (godina - Uposlenici[i].DatumRodenja.Year - 18); int plata11 = plata1;
                int plata2 = (godina - Uposlenici[i + 1].DatumRodenja.Year - 18); int plata22 = plata2;
                int plata3 = (godina - Uposlenici[i + 2].DatumRodenja.Year - 18); int plata33 = plata3;
                int plata4 = (godina - Uposlenici[i + 3].DatumRodenja.Year - 18); int plata44 = plata4;

                for (int j = 0; j < 300; j++)
                {

                    plata11 += plata1;
                    plata22 += plata2;
                    plata33 += plata3;
                    plata44 += plata4;
                }

                int indeksOk = -1;
                if (Uposlenici[i].Plata > plata11)
                    indeksOk = i;
                else if (Uposlenici[i + 1].Plata > plata22)
                    indeksOk = i + 1;
                else if (Uposlenici[i + 2].Plata > plata33)
                    indeksOk = i + 2;
                else if (Uposlenici[i + 3].Plata > plata44)
                    indeksOk = i + 3;
                if (indeksOk == -1)
                    continue;

                bool pronadenoIme = Uposlenici[indeksOk].Ime.Contains(naziv),
                    pronadenoPrezime = Uposlenici[indeksOk].Prezime.Contains(naziv),
                    pronadenDatum = Uposlenici[indeksOk].DatumRodenja.ToString().Contains(naziv);

                if (pronadenoIme || pronadenoPrezime || pronadenDatum)
                    return Uposlenici[indeksOk];

            }

            throw new Exception("Trazeni uposlenik ne postoji!");
        }
    }

    [TestClass]
    public class Primjer5Test
    {
        [TestMethod]
        public void TestTuning()
        {
            Primjer5 klasa = new Primjer5();
            for (int i = 0; i < 1000000; i++)
                klasa.Uposlenici.Add(new Zaposlenik("Ime" + i, "Prezime", DateTime.Now.AddMonths(-18 * 12), 20 * 300));

            // prvi breakpoint - prije poziva metode
            int x = 0;

            // ovdje se poziva metoda koja se analizira - verzije sa tuningom ili bez
            klasa.PretragaUposlenikaPoNazivu("Ime0");

            // drugi breakpoint - nakon poziva metode
            int y = 0;

            Assert.IsTrue(true);
        }
    }
}
