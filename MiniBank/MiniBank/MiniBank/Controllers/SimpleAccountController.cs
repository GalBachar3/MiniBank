using MiniBank.Exceptions;
using MiniBank.Models;
using NHibernate;

namespace MiniBank.Controllers
{
    public class SimpleAccountController : AccountController
    {
        public override void Withdraw(Account account, double sum)
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
    }
}
