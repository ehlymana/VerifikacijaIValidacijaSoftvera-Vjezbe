using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZamjenskiObjektiPrimjer.Klase;
using ZamjenskiObjektiPrimjer.Mock;

namespace StudentService_Test
{
    /// <summary>
    /// Summary description for Mock_Tests
    /// </summary>
    [TestClass]
    public class Mock_Tests
    {
        IFacultyService facultyService;
        StudentService service;

        public Mock_Tests()
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
        public void PripremaPodataka_MockTest()
        {
            facultyService = new FacultyMockService();
            service = new StudentService(facultyService);
        }

        [TestMethod]
        public void RegistrujStudenta_MockTest()
        {
            service.RegisterStudent(666, "Haris", "Hasic", "666");
            Assert.IsTrue(facultyService.studentExists(666));
        }

        [TestMethod]
        public void PronadjiStudenta_MockTest()
        {
            service.RegisterStudent(666, "Haris", "Hasic", "666");
            StringAssert.Equals("Haris Hasic", facultyService.findStudent(666).Ime + " " + facultyService.findStudent(666).Prezime);
        }

        [TestMethod]
        public void ObavijestiStudenta_MockTest()
        {
            service.RegisterStudent(666, "The", "Beast", "666");
            service.RegisterStudent(667, "Neighbour of the", "Beast", "667");
            service.RegisterStudent(555, "Marvin", "The Martian", "555");

            service.notifyStudent(666, "Woe to you, oh earth and sea.");

            StringAssert.Equals("Woe to you, oh earth and sea.", facultyService.findStudent(666).Obavijesti[0]);
        }

        [TestMethod]
        public void ZahtjevZaPotvrdom_MockTest()
        {
            service.RegisterStudent(666, "Haris", "Hasic", "666");
            service.addConfirmation(666, "Potvrda o redovnom studiju");

            Assert.IsTrue(facultyService.confirmationExists(666, "Potvrda o redovnom studiju"));
        }
    }
}
