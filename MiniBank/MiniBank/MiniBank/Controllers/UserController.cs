using System.Collections.Generic;
using System.Linq;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.Factories;
using MiniBank.Models;
using MiniBank.NhibernateTools;
using NHibernate;

namespace MiniBank.Controllers
{
    public class UserController
    {
        private ISession Session { get; }

        public UserController()
        {
            Session = FluentNHibernateHelper.Session;
        }

        public List<User> GetAllUsers()
        {
            var users = Session.Query<User>().ToList();

            return users;
        }

        public void CreateNewUser(string name)
        {
            var user = new User {Name = name};

            using (var transaction = new TransactionHelper { Transaction = Session.BeginTransaction() })
            {
                Session.Save(user);
            }
        }

        public User GetUserById(int id)
        {
            var user = Session.Get<User>(id);

            if (user == null)
            {
                throw new EntityNotFoundException("the entity is not exist in the db");
            }

            return user;
        }

        public List<Account> GetAccountsOfUser(int id)
        {
            return GetUserById(id).Accounts.ToList();
        }

        public void AddAccount(int id, Accounts enumAccountType)
        {
            var account = new AccountFactory().GetAccount(enumAccountType);
            var user = GetUserById(id);

            account.Users.Add(user);
            user.Accounts.Add(account);

            using (var transaction = new TransactionHelper { Transaction = Session.BeginTransaction() })
            {
                Session.Update(user);
            }
        }

        public void DeleteAllUsers()
        {
            var users = GetAllUsers();

            using (var transaction = new TransactionHelper { Transaction = Session.BeginTransaction() })
            {
                foreach (var user in users)
                {
                    Session.Delete(user);
                }
            }
        }
    }
}
