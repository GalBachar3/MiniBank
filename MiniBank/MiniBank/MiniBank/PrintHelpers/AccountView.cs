using System;
using System.Collections.Generic;
using FluentNHibernate.Conventions;
using MiniBank.Models;
using MiniBank.Resources;

namespace MiniBank.PrintHelpers
{
    public class AccountView
    {
        public void PrintAccountDetails(Account account)
        {
            Console.WriteLine(MenuMessages.AccountDetails,account.GetType().Name, account.Id, account.Balance);
        }

        public void PrintAllAccounts(List<Account> accounts)
        {
            if (!accounts.IsEmpty())
            {
                foreach (var account in accounts)
                { 
                    PrintAccountDetails(account);
                }
            }
            else
            {
                Console.WriteLine(MenuMessages.NoAccountsExistMessage);
            }
        }
    }
}
