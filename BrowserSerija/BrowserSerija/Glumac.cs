using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserSerija
{
    public class Glumac
    {
        #region Atributi

        string ime, nacionalnost;
        DateTime datumRođenja;
        double popularnost;
        int ukupanBrojSerija;

        #endregion

        #region Properties

        public string Ime 
        { 
            get => ime;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("Ime glumca ne može biti prazno!");
                ime = value;
            }
        }
        public string Nacionalnost 
        { 
            get => nacionalnost;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new FormatException("Svaki glumac mora imati nacionalnost!");
                nacionalnost = value;
            }
        }
        public DateTime DatumRođenja 
        { 
            get => datumRođenja;
            set
            {
                if (value > DateTime.Now)
                    throw new FormatException("Datum rođenja ne može biti u budućnosti!");
                datumRođenja = value;
            }
        }
        public double Popularnost { get => popularnost; }

        public int UkupanBrojSerija { get => ukupanBrojSerija; }

        #endregion

        #region Konstruktor

        public Glumac(string ime, string država, DateTime rođenje)
        {
            Ime = ime;
            Nacionalnost = država;
            DatumRođenja = rođenje;
            popularnost = 0.0;
            ukupanBrojSerija = 0;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda koja izračunava novu popularnost glumca na osnovu učešća u novoj seriji.
        /// Prvenstveno je neophodno povećati ukupni broj serija glumca, a zatim novu
        /// popularnost izračunati na sljedeći način:
        /// - ukoliko je popularnost serije veća od 8.0, koeficijent dodatne popularnosti je 0.2.
        /// - ukoliko je popularnost serije veća od 5.0, koeficijent dodatne popularnosti je 0.1.
        /// U suprotnom je koeficijent dodatne popularnosti jednak 0.
        /// - ukoliko je trenutna popularnost glumca veća od 4.99, pomnožiti trenutnu popularnost sa
        /// koeficijentom dodatne popularnosti i dodati na trenutnu popularnost.
        /// U suprotnom na trenutnu popularnost dodati 0.5 bez upotrebe koeficijenta
        /// dodatne popularnosti.
        /// Ukoliko su popularnost glumca i serije manji od 2.0, od ukupne popularnosti glumca
        /// oduzeti 0.5.
        /// Koeficijent popularnosti ne može biti manji od 0.0 niti veći od 10.0.
        /// </summary>
        /// <param name="popularnostSerije"></param>
        public void ZabilježiUčešćeUSeriji(double popularnostSerije)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
