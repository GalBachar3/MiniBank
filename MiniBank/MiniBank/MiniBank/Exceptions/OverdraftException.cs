using System;
using MiniBank.Models;

namespace MiniBank.Exceptions
{
    public class OverdraftException : Exception
    {
        public OverdraftException()
        {
        }
        
        public OverdraftException(string message) : base(message)
        {
        }

        public OverdraftException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public OverdraftException(Account account) :
            base($"Simple Account with {account.Id} id" + $" and {account.Balance} balance , can't be over drafted")
        {
        }
    }
}
