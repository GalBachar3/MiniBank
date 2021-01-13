using System;
using NHibernate;

namespace MiniBank.NhibernateTools
{
    public class TransactionHelper : IDisposable
    {
        public ITransaction Transaction { get; set; }
        
        public void Dispose()
        {
            Transaction.Commit();
        }
    }
}
