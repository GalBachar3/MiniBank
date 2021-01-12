using System;
using System.Collections.Generic;
using MiniBank.Controllers;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.Models;
using MiniBank.Resources;
using NHibernate;

namespace MiniBank.Views
{
    public class UserAction
    {
        private Dictionary<Actions, Action> UserActions { get; set; }
        public UserController UserController { get; set; }
        public AccountController AccountController { get; set; }
        public AccountView AccountView { get; set; }
        public UserView UserView { get; set; }
        public Menu Menu { get; set; }

        public UserAction(Menu menu)
        {
            UserActions = new Dictionary<Actions, Action>
            {
                {Actions.ListAllUsers, PrintAllUsers},
                {Actions.ListAllAccountsByUser, PrintAccountsOfUser},
                {Actions.DepositAccount, DepositAccount},
                {Actions.WithdrawAccount, WithdrawAccount},
                {Actions.CreateNewUser, CreateNewUser},
                {Actions.CreateNewAccountForUser, CreateNewAccount},
                {Actions.DeleteAllUsersAndAccounts, DeleteAllUsersAndAccounts},
                {Actions.Exit, ()=>Environment.Exit(0)}
            };

            Menu = menu;
            UserController = new UserController();
            UserView = new UserView();
            AccountController = new AccountController();
            AccountView = new AccountView();
        }

        public void ExecuteAction(Actions action)
        {
            try
            {
                UserActions[action]();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine(MenuMessages.ActionNotExistMessage);
            }
        }

        private void PrintAllUsers()
        {
            var users = UserController.GetAllUsers();
            
            UserView.PrintAllUsers(users);
        }

        private void PrintAccountsOfUser()
        {
            var validInput = false;
            
            while (!validInput)
            {
                try
                {
                    var userId = Menu.GetIntInput("ReadUserIdMessage");
                    var accounts = UserController.GetAccountsOfUser(userId);

                    AccountView.PrintAllAccounts(accounts);
                    validInput = true;
                }
                catch (ObjectNotFoundException)
                {
                    Console.WriteLine(MenuMessages.UserNotFoundMessage);
                }
                catch (CancelException)
                {
                    return;
                }
            }
            
        }

        private void DepositAccount()
        {
            try
            {
                AccountController.Deposit(GetAccount(), Menu.GetSum());
                Console.WriteLine(MenuMessages.SuccessMessage);
            }
            catch (CancelException)
            {
                return;
            }
        }

        private void WithdrawAccount()
        {
            try
            {
                AccountController.Withdraw(GetAccount(), Menu.GetSum());
                Console.WriteLine(MenuMessages.SuccessMessage);
            }
            catch (CancelException)
            {
                return;
            }
            catch (OverdraftException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private Account GetAccount()
        {
            var validInput = false;
            Account account = null;

            while (!validInput)
            {
                try
                {
                    account = AccountController.GetAccountById(Menu.GetIntInput("ReadAccountIdMessage"));
                    validInput = true;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine(MenuMessages.AccountNotFoundMessage);
                }
            }

            return account;
        }

        private void CreateNewUser()
        {
            try
            {
                UserController.CreateNewUser(Menu.GetName());
                Console.WriteLine(MenuMessages.SuccessMessage);
            }
            catch (CancelException)
            {
                return;
            }
        }

        private void CreateNewAccount()
        {
            var validInput = false;

            while (!validInput)
            {
                try
                {
                    var userId = Menu.GetIntInput("ReadUserIdMessage");
                    var enumAccountType = (Accounts)Menu
                        .GetIntInput("AccountTypeOptions");

                    UserController.AddAccount(userId, enumAccountType);
                    Console.WriteLine(MenuMessages.SuccessMessage);
                    validInput = true;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine(MenuMessages.AccountTypeNotExist);
                }
                catch (ObjectNotFoundException)
                {
                    Console.WriteLine(MenuMessages.UserNotFoundMessage);
                }
                catch (CancelException)
                {
                    return;
                }
            }
            
        }

        private void DeleteAllUsersAndAccounts()
        {
            UserController.DeleteAllUsers();
            Console.WriteLine(MenuMessages.SuccessMessage);
        }
    }
}
