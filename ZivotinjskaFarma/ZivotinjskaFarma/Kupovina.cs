using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public class Kupovina
    {
        #region Atributi

        string IDKupca;
        DateTime datumKupovine, rokIsporuke;
        Proizvod kupljeniProizvod;
        int kolicina;
        bool popust;
        static int brojac = 1;

        #endregion

        #region Properties

        public string IDKupca1 { get => IDKupca; set => IDKupca = value; }
        public DateTime DatumKupovine { get => datumKupovine; set => datumKupovine = value; }
        public DateTime RokIsporuke { get => rokIsporuke; set => rokIsporuke = value; }
        public Proizvod KupljeniProizvod { get => kupljeniProizvod; set => kupljeniProizvod = value; }
        public int Kolicina { get => kolicina; set => kolicina = value; }
        public bool Popust { get => popust; set => popust = value; }

        #endregion

        #region Konstruktor

        public Kupovina(string id, DateTime datumK, DateTime rok, Proizvod proizvod, int kol, bool popust)
        {
            IDKupca = id;
            DatumKupovine = datumK;
            RokIsporuke = rok;
            KupljeniProizvod = proizvod;
            Kolicina = kol;
            Popust = popust;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u okviru koje se verificira ispravnost kupovine.
        /// Tražena količina ne može biti veća od količine proizvoda koja je na stanju.
        /// Za mlijeko, jaja i sir je moguć rok isporuke od 2 do 7 dana od dana kupovine.
        /// Za vunu je moguć rok isporuke od najmanje 30 dana od dana kupovine.
        /// Ukoliko se kupovina ne može izvršiti zbog jednog od razloga navedenih iznad, 
        /// potrebno je vratiti FALSE, a u suprotnom je potrebno vratiti TRUE.
        /// </summary>
        /// <returns></returns>
        public bool VerificirajKupovinu()
        {
            //throw new NotImplementedException();
            return true;
        }

        public static int DajSljedeciBroj()
        {
            return brojac++;
        }

        #endregion
    }
}
