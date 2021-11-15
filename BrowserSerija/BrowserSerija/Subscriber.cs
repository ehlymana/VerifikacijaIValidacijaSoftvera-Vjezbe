using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserSerija
{
    public class Subscriber
    {
        #region Atributi

        string username, password;
        List<Serija> serijeNaKojeJePretplaćen;
        double ukupnaCijenaPretplate;
        bool pretplataPlaćena;
        DateTime rokUplate;

        #endregion

        #region Properties

        public string Username 
        { 
            get => username;
            set
            {
                bool uslov = value.All(c => Char.IsUpper(c) || Char.IsLower(c) || Char.IsDigit(c));
                if (String.IsNullOrWhiteSpace(value) || !uslov)
                    throw new InvalidCastException("Username se mora sastojati isključivo od slova i brojeva!");

                username = value;
            }
        }
        public string Password 
        { 
            get => password;
            set
            {
                bool uslov = value.Any(c => Char.IsUpper(c)) && value.Any(c => Char.IsLower(c)) && value.Any(c => Char.IsDigit(c));
                if (String.IsNullOrWhiteSpace(value) || !uslov)
                    throw new InvalidCastException("Password mora sadržavati barem jedno malo, jedno veliko slovo i jedan broj!");

                password = value;
            }
        }
        public List<Serija> SerijeNaKojeJePretplaćen { get => serijeNaKojeJePretplaćen; }
        public double UkupnaCijenaPretplate { get => ukupnaCijenaPretplate; }
        public bool PretplataPlaćena { get => pretplataPlaćena; }
        public DateTime RokUplate { get => rokUplate; }

        #endregion

        #region Konstruktor

        public Subscriber(string user, string pass)
        {
            Username = user;
            Password = pass;
            serijeNaKojeJePretplaćen = new List<Serija>();
            ukupnaCijenaPretplate = 0.0;
            pretplataPlaćena = false;
        }

        #endregion

        #region Metode

        public double OdaberiSerijeZaPretplatu(List<Serija> serije)
        {
            if (serije == null)
                throw new FormatException("Lista serija se mora specificirati!");
            double pretplata = 0.0;
            foreach (var serija in serije)
            {
                if (serija.PopularnostSerije < 2)
                    pretplata += 1.5;
                else if (serija.PopularnostSerije < 5)
                    pretplata += 4.0;
                else if (serija.PopularnostSerije < 7.5)
                    pretplata += 5.5;
                else if (serija.PopularnostSerije < 9.0)
                    pretplata += 7.5;
                else
                    pretplata += 10.0;
            }
            ukupnaCijenaPretplate = pretplata;
            pretplataPlaćena = false;
            rokUplate = DateTime.Now.AddMonths(1);
            return pretplata;
        }

        public void PlatiPretplatu(string password)
        {
            if (password == Password)
            {
                pretplataPlaćena = true;
                rokUplate = DateTime.Now.AddMonths(1);
            }
            else
                throw new AccessViolationException("Pokušaj hakovanja sistema!");
        }

        #endregion
    }
}
