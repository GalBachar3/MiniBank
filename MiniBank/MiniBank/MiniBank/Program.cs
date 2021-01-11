using System;
using MiniBank.Client;

namespace MiniBank
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new ApplicationRunner().Run();
            //Console.WriteLine(typeof(MiniBank.Models.User).FullName);
        }
    }
}