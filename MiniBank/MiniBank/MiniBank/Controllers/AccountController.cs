using MiniBank.Factories;
using MiniBank.Models;
using MiniBank.NhibernateTools;
using NHibernate;

namespace MiniBank.Controllers
{
    public class AccountController
    {
        protected ISession Session { get; }

        public AccountController()
        { 
            Session = FluentNHibernateHelper.Session;
        }

        public virtual void Withdraw(Account account,double sum)
        {
            new AccountControllerFactory()
                .GetAccountController(account.GetType()).Withdraw(account,sum);
        }

        public void Deposit(Account account,double sum)
        {
            using (var transaction = Session.BeginTransaction())
            {
                account.Balance += sum;
                Session.Update(account);
                transaction.Commit();
            }

                
        }

        public Account GetAccountById(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var account = Session.Get<Account>(id);
                transaction.Commit();

                return account;
            }

                
        }
    }
}