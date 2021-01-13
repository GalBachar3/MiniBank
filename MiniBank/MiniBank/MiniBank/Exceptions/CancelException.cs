using System;

namespace MiniBank.Exceptions
{
    public class CancelException : Exception
    {
        public CancelException()
        {
        }

        public CancelException(string message) : base(message)
        {
        }

        public CancelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
