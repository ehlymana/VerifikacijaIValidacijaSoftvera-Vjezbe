using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetrikeRefaktoringCodeTuning
{
    public class Uposlenik
    {
        string ime;

        public string Ime { get => ime; set => ime = value; }
    }

    public class Primjer1
    {
        List<Uposlenik> uposlenici = new List<Uposlenik>();

        public List<Uposlenik> PretragaUposlenikaPoNazivu(string naziv)
        {
            List<Uposlenik> lista = new List<Uposlenik>();
            foreach (Uposlenik u in uposlenici)
            {
                Uposlenik privremeni = null;
                if (u.Ime.Contains(naziv))
                    if (!lista.Contains(u))
                        privremeni = u;

                if (privremeni != null)
                    lista.Add(privremeni);
            }

            if (lista.Count == 0)
                throw new Exception("Trazeni uposlenik ne postoji!");

            return lista;
        }

        public List<Uposlenik> PretragaUposlenikaPoNazivuRefaktoring1(string naziv)
        {
            List<Uposlenik> lista = new List<Uposlenik>();
            foreach (Uposlenik u in uposlenici)
            {
                Uposlenik privremeni = null;
                if (u.Ime.Contains(naziv))
                    privremeni = u;

                if (privremeni != null)
                    lista.Add(privremeni);
            }

            if (lista.Count == 0)
                throw new Exception("Trazeni uposlenik ne postoji!");

            return lista;
        }

        public List<Uposlenik> PretragaUposlenikaPoNazivuRefaktoring2(string naziv)
        {
            List<Uposlenik> lista = new List<Uposlenik>();
            foreach (Uposlenik u in uposlenici)
            {
                if (u.Ime.Contains(naziv))
                    lista.Add(u);
            }

            if (lista.Count == 0)
                throw new Exception("Trazeni uposlenik ne postoji!");

            return lista;
        }

        public List<Uposlenik> PretragaUposlenikaPoNazivuRefaktoring3(string naziv)
        {
            List<Uposlenik> lista = uposlenici.FindAll(u => u.Ime.Contains(naziv));

            if (lista.Count == 0)
                throw new Exception("Trazeni uposlenik ne postoji!");

            return lista;
        }
    }
}
