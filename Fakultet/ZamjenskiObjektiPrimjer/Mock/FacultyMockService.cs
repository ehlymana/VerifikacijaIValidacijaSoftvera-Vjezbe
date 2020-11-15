using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Mock
{
    public class FacultyMockService: IFacultyService
    {
        IList<Student> _students = new List<Student>();
        IList<Potvrda> _potvrde = new List<Potvrda>();
        
        public void addStudent(Student student) {
            _students.Add(student);
        }
            
        public Boolean studentExists(uint id) {

            foreach (Student s in _students)
                if (s.Id == id)
                    return true;

            return false;
        }

        public Student findStudent(uint id)
        {
            foreach (Student s in _students)
                if (s.Id == id)
                    return s;

            return null;
        }

        public void addConfirmation(uint id, String tip)
        {
            if (studentExists(id))
            {
                Potvrda p = new Potvrda(findStudent(id), tip);
                _potvrde.Add(p);
            }
        }

        public Boolean confirmationExists(uint id, String tip)
        {
            foreach (Potvrda p in _potvrde)
                if (p.Student.Id == id && p.TipPotvrde.Equals(tip))
                    return true;

            return false;
        }

        public Potvrda findConfirmation(uint id, String tip)
        {
            foreach (Potvrda p in _potvrde)
                if (p.Student.Id == id && p.TipPotvrde.Equals(tip))
                    return p;

            return null;
        }

        public Int32 numberOfStudents()
        {
            return _students.Count;
        }
    }
}
