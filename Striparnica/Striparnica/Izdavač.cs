using System;
using System.Collections.Generic;
using System.Text;

namespace Striparnica
{
    public class Izdavač
    {
        string imeFirme, adresaFirme;
        List<Strip> izdatiStripovi;
        int godinaOsnivanja;
        public Izdavač(string ime, string adresa, int godina)
        {
            ImeFirme = ime;
            adresaFirme = adresa;
            godinaOsnivanja = godina;
        }

        public string ImeFirme { get => imeFirme; set => imeFirme = value; }

        public void izdajStrip(Strip strip)
        {
            izdatiStripovi.Add(strip);
        }
        public bool resetujIzdavaca()
        {
            try
            {
                izdatiStripovi = null;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
