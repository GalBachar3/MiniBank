using System;
using System.Collections.Generic;
using MiniBank.Controllers;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.Factories;
using MiniBank.NhibernateTools;
using NHibernate;

namespace MiniBank.Client
{
    public class UserAction
    {
        public Dictionary<Actions, Action> Actions { get; set; }
        public ISession Session { get; set; }
        public User UserController { get; set; }
        public Account AccountController { get; set; }
        public Menu Menu { get; set; }

        public UserAction()
        {
            Actions = new Dictionary<Actions, Action>
            {
                {Enums.Actions.ListAllUsers, ListAllUsers},
                {Enums.Actions.ListAllAccountsByUser, ListAllAccountsByUser},
                {Enums.Actions.DepositAccount, DepositAccount},
                {Enums.Actions.WithdrawAccount, WithdrawAccount},
                {Enums.Actions.CreateNewUser, CreateNewUser},
                {Enums.Actions.CreateNewAccountForUser, CreateNewAccountForUser},
                {Enums.Actions.DeleteAllUsersAndAccounts, DeleteAllUsersAndAccounts},
                {Enums.Actions.Exit,Exit}
            };

            Session = FluentNHibernateHelper.OpenSession();
            UserController = new User(Session);
            Menu = new Menu();
        }

        public void ExecuteAction(Actions action)
        {
            Actions[action]();
        }

        public void ListAllUsers()
        {
            UserController.PrintUsersWithView();
        }

        public void ListAllAccountsByUser()
        {
            try
            {
                UserController.DefineUser(Menu.GetInputWithMessage("ReadUserIdMessage"));
                var accounts = UserController.GetAccountsOfUser();

                foreach (var account in accounts)
                {
                    SetAccountControllerByAccount(account);
                    AccountController.PrintAccountWithView();
                }
            }
            catch (ObjectNotFoundException)
            {
                Menu.PrintMessageFromMenuMessages("UserNotFoundMessage");
                ListAllAccountsByUser();
            }
            catch (CancelException)
            {
                return;
            }
        }

        public void SetAccountControllerByAccount(Models.Account account)
        {
            AccountController = new AccountControllerFactory(Session).GetAccountController(account.GetType());
            AccountController.Model = account;
        }

        public void DepositAccount()
        {
            try
            {
                SetAccountControllerForAccountFromDb();
                AccountController.Deposit(Menu.GetSum());
            }
            catch (CancelException)
            {
                return;
            }
        }

        public void WithdrawAccount()
        {
            try
            {
                SetAccountControllerForAccountFromDb();
                AccountController.Withdraw(Menu.GetSum());
            }
            catch (OverdraftException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CancelException)
            {
                return;
            }
        }

        public void SetAccountControllerForAccountFromDb()
        {
            try
            {
                var account = Session.Get<Models.Account>(Menu.GetInputWithMessage("ReadAccountIdMessage"));
                SetAccountControllerByAccount(account);
            }
            catch (NullReferenceException)
            {
                Menu.PrintMessageFromMenuMessages("AccountNotFoundMessage");
                SetAccountControllerForAccountFromDb();
            }
        }

        public void CreateNewUser()
        {
            try
            {
                UserController.CreateNewUser(Menu.GetName());
            }
            catch (CancelException)
            {
                return;
            }
        }

        public void CreateNewAccountForUser()
        {
            try
            {
                UserController.DefineUser(Menu.GetInputWithMessage("ReadUserIdMessage"));
                var enumAccountType = (Accounts) Menu.GetInputWithMessage("AccountTypeOptions");
                var account = new AccountFactory().GetAccount(enumAccountType);
                UserController.AddAccount(account);
            }
            catch (KeyNotFoundException)
            {
                Menu.PrintMessageFromMenuMessages("AccountTypeNotExist");
                CreateNewAccountForUser();
            }
            catch (ObjectNotFoundException)
            {
                Menu.PrintMessageFromMenuMessages("UserNotFoundMessage");
                CreateNewAccountForUser();
            }
            catch (CancelException)
            {
                return;
            }
        }

        public void DeleteAllUsersAndAccounts()
        {
            UserController.DeleteAllUsers();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
