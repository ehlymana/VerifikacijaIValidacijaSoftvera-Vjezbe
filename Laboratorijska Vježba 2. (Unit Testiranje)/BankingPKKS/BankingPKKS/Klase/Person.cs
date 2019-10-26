using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS.Klase
{
    public class Person : Client
    {
        public String Name { get; set; }

        public Person(Int32 identity, String Name)
            : base(identity)
        {
            this.Name = Name;
        }
    }
}
