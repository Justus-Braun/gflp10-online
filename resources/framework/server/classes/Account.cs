using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.classes
{
    public class Account
    {
        public int balance;
        public Account(int balance)
        {
            Balance = balance;
        }

        public int Balance
        {
            get => balance;

            private set
            {
                if (value < 0)
                {
                    throw new OverflowException("Account cant go negative");
                }

                balance = value;
            }
        }

        public void Withdraw(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Amount cant be negative");

            Balance -= amount;
        }

        public void Deposit(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Amount cant be negative");
            
            Balance += amount;
        }
    }
}
