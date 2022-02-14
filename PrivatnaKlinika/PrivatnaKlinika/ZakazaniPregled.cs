using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class ZakazaniPregled
    {
        Ordinacija ime;
        DateTime krajnjiRokZaNalaze;
        public ZakazaniPregled(Ordinacija i, DateTime k)
        {
            Ime = i;
            KrajnjiRokZaNalaze = k;
        }
        public ZakazaniPregled(Ordinacija i)
        {
            Ime = i;
        }

        public Ordinacija Ime
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

        public DateTime KrajnjiRokZaNalaze
        {
            get
            {
                return krajnjiRokZaNalaze;
            }

            set
            {
                krajnjiRokZaNalaze = value;
            }
        }

    }
}
