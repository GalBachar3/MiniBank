using System;
using MiniBank.Models;

namespace MiniBank.Exceptions
{
    public class OverdraftException : Exception
    {
        public OverdraftException(string message) : base(message)
        {
        }

        public OverdraftException(Account account) :
            base($"Simple Account with {account.Id} id" + $" and {account.Balance} balance , can't be over drafted")
        {
        }


    }
}
