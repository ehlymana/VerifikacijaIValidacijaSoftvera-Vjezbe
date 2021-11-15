using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserSerija
{
    public class Serija
    {
        #region Atributi

        string ime, opis;
        Žanr žanr;
        List<Glumac> glumci;
        double popularnostSerije;
        int brojSezona;
        List<int> brojEpizoda;
        bool trenutnoAktivna;
        int trenutniBrojGledalaca;

        #endregion

        #region Properties

        public string Ime 
        { 
            get => ime;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new InvalidOperationException("Neispravno uneseno ime serije!");
                ime = value;
            }
        }
        public string Opis 
        { 
            get => opis;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 20)
                    throw new InvalidOperationException("Neispravno unesen opis serije!");
                opis = value;
            }
        }
        public Žanr Žanr 
        { 
            get => žanr;
            set
            {
                if (value == Žanr.Američka && !opis.Contains("SAD") ||
                    value == Žanr.Indijska && !opis.Contains("Indija") ||
                    value == Žanr.Korejanska && !opis.Contains("Koreja") ||
                    value == Žanr.Turska && !opis.Contains("Turska") ||
                    value == Žanr.Španska && !opis.Contains("Španija"))
                    throw new InvalidOperationException("Žanr mora odgovarati opisu serije!");
                žanr = value;
            }
        }
        public List<Glumac> Glumci 
        { 
            get => glumci;
            set
            {
                if (glumci != null && (value == null || value.Count < 1))
                    throw new InvalidOperationException("Neispravno unesena lista glumaca!");

                glumci = value;
            }
        }
        public double PopularnostSerije { get => popularnostSerije; }
        public int BrojSezona { get => brojSezona; }
        public List<int> BrojEpizoda { get => brojEpizoda; }
        public bool TrenutnoAktivna { get => trenutnoAktivna; }
        public int TrenutniBrojGledalaca { get => trenutniBrojGledalaca; }

        #endregion

        #region Konstruktor

        public Serija(string ime, string opis, Žanr žanr, List<Glumac> glumci = null)
        {
            Ime = ime;
            Opis = opis;
            Žanr = žanr;
            if (glumci != null)
                Glumci = glumci;
            else
                Glumci = new List<Glumac>();

            popularnostSerije = 0.0;
            brojSezona = 0;
            brojEpizoda = new List<int>();
            trenutnoAktivna = false;
            trenutniBrojGledalaca = 0;
        }

        #endregion

        #region Metode

        public void DodajGlumca(Glumac g)
        {
            if (!Glumci.Contains(g))
                Glumci.Add(g);
        }

        /// <summary>
        /// Metoda koja dodaje nove epizode seriji.
        /// Prvenstveno je neophodno ponovo aktivirati seriju, ukoliko je ista neaktivna.
        /// Ukoliko je u pitanju nova sezona, potrebno je povećati broj sezona serije za 1
        /// i broj epizoda dodati kao novi element liste epizoda.
        /// U suprotnom, potrebno je trenutnoj sezoni dodati nove epizode tako što će se
        /// povećati broj epizoda u listi epizoda za trenutnu sezonu.
        /// Kao rezultat metode potrebno je vratiti ukupan broj epizoda koje serija ima
        /// u svim sezonama.
        /// Ukoliko se proslijedi broj epizoda manji od ili jednak nuli, potrebno je
        /// baciti izuzetak.
        /// </summary>
        /// <param name="novaSezona"></param>
        /// <param name="brojEpizoda"></param>
        public int DodajEpizode(bool novaSezona, int brojEpizoda)
        {
            throw new NotImplementedException();
        }

        public void ZabilježiGledanost(bool rast, int brojGledalaca)
        {
            if (rast)
                trenutniBrojGledalaca += brojGledalaca;
            else
                trenutniBrojGledalaca = brojGledalaca;

            AžurirajPopularnost();
        }

        public void AžurirajPopularnost()
        {
            popularnostSerije = (double)trenutniBrojGledalaca / (Browser.MaksimalniZabilježeniBrojGledalaca) * 10.0;
        }

        #endregion
    }
}
