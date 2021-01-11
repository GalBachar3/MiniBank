using System;
using System.Collections.Generic;
using MiniBank.Enums;
using MiniBank.Models;

namespace MiniBank.Factories
{
    public class AccountFactory
    {
        public Dictionary<Accounts,Func<Account>> Accounts { get; set; }

        public AccountFactory()
        {
            Accounts = new Dictionary<Accounts, Func<Account>>
            {
                {Enums.Accounts.SimpleAccount, () => new SimpleAccount()},
                {Enums.Accounts.VipAccount, () => new VipAccount()}
            };
        }

        public Account GetAccount(Accounts type)
        {
            return Accounts[type]();
        }
    }
}
