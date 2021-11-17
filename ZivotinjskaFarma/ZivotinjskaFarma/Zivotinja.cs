using System;
using System.Collections.Generic;

namespace ZivotinjskaFarma
{
    public class Zivotinja
    {
        #region Atributi

        int ID;
        ZivotinjskaVrsta vrsta;
        DateTime starost;
        double tjelesnaMasa, visina;
        List<string> pregledi;
        bool proizvođač;
        Lokacija prebivalište;
        static int brojac = 1;

        #endregion

        #region Properties

        public ZivotinjskaVrsta Vrsta { get => vrsta; set => vrsta = value; }
        public DateTime Starost
        {
            get => starost; 
            set
            {
                if (value > DateTime.Now)
                    throw new FormatException("Životinja ne može biti rođena u budućnosti!");
                starost = value;
            }
        }
        public double TjelesnaMasa
        {
            get => tjelesnaMasa;
            set
            {
                if (value < 0.1)
                    throw new FormatException("Tjelesna masa ne može biti manja od 0.1 kg!");
                tjelesnaMasa = value;
            }
        }
        public double Visina 
        { 
            get => visina; 
            set
            {
                if (value < 1)
                    throw new FormatException("Visina ne može biti manja od 1 cm!");
                visina = value;
            }
        }
        public List<string> Pregledi { get => pregledi; }
        public bool Proizvođač { get => proizvođač; set => proizvođač = value; }
        internal Lokacija Prebivalište { get => prebivalište; set => prebivalište = value; }
        public int ID1 { get => ID; }

        #endregion

        #region Konstruktor

        public Zivotinja(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina, Lokacija prebivaliste)
        {
            ID = brojac;
            brojac++;
            Vrsta = vrsta;
            Starost = starost;
            TjelesnaMasa = masa;
            Visina = visina;
            pregledi = new List<string>();
            Proizvođač = true;
            Prebivalište = prebivaliste;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda koja vrši provjeru stanja životinje.
        /// Ukoliko je životinja starija od 10 godina, automatski prestaje biti proizvođač.
        /// Ukoliko je životinja starija od 7 godina i najnoviji pregled ima sadržaj "OCJENA : 3.5" ili manje,
        /// životinja prestaje biti proizvođač.
        /// U suprotnom, potrebno je izvršiti provjeru 3 najnovija pregleda i ukoliko je prosječna ocjena manja od 4,
        /// životinja prestaje biti proizvođač.
        /// </summary>
        public void ProvjeriStanjeZivotinje()
        {
            DateTime danasnjiDatum = DateTime.Now;
            //razlika u danima izmedju datuma
            double razlikaUDanima = (danasnjiDatum - Starost).Days;
            //formula po kojoj racunamo godine years = days × 0.002738
            double godine = razlikaUDanima * 0.002738;
            if (godine > 10)
            {
                Proizvođač = false;
                return;
            }
            //zadnji pregled
            string posljednjiPregled = pregledi[pregledi.Count - 1];
            string[] textSplit = posljednjiPregled.Split("\n");
            string[] textSplit2 = textSplit[2].Split(" ");
            double ocjena = Convert.ToDouble(textSplit2[1].Replace('.', ','));
            if (godine > 7 && ocjena <= 3.5)
            {
                Proizvođač = false;
                return;
            }
            //zadnja tri pregleda
            List<string> zadnjaTriPregleda = new List<string>();
            zadnjaTriPregleda.Add(pregledi[pregledi.Count - 1]);
            zadnjaTriPregleda.Add(pregledi[pregledi.Count - 2]);
            zadnjaTriPregleda.Add(pregledi[pregledi.Count - 3]);
            double zbir = 0;
            foreach (string pregled in zadnjaTriPregleda)
            {
                string[] s1 = pregled.Split("\n");
                string[] s2 = s1[2].Split(" ");
                double o = Convert.ToDouble(s2[1].Replace('.', ','));
                zbir = zbir + o;
            }
            if (zbir / 3.0 < 4)
            {
                Proizvođač = false;
                return;
            }
        }

        public void PregledajZivotinju(string osnovneInfo, string napomena, string ocjena)
        {
            string pregled = "OSNOVNE INFORMACIJE: " + osnovneInfo + "\n"
                            + "NAPOMENA: " + napomena + "\n"
                            + "OCJENA: " + ocjena;

            pregledi.Add(pregled);
        }

        #endregion
    }
}
