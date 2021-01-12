using System;
using System.Collections.Generic;
using MiniBank.Enums;
using MiniBank.Models;

namespace MiniBank.Factories
{
    public class AccountFactory
    {
        private Dictionary<Accounts, Func<Account>> AccountsCtor { get; }

        public AccountFactory()
        {
            AccountsCtor = new Dictionary<Accounts, Func<Account>>
            {
                { Accounts.SimpleAccount, () => new SimpleAccount()},
                {Accounts.VipAccount, () => new VipAccount()}
            };
        }

        public Account GetAccount(Accounts type)
        {
            return AccountsCtor[type]();
        }
    }
}
