using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrivatnaKlinika;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace UnitTestovi
{
    [TestClass]
    public class UnitTest1
    {
        #region Inline Testovi

        static IEnumerable<object[]> Pacijenti
        {
            get
            {
                return new[]
                {
                new object[] { "", "Prezime", DateTime.Parse("01/01/1996"), "0101996170001", "M", "ZD-01" },
                new object[] { "Ime", "", DateTime.Parse("01/01/1996"), "0101996170001", "M", "ZD-01" },
                new object[] { "Ime", "Prezime", DateTime.Now.AddDays(1), "0101996170001", "M", "ZD-01" },
                new object[] { "Ime", "Prezime", DateTime.Parse("01/01/1996"), "5001996170001", "M", "ZD-01" },
                new object[] { "Ime", "Prezime", DateTime.Parse("01/01/1996"), "0101996170001", "Muško", "ZD-01" },
                new object[] { "Ime", "Prezime", DateTime.Parse("01/01/1996"), "0101996170001", "M", "01" }
                };
            }
        }

        static IEnumerable<object[]> Pacijenti2
        {
            get
            {
                return new[]
                {
                    new object[] {"Ime", "Prezime", DateTime.Parse("01/01/1996"), "0101996170001", "M", "ZD-01" }
                };
            }
        }

        [TestMethod]
        [DynamicData("Pacijenti")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKonstruktoraPacijenta(string ime, string prezime, DateTime rođenje, string matični, string spol, string knjižica)
        {
            Pacijent p = new Pacijent(ime, prezime, rođenje, matični, spol, "Neka adresa", "Slobodan", "Sistematski", DateTime.Now, knjižica);
        }

        [TestMethod]
        [DynamicData("Pacijenti2")]
        public void TestKonstruktoraPacijenta2(string ime, string prezime, DateTime rođenje, string matični, string spol, string knjižica)
        {
            Pacijent p = new Pacijent(ime, prezime, rođenje, matični, spol, "Neka adresa", "Slobodan", "Sistematski", DateTime.Now, knjižica);
            Assert.AreEqual(p.Maticni, matični);
            Assert.IsTrue(p.ZdravstvenaKnjizica == knjižica);
        }

        #endregion

        #region Testovi CSV

        static IEnumerable<object[]> PacijentiCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Pacijenti.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], DateTime.Parse(elements[2]), elements[3], elements[4], elements[5] };
                }
            }
        }

        [TestMethod]
        [DynamicData("PacijentiCSV")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKonstruktoraPacijentaCSV(string ime, string prezime, DateTime rođenje, string matični, string spol, string knjižica)
        {
            Pacijent p = new Pacijent(ime, prezime, rođenje, matični, spol, "Neka adresa", "Slobodan", "Sistematski", DateTime.Now, knjižica);
        }

        #endregion

        #region Testovi XML

        static IEnumerable<object[]> PacijentiXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Pacijenti.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1], DateTime.Parse(elements[2]), elements[3], elements[4], elements[5] };
            }
        }

        [TestMethod]
        [DynamicData("PacijentiXML")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKonstruktoraPacijentaXML(string ime, string prezime, DateTime rođenje, string matični, string spol, string knjižica)
        {
            Pacijent p = new Pacijent(ime, prezime, rođenje, matični, spol, "Neka adresa", "Slobodan", "Sistematski", DateTime.Now, knjižica);
        }

        #endregion

    }
}
