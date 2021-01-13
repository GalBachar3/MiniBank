using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Exceptions
{
    public class AccountTypeNotExistException : Exception
    {
        public AccountTypeNotExistException()
        {
        }

        public AccountTypeNotExistException(string message) : base(message)
        {
        }

        public AccountTypeNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
