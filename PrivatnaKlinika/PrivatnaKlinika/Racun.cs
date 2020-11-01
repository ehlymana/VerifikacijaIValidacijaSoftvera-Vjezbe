using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Racun
    {
        double cijena, popust=1, rata;
        int brojRata;
        List<string> stavke;
        int osnovica = 50;

        public double Cijena
        {
            get
            {
                return Cijena1;
            }

            set
            {
                Cijena1 = value;
            }
        }

        public int BrojRata
        {
            get
            {
                return brojRata;
            }

            set
            {
                brojRata = value;
            }
        }

        public List<string> Stavke
        {
            get
            {
                return stavke;
            }

            set
            {
                stavke = value;
            }
        }

        public int Osnovica
        {
            get
            {
                return osnovica;
            }

            set
            {
                osnovica = value;
            }
        }

        public double Rata
        {
            get
            {
                return rata;
            }

            set
            {
                rata = value;
            }
        }

        public double Cijena1
        {
            get
            {
                return cijena;
            }

            set
            {
                cijena = value;
            }
        }
        public Racun ()
        {
            Stavke = new List<string>();
        }
        public void obracunaj(Pacijent p, int rate)
        {
            if (DateTime.Now.Year - p.Rodenje.Year < 18 || (DateTime.Now.Year - p.Rodenje.Year == 18 && DateTime.Now.Month < p.Rodenje.Month) || (DateTime.Now.Year - p.Rodenje.Year == 18 && DateTime.Now.Month == p.Rodenje.Month && DateTime.Now.Day < p.Rodenje.Day)) {
                if (rate == 0) popust = 0.5;
                else popust = 0.6;
            }
            else
            {
                int brojPosjeta = p.BrojPosjeta;
                if (brojPosjeta > 3 && rate == 0) popust = 0.9;
                else if (brojPosjeta < 3 && rate > 0) popust = 1.15;
            }
            foreach (Pregled pr in p.Karton.dajNajnovijePreglede()) Stavke.Add(pr.Terapija);
            Cijena = System.Convert.ToDouble(Osnovica) * Stavke.Count * popust;
            BrojRata = rate;
            if (BrojRata > 0) Rata = Cijena / BrojRata;
            else Rata = 0;
        }
    }
}
