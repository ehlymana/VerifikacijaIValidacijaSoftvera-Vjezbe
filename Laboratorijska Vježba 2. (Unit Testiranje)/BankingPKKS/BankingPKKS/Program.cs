using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS
{
    class Program
    {
        static void Main(string[] args)
        {
            Klase.Bank b = new Klase.Bank();
            b.addPersonClient(new Klase.Person(1, "John"));

            Klase.Person p = b.findClient(1) as Klase.Person;
            b.openDrawingAccountForPerson(p);

            p.Accounts[0].deposit(550);
            Console.WriteLine("Status: {0}", b.findClient(1).Accounts[0].Balance);
        }
    }
}
