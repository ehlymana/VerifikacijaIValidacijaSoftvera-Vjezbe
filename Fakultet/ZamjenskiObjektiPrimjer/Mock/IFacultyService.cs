using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Mock
{
    public interface IFacultyService
    {
        void addStudent(Student s);
        Boolean studentExists(uint id);
        Student findStudent(uint id);

        void addConfirmation(uint id, String tip);
        Boolean confirmationExists(uint id, String tip);
        Potvrda findConfirmation(uint id, String tip);

        Int32 numberOfStudents();
    }
}
