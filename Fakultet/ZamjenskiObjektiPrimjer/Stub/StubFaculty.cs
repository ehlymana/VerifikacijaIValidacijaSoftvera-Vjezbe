using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamjenskiObjektiPrimjer.Klase;

namespace ZamjenskiObjektiPrimjer.Stub
{
    public class StubFaculty : IFaculty
    {
        public string getName()
        {
            return "Elektrotehnicki Fakultet";
        }

        public Student getStudent(uint id)
        {
            return new Student(666, "Haris", "Hasic", "666");
        }

        public IList<Department> getDepartments()
        {
            IList<Department> departments = new List<Department>();
            
            departments.Add(new Department("Automatika i Elektronika"));
            departments.Add(new Department("Elektroenergetika"));
            departments.Add(new Department("Racunarstvo i Informatika"));
            departments.Add(new Department("Telekomunikacije"));

            return departments;
        }

        public IList<Student> findStudents(String keyword)
        {
            IList<Student> students = new List<Student>();
            
            students.Add(new Student(666, "Haris", "Hasic", "666"));

            for (int i = 0; i < 9; i++)
            {
                students.Add(new Student((uint)i, "Neki", "Student", Convert.ToString(1000+i)));
            }

            return students;
        }
    }
}
