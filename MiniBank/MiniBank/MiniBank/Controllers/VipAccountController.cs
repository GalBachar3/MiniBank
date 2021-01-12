using MiniBank.Models;
using NHibernate;

namespace MiniBank.Controllers
{
    public class VipAccountController : AccountController
    {
        public override void Withdraw(Account account,double sum)
        {
            using (var transaction = Session.BeginTransaction())
            {
                account.Balance -= sum;
                Session.Update(account);
                transaction.Commit();
            }

                
        }
    }
}
