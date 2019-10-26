using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPKKS.Klase
{
    public class DrawingAccount : Account
    {
        private static Decimal WithdrawLimit = 1000;

        public DrawingAccount(Int32 accountIdentity, Decimal startingBalance)
            : base(accountIdentity, startingBalance)
        {

        }

        // override for specific behavior when withdrawing 
        override public void withdraw(Decimal amount)
        {
            if (amount < 0)
            {
                throw new Exception("Amount's got to be positive in order to withdraw money.");
            }
            else if (amount > WithdrawLimit)
            {
                throw new Exception("Amount's got to be smaller than security limit for single transaction.");
            }
            _balance -= amount;
        }
    }
}
