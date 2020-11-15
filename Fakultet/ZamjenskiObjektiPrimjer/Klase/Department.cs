using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamjenskiObjektiPrimjer.Klase
{
    public class Department
    {
        String _naziv;

        public Department() { }

        public Department(String n)
        {
            Naziv = n;
        }

        public String Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }
    }
}
