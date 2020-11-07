using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class HitniPacijent : Pacijent
    {
        string opisPrvePomoci, razlogPrvePomoci;
        bool smrt;
        DateTime vrijemeSmrti, obdukcija;
        string uzrokSmrti;

        public string OpisPrvePomoci
        {
            get
            {
                return opisPrvePomoci;
            }

            set
            {
                opisPrvePomoci = value;
            }
        }

        public string RazlogPrvePomoci
        {
            get
            {
                return razlogPrvePomoci;
            }

            set
            {
                razlogPrvePomoci = value;
            }
        }

        public bool Smrt
        {
            get
            {
                return smrt;
            }

            set
            {
                smrt = value;
            }
        }

        public DateTime VrijemeSmrti
        {
            get
            {
                return vrijemeSmrti;
            }

            set
            {
                vrijemeSmrti = value;
            }
        }

        public DateTime Obdukcija
        {
            get
            {
                return obdukcija;
            }

            set
            {
                obdukcija = value;
            }
        }

        public string UzrokSmrti
        {
            get
            {
                return uzrokSmrti;
            }

            set
            {
                uzrokSmrti = value;
            }
        }
        public HitniPacijent(string i, string p, DateTime r, string m, string s, string a, string b, string z, DateTime pr, string zdr, string o, string rp)
            : base(i, p, r, m, s, a, b, z, pr, zdr)
        {
            OpisPrvePomoci = o;
            RazlogPrvePomoci = rp;
            Smrt = false;
        }
        public HitniPacijent (string i, string p, DateTime r, string m, string s, string a, string b, string z, DateTime pr, string zdr, string o, string rp, DateTime vr, DateTime ob, string uz)
            : base(i, p, r, m, s, a, b, z, pr, zdr)
        {
            OpisPrvePomoci = o;
            RazlogPrvePomoci = rp;
            VrijemeSmrti = vr;
            Obdukcija = ob;
            UzrokSmrti = uz;
            Smrt = true;
        }
    }
}
