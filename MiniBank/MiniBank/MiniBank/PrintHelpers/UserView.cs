using System;
using System.Collections.Generic;
using FluentNHibernate.Conventions;
using MiniBank.Models;
using MiniBank.Resources;

namespace MiniBank.PrintHelpers
{
    public class UserView
    {
        public void PrintUserDetails(Models.User user)
        {
            Console.WriteLine(MenuMessages.UserDetails, user.Id, user.Name);
        }

        public void PrintAllUsers(List<User> users)
        {
            if (!users.IsEmpty())
            {
                foreach (var user in users)
                {
                    PrintUserDetails(user);
                }
            }
            else
            {
                Console.WriteLine(MenuMessages.NoUsersExistMessage);
            }
        }
    }
}
