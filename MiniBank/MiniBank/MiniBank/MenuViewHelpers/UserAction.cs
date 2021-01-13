using System;
using System.Collections.Generic;
using MiniBank.Controllers;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.Models;
using MiniBank.PrintHelpers;
using MiniBank.Resources;

namespace MiniBank.MenuViewHelpers
{
    public class UserAction
    {
        private Dictionary<Actions, Action> UserActions { get; }
        public UserController UserController { get; set; }
        public AccountController AccountController { get; set; }
        public InputOutput Io { get; set; }

        public UserAction(InputOutput menu)
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
                {Actions.Exit, () => Environment.Exit(0)}
            };

            Io = menu;
            UserController = new UserController();
            AccountController = new AccountController();
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
            catch (CancelException)
            {
                return;
            }
        }

        private void PrintAllUsers()
        {
            var users = UserController.GetAllUsers();

            new UserView().PrintAllUsers(users);
        }

        private void PrintAccountsOfUser()
        {
            var validInput = false;

            while (!validInput)
            {
                try
                {
                    var userId = Io.GetIntInput("ReadUserIdMessage");
                    var accounts = UserController.GetAccountsOfUser(userId);

                    new AccountView().PrintAllAccounts(accounts);
                    validInput = true;
                }
                catch (EntityNotFoundException)
                {
                    Console.WriteLine(MenuMessages.UserNotFoundMessage);
                }
            }
        }

        private void DepositAccount()
        {
            AccountController.Deposit(GetAccount(), Io.GetSum());
            Console.WriteLine(MenuMessages.SuccessMessage);
        }

        private void WithdrawAccount()
        {
            try
            {
                AccountController.Withdraw(GetAccount(), Io.GetSum());
                Console.WriteLine(MenuMessages.SuccessMessage);
            }
            catch (OverdraftException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private Account GetAccount()
        {
            Account account = null;
            
            while (account == null)
            {
                try
                {
                    account = AccountController
                        .GetAccountById(Io.GetIntInput("ReadAccountIdMessage"));
                }
                catch (EntityNotFoundException)
                {
                    Console.WriteLine(MenuMessages.AccountNotFoundMessage);
                }
            }

            return account;
        }

        private void CreateNewUser()
        {
            UserController.CreateNewUser(Io.GetName());
            Console.WriteLine(MenuMessages.SuccessMessage);
        }

        private void CreateNewAccount()
        {
            var validInput = false;

            while (!validInput)
            {
                try
                {
                    var userId = Io.GetIntInput("ReadUserIdMessage");
                    var enumAccountType = (Accounts) Io.GetIntInput("AccountTypeOptions");

                    UserController.AddAccount(userId, enumAccountType);
                    Console.WriteLine(MenuMessages.SuccessMessage);
                    validInput = true;
                }
                catch (AccountTypeNotExistException)
                {
                    Console.WriteLine(MenuMessages.AccountTypeNotExist);
                }
                catch (EntityNotFoundException)
                {
                    Console.WriteLine(MenuMessages.UserNotFoundMessage);
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
