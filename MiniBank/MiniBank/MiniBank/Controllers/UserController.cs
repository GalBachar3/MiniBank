using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Conventions;
using MiniBank.Enums;
using MiniBank.Factories;
using MiniBank.Models;
using MiniBank.NhibernateTools;
using MiniBank.Resources;
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
            using (var transaction = Session.BeginTransaction())
            {
                var users = Session.Query<User>().ToList();

                transaction.Commit();
                return users;
            }
                

            
        }

        

        public void CreateNewUser(string name)
        {
            var user = new User {Name = name};

            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(user);
                transaction.Commit();
            }
        }

        public User GetUserById(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var user = Session.Load<User>(id);
                transaction.Commit();

                return user;
            }
        }

        public List<Account> GetAccountsOfUser(int id)
        {
            return GetUserById(id).Accounts.ToList();
        }

        public void AddAccount(int id,Accounts enumAccountType)
        {
            var account = new AccountFactory().GetAccount(enumAccountType);
            var user = GetUserById(id);
            
            account.Users.Add(user);
            user.Accounts.Add(account);
            
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(user);
                transaction.Commit();
            }

               
        }

        public void DeleteAllUsers()
        {
            var users = GetAllUsers();
            
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var user in users)
                {
                    Session.Delete(user);
                }

                transaction.Commit();
            }
        }
    }
}
