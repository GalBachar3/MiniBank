using System;
using System.Collections.Generic;
using MiniBank.Controllers;
using MiniBank.Models;

namespace MiniBank.Factories
{
    public class AccountControllerFactory
    {
        private Dictionary<Type, Func<AccountController>> Controllers { get; }
        
        public AccountControllerFactory()
        {
            Controllers = new Dictionary<Type, Func<AccountController>> {
                { typeof(SimpleAccount), () => new SimpleAccountController() },
                {typeof(VipAccount),()=>new VipAccountController()} };
        }

        public AccountController GetAccountController(Type type)
        {
            return Controllers[type]();
        }
    }
}
