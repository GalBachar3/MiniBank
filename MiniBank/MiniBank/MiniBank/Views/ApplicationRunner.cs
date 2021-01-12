using System;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class ApplicationRunner
    {
        public Menu Menu { get; set; }
        public UserAction ActionExecuter { get; set; }

        public ApplicationRunner()
        {
            Menu = new Menu();
            ActionExecuter = new UserAction(Menu);
        }

        public void Run()
        {
            Console.WriteLine(MenuMessages.CancelMessage);
            
            try
            {
                var choice = Menu.GetIntInput("MenuOptions");

                while (true)
                {
                    var action = (Actions) choice;

                        ActionExecuter.ExecuteAction(action);
                    
                    choice = Menu.GetIntInput("MenuOptions");
                }
            }
            catch (CancelException)
            {
                return;
            }
        }
    }
}
