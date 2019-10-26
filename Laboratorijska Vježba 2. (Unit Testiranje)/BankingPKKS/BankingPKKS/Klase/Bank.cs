using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS.Klase
{
    public class Bank
    {
        private List<Client> Clients { get; set; }
        private List<Account> Accounts { get; set; }

        public Bank()
        {
            Clients = new List<Client>();
            Accounts = new List<Account>();
        }

        public void addPersonClient(Person p)
        {
            Clients.Add(p);
        }

        public void openDrawingAccountForPerson(Person p)
        {
            DrawingAccount acc = new DrawingAccount(Accounts.Count, 0);
            Accounts.Add(acc);
            p.Accounts.Add(acc);
        }

        public Client findClient(Int32 identity)
        {
            return Clients.Single(person => person.Identity == identity);
        }

        public void depositMoney(DrawingAccount acc, Decimal amount)
        {
            acc.deposit(amount);
        }

        private String projectClientToString(Client c)
        {
            if (c is Person)
            {
                return (c as Person).Name;
            }
            return "";
        }
    }
}
