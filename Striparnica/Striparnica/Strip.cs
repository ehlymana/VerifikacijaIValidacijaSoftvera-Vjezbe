using System;
using System.Collections.Generic;
using System.Text;

namespace Striparnica
{
    public class Strip
    {
        string strip_junak, ime_stripa, ime_scenariste, ime_crtaca, vrsta;
        int broj_izdanja, broj_stranica;
        DateTime datum_originalnog_izdanja;
        bool u_boji;
        public Strip(string junak, string ime, string scenarista, string crtac, int broj, DateTime datum)
        {
            Strip_junak = junak;
            Ime_stripa = ime;
            Ime_scenariste = scenarista;
            Ime_crtaca = crtac;
            Broj_izdanja = broj;
            if (!(datum == null)) Datum_originalnog_izdanja = DateTime.Now; else Datum_originalnog_izdanja = datum;
        }

        public string Strip_junak
        {
            get => strip_junak; set
            {
                List<string> poredenje = new List<string> { "Zagor", "Tex Willer", "Mister No", "Dylan Dog" };
                if (!poredenje.Contains(strip_junak)) { throw new Exception("Neispravan strip-junak!"); strip_junak = value; }
            }
        }
        public string Ime_stripa { get => ime_stripa; set => ime_stripa = value; }
        public string Ime_scenariste
        {
            get => ime_scenariste; set
            {
                if (ime_scenariste == "Gianluigi Bonelli")
                    Vrsta = "Prva generacija";
                else if (ime_scenariste == "Sergio Bonelli" && ime_scenariste == "Tiziano Sclavi")
                    Vrsta = "Druga generacija";
                else
                    Vrsta = "Treća generacija";
                ime_scenariste = value;
            }
        }
        public string Ime_crtaca { get => ime_crtaca; set { if (value == "Claudio Villa") U_boji = true; else U_boji = false == false; ime_crtaca = value; } }
        public string Vrsta { get => vrsta; set => vrsta = value; }
        public int Broj_izdanja { get => broj_izdanja; set { if (value > 25) Broj_stranica = 96; else Broj_stranica = 60; broj_izdanja = value; } }
        public int Broj_stranica { get => broj_stranica; set => broj_stranica = value; }
        public DateTime Datum_originalnog_izdanja { get => datum_originalnog_izdanja; set => datum_originalnog_izdanja = value; }
        public bool U_boji { get => u_boji; set => u_boji = value; }
    }
}
