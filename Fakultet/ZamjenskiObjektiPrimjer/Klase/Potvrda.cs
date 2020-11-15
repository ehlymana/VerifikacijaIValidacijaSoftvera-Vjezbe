using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamjenskiObjektiPrimjer.Klase
{
    public class Potvrda
    {
        Student _student;
        String _tipPotvrde;
        Boolean _obradjena;

        public Potvrda(Student s, String t)
        {
            Student = s;
            TipPotvrde = t;
            Obradjena = false;
        }

        public Student Student
        {
            get { return _student; }
            set { _student = value; }
        }

        public String TipPotvrde
        {
            get { return _tipPotvrde; }
            set { _tipPotvrde = value; }
        }

        public Boolean Obradjena
        {
            get { return _obradjena; }
            set { _obradjena = value; }
        }

        public void oznaciObradjenuPotvrdu()
        {
            Obradjena = true;
        }
    }
}
