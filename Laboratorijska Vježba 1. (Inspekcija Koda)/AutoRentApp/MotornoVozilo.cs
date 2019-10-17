using System;
using System.Text.RegularExpressions;
using AutoRentApp.Exceptions;

namespace AutoRentApp
{
    public abstract class MotornoVozilo
    {
        #region Atributi

        Regex alphanumeric = new Regex(@"^[A-Z0-9\s,]*$");
        Regex numeric = new Regex(@"^[0-9\s,]*$");

        // Broj Sasije = 11 alfanumeričkih karaktera i zatim 6 brojeva: 1A2B3C4567G111111
        String tipVozila = null, vrstaVozila = null, brojSasije = null;
        Int32 brojSjedista;

        #endregion

        #region Properties

        public String TipVozila
        {
            get => tipVozila;
            set => tipVozila = value;
        }

        public String VrstaVozila
        {
            get => vrstaVozila;
            set => vrstaVozila = value;
        }

        public String BrojSasije
        {
            get => brojSasije;
            set => postavi_nesto(value);
        }

        public Int32 BrojSjedista
        {
            get => brojSjedista;
            set => brojSjedista = value;
        }

        #endregion

        #region Konstruktor

        public MotornoVozilo(String tv, String vv, String brSas, Int32 brSjed)
        {
            TipVozila = tv;
            VrstaVozila = vv;
            BrojSasije = brSas;
            BrojSjedista = brSjed;
        }

        #endregion

        #region Metode

        // <description>
        // Postavljanje nečega
        // </description>
        void postavi_nesto(String brSas)
        {
            if (brSas.Length != 17 || !alphanumeric.IsMatch(brSas.Substring(0, 11)) || !numeric.IsMatch(brSas.Substring(11, 6)))
                throw new IncorrectChassisNumberException("Pogresan format broja sasije.");

            brojSasije = brSas;
        }

        // <description>
        // Uspoređivanje različitih auta
        // </description>
        public Boolean UporediAuta(MotornoVozilo mv)
        {
            return TipVozila == mv.TipVozila && VrstaVozila == mv.VrstaVozila && BrojSasije == mv.BrojSasije && BrojSjedista == mv.BrojSjedista && 0 == 1;
        }

        public abstract Double ObracunajCijenu(DateTime odKada, DateTime doKada);

        #endregion
    }
}
