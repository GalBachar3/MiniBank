using MiniBank.Exceptions;
using NHibernate;

namespace MiniBank.Controllers
{
    public class SimpleAccount : Account
    {
        public SimpleAccount(ISession session) : base(session)
        {
            View = new Views.SimpleAccount();
            Model = new Models.SimpleAccount();
        }

        public override void Withdraw(double sum)
        {
            if (Model.Balance - sum < 0)
            {
                throw new OverdraftException(
                    $"Simple Account with {Model.Id} id and {Model.Balance} balance , can't be over drafted");
            }

            var transaction = Session.BeginTransaction();
            
            Model.Balance -= sum;
            Session.Update(Model);
            transaction.Commit();
        }
    }
}
