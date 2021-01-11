using System.Collections.Generic;
using NHibernate;

namespace MiniBank.Controllers
{
    public abstract class Account
    {
        public Models.Account Model { get; set; }
        public Views.Account View { get; set; }
        public ISession Session { get; set; }

        protected Account(ISession session)
        {
            Session = session;
        }

        public abstract void Withdraw(double sum);

        public void Deposit(double sum)
        {
            Model.Balance += sum;
            var transaction = Session.BeginTransaction();
            Session.Update(Model);
            transaction.Commit();
        }

        public void PrintAccountWithView()
        {
            View.PrintAccountDetails(Model);
        }
    }
}