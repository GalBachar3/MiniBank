using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace MiniBank.Controllers
{
    public class User
    {
        public Models.User Model { get; set; }
        public Views.User View { get; set; }
        public ISession Session { get; set; }

        public User(ISession session)
        {
            Session = session;
            View = new Views.User();
        }

        public List<Models.User> GetAllUsers()
        {
            return Session.Query<Models.User>().ToList();
        }

        public void PrintUsersWithView()
        {
            View.PrintAllUsers(GetAllUsers());
        }

        public void CreateNewUser(string name)
        {
            Model = new Models.User {Name = name};
            Session.Save(Model);
        }

        public void DefineUser(int id)
        {
            Model = Session.Load<Models.User>(id);
        }

        public List<Models.Account> GetAccountsOfUser()
        {
            return Model.Accounts.ToList();
        }

        public void AddAccount(Models.Account account)
        {
            account.Users.Add(Model);
            Model.Accounts.Add(account);
            var transaction = Session.BeginTransaction();
            
            Session.Update(Model);
            transaction.Commit();
        }

        public void DeleteAllUsers()
        {
            var users = Session.Query<Models.User>();
            var transaction = Session.BeginTransaction();
            
            foreach (var user in users)
            {
                Session.Delete(user);
            }
            
            transaction.Commit();
        }
    }
}
