using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Stub
{
    public interface IFaculty 
    {
        string getName();
        Student getStudent(uint id);
        IList<Department> getDepartments();
        IList<Student> findStudents(string keyword);
    }
}
