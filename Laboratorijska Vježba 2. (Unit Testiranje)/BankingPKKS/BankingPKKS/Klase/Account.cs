using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS.Klase
{
    abstract public class Account
    {
        public Int32 AccountIdentity { get; set; }

        protected Decimal _balance;

        protected Account(Int32 identity, Decimal startingBalance)
        {
            AccountIdentity = identity;
            _balance = startingBalance;
        }

        public Decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        public virtual void deposit(Decimal amount)
        {
            if (amount < 0)
            {
                throw new Exception("Amount's got to be positive in order to deposit money.");
            }
            _balance += amount;
        }

        public virtual void withdraw(Decimal amount)
        {
            if (amount < 0)
            {
                throw new Exception("Amount's got to be positive in order to withdraw money.");
            }
            _balance -= amount;
        }
    }
}
