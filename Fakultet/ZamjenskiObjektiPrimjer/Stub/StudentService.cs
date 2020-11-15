using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Stub
{
    public class StudentService
    {
        private IFaculty _electricalEngineering;
        List<Potvrda> _potvrde;

        public StudentService(IFaculty faculty) 
        {
            _electricalEngineering = faculty;
            _potvrde = new List<Potvrda>();
        }

        public String imeFakulteta()
        {
            return _electricalEngineering.getName();
        }

        public IList<Department> odjeljenjaFakulteta()
        {
            return _electricalEngineering.getDepartments();
        }

        public void zahtjevZaPotvrdu(uint id, String tip)
        {
            Potvrda p = new Potvrda(_electricalEngineering.getStudent(id), tip);
            _potvrde.Add(p);
        }

        public Potvrda vratiPotvrdu(uint id, String tip)
        {
            foreach(Potvrda p in _potvrde)
                if(p.Student.Id == id && p.TipPotvrde.Equals(tip))
                    return p;

            return null;
        }

        public Boolean obradiPotvrdu(uint id, String tip)
        {
            foreach (Potvrda p in _potvrde) 
                if (p.Student.Id == id && p.TipPotvrde.Equals(tip))
                {
                    p.oznaciObradjenuPotvrdu();
                    return true;
                }

            return false;
        }

        public IList<Student> posaljiObavijestStudentima(String keyword, String poruka)
        {
            IList<Student> st = _electricalEngineering.findStudents(keyword);

            foreach (Student s in st)
                s.Obavijesti.Add(poruka);

            return st;
        }
    }
}
