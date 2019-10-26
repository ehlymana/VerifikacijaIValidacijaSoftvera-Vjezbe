using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS.Klase
{
    abstract public class Client
    {
        public Int32 Identity { get; set; }
        public List<Account> Accounts { get; set; }

        protected Client(Int32 identity)
        {
            Identity = identity;
            Accounts = new List<Account>();
        }
    }
}
