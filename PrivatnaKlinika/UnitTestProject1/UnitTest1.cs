using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrivatnaKlinika;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;
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

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\data.csv", "data#csv", DataAccessMethod.Sequential), DeploymentItem("data.csv")]
        public void TestCSVPacijenti()
        {
            // dobavljanje redova podataka
            string ime = Convert.ToString(TestContext.DataRow["Ime"]),
                prezime = Convert.ToString(TestContext.DataRow["Prezime"]);
            int dan = Convert.ToInt32(TestContext.DataRow["RodjenjeDan"]),
                mjesec = Convert.ToInt32(TestContext.DataRow["RodjenjeMjesec"]),
                godina = Convert.ToInt32(TestContext.DataRow["RodjenjeGodina"]);
            string maticni = Convert.ToString(TestContext.DataRow["Maticni"]),
                spol = Convert.ToString(TestContext.DataRow["Spol"]),
                adresa = Convert.ToString(TestContext.DataRow["Adresa"]),
                bracnoStanje = Convert.ToString(TestContext.DataRow["BracnoStanje"]),
                zeljeniPregled = Convert.ToString(TestContext.DataRow["ZeljeniPregled"]),
                knjizica = Convert.ToString(TestContext.DataRow["ZdravstvenaKnjizica"]);

            // pokušaj kreiranja Pacijenta s ispravnim podacima
            Pacijent p = new Pacijent(ime, prezime, new DateTime(godina, mjesec, dan),
                maticni, spol, adresa, bracnoStanje, zeljeniPregled, DateTime.Now, knjizica);

            // pacijent bi se trebao ispravno kreirati - provjera da li se u konstruktoru došlo
            // do posljednje linije gdje se broj posjeta inicijalizira na 1
            Assert.AreEqual(1, p.BrojPosjeta);
        }
    }
}
