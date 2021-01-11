using System.Collections.Generic;
using MiniBank.Enums;
using MiniBank.Exceptions;

namespace MiniBank.Client
{
    public class ApplicationRunner
    {
        public Menu Menu { get; set; }
        public UserAction Action { get; set; }
        

        public ApplicationRunner()
        {
            Menu = new Menu();
            Action = new UserAction();
        }

        public void Run()
        {
            try
            {
                var choice = Menu.GetInputWithMessage("MenuOptions");

                while (true)
                {
                    try
                    {
                        var action = (Actions) choice;
                        
                        Action.ExecuteAction(action);
                    }
                    catch (KeyNotFoundException)
                    {
                        Menu.PrintMessageFromMenuMessages("ActionNotExistMessage");
                    }

                    choice = Menu.GetInputWithMessage("MenuOptions");
                }
            }
            catch (CancelException)
            {
                return;
            }


        }
    }
}
