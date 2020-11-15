using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZamjenskiObjektiPrimjer.Klase;
using ZamjenskiObjektiPrimjer.Stub;

namespace StudentService_Test
{
    /// <summary>
    /// Summary description for Stub_Tests
    /// </summary>
    [TestClass]
    public class Stub_Tests
    {
        StudentService studentService;

        public Stub_Tests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void PripremaPodataka_StubTest()
        {
            studentService = new StudentService(new StubFaculty());
        }

        [TestMethod]
        public void ImeFakulteta_StubTest()
        {
            StringAssert.Equals(studentService.imeFakulteta(), "Elektrotehnicki Fakultet");
        }

        [TestMethod]
        public void BrojOdjeljenja_StubTest()
        {
            Assert.AreEqual(studentService.odjeljenjaFakulteta().Count, 4);
        }

        [TestMethod]
        public void ProvjeraPotvrde_StubTest()
        {
            studentService.zahtjevZaPotvrdu(666, "Potvrda o redovnom studiju");
            Assert.IsFalse(studentService.vratiPotvrdu(666, "Potvrda o redovnom studiju").Obradjena);
        }

        [TestMethod]
        public void ProvjeraPodnositeljaPrijave_StubTest()
        {
            studentService.zahtjevZaPotvrdu(666, "Potvrda o redovnom studiju");
            StringAssert.Equals(studentService.vratiPotvrdu(666, "Potvrda o redovnom studiju").Student.Ime, "Haris");
        }

        [TestMethod]
        public void TestiranjeBrojaPoslanihObavijesti_StubTest()
        {
            IList<Student> st = studentService.posaljiObavijestStudentima("Haris", "Poruka svim Harisima.");

            Assert.AreEqual(10, st.Count);
        }

        [TestMethod]
        public void TestiranjeSlanjaObavijesti_StubTest()
        {
            IList<Student> st = studentService.posaljiObavijestStudentima("Haris", "Poruka svim Harisima.");
            String obavijest = "";

            foreach(Student s in st)
                if(s.Ime.Equals("Haris"))
                    obavijest = s.Obavijesti[0];

            Assert.AreEqual("Poruka svim Harisima.", obavijest);
        }
    }
}
