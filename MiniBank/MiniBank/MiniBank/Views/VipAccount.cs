using System;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class VipAccount : Account
    {
        public override void PrintAccountDetails(Models.Account account)
        { 
            Console.WriteLine(MenuMessages.VipAccountDetails, account.Id, account.Balance);
        }
    }
}
