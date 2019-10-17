using System;
using System.Collections.Generic;
using System.Linq;
using AutoRentApp.Exceptions;

namespace AutoRentApp
{
    public class LuksuzniAutomobil: MotornoVozilo
    {
        #region Atributi

        const Double CIJENA_DNEVNOG_KORISTENJA = 25;
        List<String> dodatneUsluge;
        static string[] moguceDodatneUsluge = { "Krevet", "Bar", "PS4", "XBox1", "Nintendo Switch"};

        #endregion

        #region Konstruktor

        public LuksuzniAutomobil(String tv, String vv, String brSas, Int32 brSjed): base(tv, vv, brSas, brSjed)
        {
            dodatneUsluge = new List<String>();
        }

        #endregion

        #region Metode

        // <description>
        // Vraćanje svih dostupnih dodatnih usluga
        // </description>
        public List<String> VratiDodatneUsluge()
        {
            return dodatneUsluge;
        }

        // <description>
        // Brisanje postojeće dodatne usluge
        // </description>
        public void ObrisiDodatnuUslugu(Int16 idx)
        {
            dodatneUsluge.RemoveAt(idx);
        }

        // <description>
        // Dodavanje nove dodatne usluge
        // </description>
        public void DodajDodatnuUslugu(String u)
        {
            if (dodatneUsluge.Count.Equals(3))
                throw new AdditionalPackagesCapacityFullException("Kapacitet dodatnih paketa je pun. Ako želite da dodate neku od usluga, molimo vas izbrišite neku od postojećih.");
            if (!moguceDodatneUsluge.Contains(u))
                throw new AdditionalPackageDoesNotExistException("Paket koji ste odabrali nije u ponudi.");

            dodatneUsluge.Add(u);
        }

        // <description>
        // Obračun cijene za korištenje luksuznog automobila
        // </description>
        public override Double ObracunajCijenu(DateTime odKada, DateTime doKada)
        {
            // ako je auto iznajmljeno tokom vikenda, cijena je skuplja
            if(odKada.DayOfWeek.ToString().Equals("Saturday") || odKada.DayOfWeek.ToString().Equals("Sunday"))
                return CIJENA_DNEVNOG_KORISTENJA * (doKada - odKada).TotalDays + 100;

            return CIJENA_DNEVNOG_KORISTENJA * Math.Abs((doKada - odKada).TotalDays);
        }

        // <description>
        // Definisanje tekstualnog opisa luksuznog automobila
        // </description>
        public override string ToString()
        {
            String rez = BrojSasije + " - " + VrstaVozila + " sa " + BrojSjedista + " sjedišta. ";

            if (dodatneUsluge.Count == 0)
                return rez + "Bez dodatnih usluga.";
            else
            {
                rez += "Dodatne usluge:";

                foreach (String s in dodatneUsluge)
                    rez += " " + s;

                return rez + ".";
            }
        }

        #endregion
    }
}
