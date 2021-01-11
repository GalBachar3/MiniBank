using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace MiniBank.Controllers
{
    public class VipAccount : Account
    {
        public VipAccount(ISession session) : base(session)
        {
            View = new Views.VipAccount();
            Model = new Models.VipAccount();
        }

        public override void Withdraw(double sum)
        {
            Model.Balance -= sum;
            var transaction = Session.BeginTransaction();
            
            Session.Update(Model);
            transaction.Commit();
        }
    }
}
