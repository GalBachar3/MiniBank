using System;
using System.Collections.Generic;
using MiniBank.Models;
using MiniBank.NhibernateTools;
using MiniBank.Views;

namespace MiniBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FluentNHibernateHelper.OpenSession();
            using (var session = FluentNHibernateHelper.Session)
            {
                new ApplicationRunner().Run();
                session.Close();
            }
        }
    }
}