using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackBoxTestiranje
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver driver;

        [ClassInitialize]
        public static void Inicijalizacija(TestContext context)
        {
            driver = new ChromeDriver();
            string urlStranice = "https://localhost:44369/";
            driver.Navigate().GoToUrl(urlStranice);
        }

        [TestMethod]
        public void TestJednaValidacija()
        {
            // prelazak na stranicu za dodavanje rezervacije
            IWebElement buttonDodajRezervaciju = driver.FindElement(By.Id("buttonDodaj"));
            buttonDodajRezervaciju.Click();
            Thread.Sleep(100);

            // unos neispravne vrijednosti broja leta
            IWebElement inputBrojLeta = driver.FindElement(By.Id("inputBrojLeta"));
            inputBrojLeta.SendKeys("1");
            Thread.Sleep(100);

            // pokušaj unosa leta
            IWebElement buttonDodajLet = driver.FindElement(By.Id("buttonDodajLet"));
            buttonDodajLet.Click();
            Thread.Sleep(100);

            // provjera neispravne validacije
            IWebElement validacija = driver.FindElement(By.Id("validacijaBrojLeta"));
            Assert.AreEqual(validacija.Text, "Broj leta je u formatu AA-123");

            // ispravka vrijednosti
            inputBrojLeta = driver.FindElement(By.Id("inputBrojLeta"));
            inputBrojLeta.Clear();
            inputBrojLeta.SendKeys("AA-123");
            Thread.Sleep(100);

            // provjera ispravne validacije
            buttonDodajLet = driver.FindElement(By.Id("buttonDodajLet"));
            buttonDodajLet.Click();
            Thread.Sleep(100);

            validacija = driver.FindElement(By.Id("validacijaBrojLeta"));
            Assert.IsTrue(validacija.Text.Length == 0);

            // prikaz alerta
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("alert('Uspješno!');");
        }

        static DateTime datumOK = DateTime.Now.AddDays(5);
        static DateTime datumNOK = DateTime.Now.AddDays(-5);
        static string formatDatumOK = datumOK.Day + "-" + datumOK.Month + "-" + datumOK.Year;
        static string formatDatumNOK = datumNOK.Day + "-" + datumNOK.Month + "-" + datumNOK.Year;

        static IEnumerable<object[]> Letovi
        {
            get
            {
                return new[]
                {
                    new object[] { null, formatDatumNOK },
                    new object[] { "**-*", formatDatumOK },
                    new object[] {"AA-123", null },
                    new object[] {"AA-123", formatDatumNOK }
                };
            }
        }

        [TestMethod]
        [DynamicData("Letovi")]
        public void TestSveValidacije(string brojLeta, string odlazak)
        {
            string urlStranice = "https://localhost:44369/Home/Create/";
            driver.Navigate().GoToUrl(urlStranice);

            // unos broja leta
            if (brojLeta != null)
            {
                IWebElement inputBrojLeta = driver.FindElement(By.Id("inputBrojLeta"));
                inputBrojLeta.SendKeys(brojLeta);
                Thread.Sleep(100);
            }

            // unos datuma odlaska
            if (odlazak != null)
            {
                string[] dijelovi = odlazak.Split("-");
                IWebElement dateTimeOdlazak = driver.FindElement(By.Id("inputOdlazak"));
                dateTimeOdlazak.SendKeys(dijelovi[0]);
                Thread.Sleep(100);
                dateTimeOdlazak.SendKeys(dijelovi[1]);
                Thread.Sleep(100);
                dateTimeOdlazak.SendKeys(Keys.Tab);
                Thread.Sleep(100);
                dateTimeOdlazak.SendKeys(dijelovi[2]);
                Thread.Sleep(100);
            }

            // unos datuma povratka - uvijek ispravan da bi se triggerovala validacija
            string[] dijeloviDatuma = formatDatumOK.Split("-");
            IWebElement dateTimePovratak = driver.FindElement(By.Id("inputPovratak"));
            dateTimePovratak.SendKeys(dijeloviDatuma[0]);
            Thread.Sleep(100);
            dateTimePovratak.SendKeys(dijeloviDatuma[1]);
            Thread.Sleep(100);
            dateTimePovratak.SendKeys(Keys.Tab);
            Thread.Sleep(100);
            dateTimePovratak.SendKeys(dijeloviDatuma[2]);
            Thread.Sleep(100);

            // pokušaj unosa leta
            IWebElement buttonDodajLet = driver.FindElement(By.Id("buttonDodajLet"));
            buttonDodajLet.Click();
            Thread.Sleep(100);

            // provjera neispravnih validacija za broj leta
            IWebElement validacijaBrojLeta = driver.FindElement(By.Id("validacijaBrojLeta"));
            string očekivanaVrijednost = "The Broj leta: field is required.";
            if (brojLeta != null && brojLeta.Length < 6)
                očekivanaVrijednost = "Broj leta je u formatu AA-123";
            else if (brojLeta != null && brojLeta.Length == 6)
                očekivanaVrijednost = "";

            Assert.AreEqual(validacijaBrojLeta.Text, očekivanaVrijednost);

            // provjera neispravnih validacija za datum odlaska
            IWebElement validacijaOdlazak = driver.FindElement(By.Id("validacijaOdlazak"));
            očekivanaVrijednost = "The Datum odlaska: field is required.";
            if (odlazak != null && (brojLeta == null || brojLeta.Length != 6))
                očekivanaVrijednost = "";
            else if (odlazak != null && !odlazak.StartsWith(formatDatumOK.Substring(0, 2)))
                očekivanaVrijednost = "Možete rezervisati samo letove između 1 i 30 dana nakon dana rezervacije!";
            else if (odlazak != null)
                očekivanaVrijednost = "";

            Assert.AreEqual(validacijaOdlazak.Text, očekivanaVrijednost);
        }
    }
}
