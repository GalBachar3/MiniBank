using System;
using System.Collections.Generic;
using MiniBank.Controllers;
using NHibernate;

namespace MiniBank.Factories
{
    public class AccountControllerFactory
    {
        public Dictionary<Type, Func<Account>> Controllers { get; set; }
        
        public AccountControllerFactory(ISession session)
        {
            Controllers = new Dictionary<Type, Func<Account>> {
                { typeof(Models.SimpleAccount), () => new SimpleAccount(session) },
                {typeof(Models.VipAccount),()=>new VipAccount(session)} };
        }

        public Account GetAccountController(Type type)
        {
            return Controllers[type]();
        }
    }
}
