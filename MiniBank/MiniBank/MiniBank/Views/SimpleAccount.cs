using System;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class SimpleAccount:Account
    {
        public override void PrintAccountDetails(Models.Account account)
        {
            Console.WriteLine(MenuMessages.SimpleAccountDetails, account.Id, account.Balance);
        }
    }
}
