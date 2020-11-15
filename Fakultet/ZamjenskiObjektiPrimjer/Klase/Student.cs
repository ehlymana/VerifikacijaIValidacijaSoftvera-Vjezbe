using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamjenskiObjektiPrimjer.Klase
{
    public class Student
    {
        uint _id;
        String _ime;
        String _prezime;
        String _brojIndeksa;
        List<String> _obavijesti;

        public Student() {}

        public Student(uint id, String i, String p, String br)
        {
            Id = id;
            Ime = i;
            Prezime = p;
            BrojIndeksa = br;

            _obavijesti = new List<string>();
        }

        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public String Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        public String BrojIndeksa
        {
            get { return _brojIndeksa; }
            set { _brojIndeksa = value; }
        }

        public List<String> Obavijesti
        {
            get { return _obavijesti; }
            set { _obavijesti = value; }
        }

        public Boolean DaLiJeJednak(Student s)
        {
            return s.Id == Id;
        }


    }
}
