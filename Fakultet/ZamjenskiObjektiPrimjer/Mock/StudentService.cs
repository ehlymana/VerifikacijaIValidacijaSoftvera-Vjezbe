using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Mock
{
    public class StudentService
    {
        public IFacultyService _service;
        
        public StudentService(IFacultyService service)
        {
            _service = service;
        }
        
        public void RegisterStudent(uint id, String ime, String prezime, String brIndeksa)
        {
            Student s = new Student(id, ime, prezime, brIndeksa);
            _service.addStudent(s);
        }

        public Boolean StudentRegistered(uint id)
        {
            return _service.studentExists(id);
        }

        public void notifyStudent(uint id, String message)
        {
            if (_service.studentExists(id))
                _service.findStudent(id).Obavijesti.Add(message);
        }

        public void addConfirmation(uint id, String tip)
        {
            _service.addConfirmation(id, tip);
        }

        public Int32 numberOfStudents()
        {
            return _service.numberOfStudents();
        }
    }
}
