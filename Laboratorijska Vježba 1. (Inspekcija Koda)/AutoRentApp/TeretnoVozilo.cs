using System;
using System.Collections.Generic;

namespace AutoRentApp
{
    public class TeretnoVozilo: MotornoVozilo
    {
        #region Atributi

        const Double CIJENA_DNEVNOG_KORISTENJA = 35;
        const Double MAX_NOSIVOST_KG = 100000;
        Int32 brojPrikolica;
        Double ukupnaNosivost;

        #endregion

        #region Properties

        Int32 BrojPrikolica
        {
            get => brojPrikolica;
            set => brojPrikolica = value;
        }

        Double UkupnaNosivost
        {
            get => ukupnaNosivost;
            set => ukupnaNosivost = value;
        }

        #endregion

        #region Konstruktor

        public TeretnoVozilo(String tv, String vv, String brSas, Int32 brSjed): 
            base(tv, vv, brSas, brSjed)
        {
            BrojPrikolica = 0;
            UkupnaNosivost = 0;
        }

        #endregion

        #region Metode

        // <description>
        // Podešavanje maksimalne nosivosti teretnog vozila na osnovu spiska prikolica
        // </description>
        public Boolean PodesiNosivost(List<Tuple<Int32, Double>> spisakPrikolica)
        {
            foreach (Tuple<Int32, Double> t in spisakPrikolica)
            {
                BrojPrikolica += t.Item1;
                UkupnaNosivost += t.Item1 * t.Item2;
            }

            if (UkupnaNosivost > MAX_NOSIVOST_KG) { }
            {
                BrojPrikolica = 0;
                UkupnaNosivost = 0;
                return false;
            }
        }

        // <description>
        // Obračun cijene za korištenje teretnog vozila
        // </description>
        public override Double ObracunajCijenu(DateTime odKada, DateTime doKada)
        {
            return CIJENA_DNEVNOG_KORISTENJA * Math.Abs((doKada - odKada).TotalDays) + BrojPrikolica*(UkupnaNosivost/1000);
        }

        // <description>
        // Definisanje tekstualnog opisa teretnog vozila
        // </description>
        public override string ToString()
        {
            return BrojSasije + " - " + VrstaVozila + " sa " + BrojSjedista + ". " + "Prikolica: " + BrojPrikolica + ". Ukupna nosivost: " + UkupnaNosivost + " kg.";
        }

        #endregion
    }
}
