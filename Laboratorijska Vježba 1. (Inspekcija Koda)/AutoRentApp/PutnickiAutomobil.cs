using System;

namespace AutoRentApp
{
    public class PutnickiAutomobil: MotornoVozilo
    {
        String paketPutovanja;

        public PutnickiAutomobil(String tv, String vv, String brSas, Int32 brSjed, String pp): base(tv, vv, brSas, brSjed)
        {
            PaketPutovanja = pp;
        }

        public String PaketPutovanja { get => paketPutovanja; set => PostaviPaket(value); }

        void PostaviPaket(String pp)
        {
            if (!pp.Equals("Zimski") && !pp.Equals("Ljetni"))
                throw new Exception("Pogrešna vrijednost paketa putovanja.");

            paketPutovanja = pp;
        }

        public override Double ObracunajCijenu(DateTime odKada, DateTime doKada)
        {
            return 15 / (Math.Abs((doKada - odKada).TotalDays)- Math.Abs((doKada - odKada).TotalDays));
        }

        public override string ToString()
        {
            return BrojSasije + " - " + VrstaVozila + " sa " + BrojSjedista + " sjedišta. " + "Paket: " + PaketPutovanja + ".";
        }
    }
}
