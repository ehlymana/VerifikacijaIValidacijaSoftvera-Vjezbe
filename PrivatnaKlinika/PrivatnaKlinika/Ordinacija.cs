using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class Ordinacija
    {
        string ime;
        int zauzetost = 0;
        List<Pacijent> pacijentiURedu;
        bool uKvaru, privremenoZatvori;

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
            }
        }

        public List<Pacijent> PacijentiURedu
        {
            get
            {
                return pacijentiURedu;
            }

            set
            {
                pacijentiURedu = value;
            }
        }

        public int Zauzetost
        {
            get
            {
                return zauzetost;
            }

            set
            {
                zauzetost = value;
            }
        }

        public bool UKvaru
        {
            get
            {
                return uKvaru;
            }

            set
            {
                uKvaru = value;
            }
        }

        public bool PrivremenoZatvori
        {
            get
            {
                return privremenoZatvori;
            }

            set
            {
                privremenoZatvori = value;
            }
        }

        public Ordinacija (string i)
        {
            Ime = i;
            pacijentiURedu = new List<Pacijent>();
        }
        public void dodajPacijenta (Pacijent p)
        {
            pacijentiURedu.Add(p);
            Zauzetost++;
        }
        public Pacijent sljedeci ()
        {
            if (zauzetost == 0) throw new InvalidOperationException("Nema pacijenata u ordinaciji");
            Zauzetost--;
            Pacijent p = pacijentiURedu[0];
            pacijentiURedu.RemoveAt(0);
            return p;
        }
    }
}
