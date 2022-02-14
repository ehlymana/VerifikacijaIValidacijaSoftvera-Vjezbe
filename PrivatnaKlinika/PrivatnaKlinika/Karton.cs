using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Karton
    {
        List<Pregled> prosliPregledi;
        List<ZakazaniPregled> zakazaniPregledi;
        string stanjePorodice, zakljucak;
        string sadasnjeBolesti, sadasnjeAlergije, ranijeBolesti, ranijeAlergije;
        MedicinskoOsoblje preuzeoAnamnezu;

        public List<Pregled> ProsliPregledi
        {
            get
            {
                return prosliPregledi;
            }

            set
            {
                prosliPregledi = value;
            }
        }

        public List<ZakazaniPregled> ZakazaniPregledi
        {
            get
            {
                return zakazaniPregledi;
            }

            set
            {
                zakazaniPregledi = value;
            }
        }

        public string StanjePorodice
        {
            get
            {
                return stanjePorodice;
            }

            set
            {
                stanjePorodice = value;
            }
        }

        public string Zakljucak
        {
            get
            {
                return zakljucak;
            }

            set
            {
                zakljucak = value;
            }
        }

        public string SadasnjeBolesti
        {
            get
            {
                return sadasnjeBolesti;
            }

            set
            {
                sadasnjeBolesti = value;
            }
        }

        public string SadasnjeAlergije
        {
            get
            {
                return sadasnjeAlergije;
            }

            set
            {
                sadasnjeAlergije = value;
            }
        }

        public string RanijeBolesti
        {
            get
            {
                return ranijeBolesti;
            }

            set
            {
                ranijeBolesti = value;
            }
        }

        public string RanijeAlergije
        {
            get
            {
                return ranijeAlergije;
            }

            set
            {
                ranijeAlergije = value;
            }
        }

        public MedicinskoOsoblje PreuzeoAnamnezu
        {
            get
            {
                return preuzeoAnamnezu;
            }

            set
            {
                preuzeoAnamnezu = value;
            }
        }

        public Karton(string sb, string sa, string rb, string ra, string sp, string z, MedicinskoOsoblje p)
        {
            ProsliPregledi = new List<Pregled>();
            ZakazaniPregledi = new List<ZakazaniPregled>();
            SadasnjeBolesti = sb;
            SadasnjeAlergije = sa;
            RanijeBolesti = rb;
            RanijeAlergije = ra;
            StanjePorodice = sp;
            Zakljucak = z;
            PreuzeoAnamnezu = p;
        }
        public void dodajProsliPregled(Pregled p)
        {
            ProsliPregledi.Add(p);
        }
        public void dodajZakazaniPregled(ZakazaniPregled p)
        {
            ZakazaniPregledi.Add(p);
        }
        public List<Pregled> dajNajnovijePreglede()
        {
            List<Pregled> pregledi = new List<Pregled>();
            foreach (Pregled p in ProsliPregledi)
            {
                if (p.DatumPropisivanja.Date == DateTime.Now.Date) pregledi.Add(p);
            }
            return pregledi;
        }
    }
}
