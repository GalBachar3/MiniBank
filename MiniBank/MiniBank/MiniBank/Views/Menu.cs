using System;
using MiniBank.Enums;
using MiniBank.Exceptions;
using MiniBank.MenuViewHelpers;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class Menu
    {
        public InputOutput Io { get; set; }
        public UserAction ActionExecuter { get; set; }

        public Menu()
        {
            Io = new InputOutput();
            ActionExecuter = new UserAction(Io);
        }

        public void Run()
        {
            Console.WriteLine(MenuMessages.CancelMessage);

            try
            {
                var choice = Io.GetIntInput("MenuOptions");

                while (true)
                {
                    var action = (Actions) choice;

                    ActionExecuter.ExecuteAction(action);

                    choice = Io.GetIntInput("MenuOptions");
                }
            }
            catch (CancelException)
            {
                return;
            }
        }
    }
}
