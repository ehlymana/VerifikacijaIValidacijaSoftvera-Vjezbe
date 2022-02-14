using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatnaKlinika
{
    public class NemedicinskoOsoblje : Osoblje
    {
        //budući da ništa u sistemu nije razvijeno za korištenje od strane nemedicinskog osoblja, ova klasa će biti prazna dok se isto ne nadogradi

        public NemedicinskoOsoblje (string i, string p, string u, string pa) : base(i, p, u, pa) { }
    }
}
