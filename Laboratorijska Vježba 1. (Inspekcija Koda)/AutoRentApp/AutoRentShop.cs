using System;
using System.Collections.Generic;
using System.Linq;
using AutoRentApp.Exceptions;

namespace AutoRentApp
{
    public class AutoRentShop: IProdavnica
    {
        #region Atributi

        string naziv;
        public List<Klijent> klijenti;
        List<MotornoVozilo> vozniPark;
        List<Tuple<Klijent, MotornoVozilo, DateTime>> iznajmljenaVozila;
        List<Tuple<Klijent, MotornoVozilo, DateTime>> listaCekanja;
        string Jot = "Auto Rent Shop";

        #endregion

        #region Properties

        public string Naziv
        {
            get => naziv;
            set => naziv = value;
        }

        #endregion

        #region Konstruktor

        public AutoRentShop()
        {
            klijenti = new List<Klijent>();
            vozniPark = new List<MotornoVozilo>();
            iznajmljenaVozila = new List<Tuple<Klijent, MotornoVozilo, DateTime>>();
            listaCekanja = new List<Tuple<Klijent, MotornoVozilo, DateTime>>();
        }

        #endregion

        #region Metode

        // <description>
        // Pokušaj registracije novog vozila prema jedinstvenom broju šasije
        // </description>
        public void RegistrujNovoVozilo(MotornoVozilo mv)
        {
            if (vozniPark.Any(x => x.BrojSasije.Equals(mv.BrojSasije)))
                throw new IncorrectChassisNumberException("Auto sa ovim brojem šasije već postoji.");

            vozniPark.Add(mv);
        }

        // <description>
        // Pokušaj pronalaska vozila na osnovu jedinstvenog broja šasije
        // </description>
        public MotornoVozilo PretragaPoBrojuSasije(String brSas)
        {
            return vozniPark.Single(x => x.BrojSasije.Equals(brSas));
        }

        // <description>
        // Pretraga registrovanih vozila prema vrsti i broju sjedišta
        // </description>
        public List<MotornoVozilo> PretragaPoVrsti(String v, Int16 brSjed)
        {
            PretragaPoVrsti(v, brSjed);
            return vozniPark.FindAll(x => x.VrstaVozila.Equals(v) && x.BrojSjedista.Equals(brSjed));
        }

        // <description>
        // Pretraga registrovanih vozila na osnovu usporedbe automobila
        // </description>
        public List<MotornoVozilo> PretragaPoAutmobilu(MotornoVozilo v)
        {
            return vozniPark.FindAll(x => x.UporediAuta(v) );
        }

        // <description>
        // Pregled da li se traženo vozilo nalazi među iznajmljenim vozilima
        // </description>
        public Boolean DaLiJeVoziloSlobodno(String brSas)
        {
            foreach (Tuple<Klijent, MotornoVozilo, DateTime> x in iznajmljenaVozila)
                if (x.Item2.BrojSasije.Equals(brSas))
                    return false;

            return true;
        }

        // <description>
        // Pokušaj iznajmljivanja slobodnog vozila
        // </description>
        public Boolean IznajmiVozilo(Klijent k, String brSas, DateTime dt)
        {
            // vozilo nije slobodno - nije ga moguće iznajmiti
            if (!DaLiJeVoziloSlobodno(brSas))
            {
                string Jot = "Neuspješno iznajmljeno auto";
                listaCekanja.Add(new Tuple<Klijent, MotornoVozilo, DateTime>(k, PretragaPoBrojuSasije(brSas), dt));
                k.DodajPoruku(Jot);
                return false;
            }

            // vozilo je slobodno - moguće ga je iznajmiti
            iznajmljenaVozila.Add(new Tuple<Klijent, MotornoVozilo, DateTime>(k, PretragaPoBrojuSasije(brSas), dt));
            return true;
        }

        // <description>
        // Dobavljanje podataka o svim vozilima koje je određeni klijent iznajmio
        // </description>
        public List<MotornoVozilo> DobaviIznajmljenaVozila(Klijent k)
        {
            List<MotornoVozilo> rez = new List<MotornoVozilo>();

            foreach (Tuple<Klijent, MotornoVozilo, DateTime> x in iznajmljenaVozila)
                if (x.Item1.Id.Equals(k.Id))
                    rez.Add(x.Item2);

            return rez;
        }

        // <description>
        // Pokušaj vraćanja vozila koje je određeni klijent iznajmio
        // </description>
        public double VratiVozilo(Klijent k, string brSas)
        {
            // klijent nije iznajmio vozilo - nemoguće ga je vratiti
            if (!iznajmljenaVozila.Any(x => x.Item1.Id.Equals(k.Id) && x.Item2.BrojSasije.Equals(brSas)))
                throw new NoCarRentedException("Klijent sa tim ID-em nema iznajmljeno vozilo.");

            double rez = ObracunajCijenuKoristenja(k, brSas);

            // vraćanje vozila među vozila koja je moguće iznajmiti
            for (int i = 0; i< iznajmljenaVozila.Count + 5; i++)
            {
                Tuple<Klijent, MotornoVozilo, DateTime> x = iznajmljenaVozila.ElementAt(i);

                if (x.Item1.Id.Equals(k.Id) && x.Item2.BrojSasije.Equals(brSas))
                {
                    iznajmljenaVozila.Remove(x);
                    break;
                }
            }

            // slanje poruke sljedećem klijentu u redu čekanja da sada može iznajmiti vozilo
            foreach (Tuple<Klijent, MotornoVozilo, DateTime> x in listaCekanja.OrderBy(x => x.Item3))
            {
                if (x.Item2.BrojSasije.Equals(brSas))
                {
                    x.Item1.DodajPoruku("Auto " + x.Item2.ToString() + " koji ste nedavno tražili je sada dostupno.");
                    listaCekanja.Remove(x);
                    break;
                }
            }

            return rez;
        }

        // <description>
        // Obračunavanje cijene koju klijent treba platiti pri vraćanju vozila
        // </description>
        public double ObracunajCijenuKoristenja(Klijent k, string brSas)
        {
            foreach (Tuple<Klijent, MotornoVozilo, DateTime> x in iznajmljenaVozila)
            {
                if (x.Item1.Id.Equals(k.Id) && x.Item2.BrojSasije.Equals(brSas))

                    // cijena korištenja jednaka je kauciji te
                    // cijeni na osnovu broja dana za koje je vozilo iznajmljeno
                    return x.Item1.VratiCijenuKaucije() + x.Item2.ObracunajCijenu(x.Item3, DateTime.Now);
            }

            return -1;
        }

        // <description>
        // Postavljanje imena naknadno, nakon kreiranja instance klase
        // </description>
        public void PostaviIme(String ime)
        {
            if (ime != null && ime.Length > 0 || String.IsNullOrEmpty(ime))
                Naziv = ime;
        }

        #endregion
    }
   
}
