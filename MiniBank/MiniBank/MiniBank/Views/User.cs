using System;
using System.Collections.Generic;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class User
    {
        public void PrintUserDetails(Models.User user)
        {
            Console.WriteLine(MenuMessages.UserDetails, user.Id, user.Name);
        }

        public void PrintAllUsers(List<Models.User> users)
        {
            foreach (var user in users)
            { 
                PrintUserDetails(user);
            }
        }
    }
}
