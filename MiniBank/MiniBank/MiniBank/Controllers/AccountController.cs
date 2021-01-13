using MiniBank.Exceptions;
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

        public void Withdraw(Account account, double sum)
        {
            if (account.GetType() == typeof(SimpleAccount))
            {
                WithdrawSimpleAccount(account, sum);
            }
            else
            {
                WithdrawVipAccount(account, sum);
            }
        }

        public void WithdrawSimpleAccount(Account account, double sum)
        {
            if (account.Balance - sum < 0)
            {
                throw new OverdraftException(account);
            }

            using (var transaction = Session.BeginTransaction())
            {
                account.Balance -= sum;
                Session.Update(account);
                transaction.Commit();
            }
        }

        public void WithdrawVipAccount(Account account, double sum)
        {
            using (var transaction = Session.BeginTransaction())
            {
                account.Balance -= sum;
                Session.Update(account);
                transaction.Commit();
            }
        }

        public void Deposit(Account account, double sum)
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
            var account = Session.Get<Account>(id);

            if (account == null)
            {
                throw new EntityNotFoundException("the entity is not exist in the db");
            }

            return account;
        }
    }
}
