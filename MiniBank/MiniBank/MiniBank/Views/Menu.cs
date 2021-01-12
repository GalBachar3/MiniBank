using System;
using System.Reflection;
using System.Resources;
using MiniBank.Exceptions;
using MiniBank.Resources;

namespace MiniBank.Views
{
    public class Menu
    {
        public ResourceManager ResourceManager { get; set; }
        public string Input { get; set; }

        public Menu()
        {
            ResourceManager = new ResourceManager(
                "MiniBank.Resources.MenuMessages", Assembly.GetExecutingAssembly());
        }

        public int GetIntInput(string menuMessagesValueName)
        {
            Console.WriteLine(ResourceManager.GetString(menuMessagesValueName));

            return ReadIntInput();
        }

        private int ReadIntInput()
        {
            try
            {
                ReadInput();
                return int.Parse(Input);
            }
            catch (FormatException)
            {
                return GetIntInput("ErrorInputMessage");
            }
            catch (OverflowException)
            {
                return GetIntInput("ErrorInputMessage");
            }
        }

        public double GetSum()
        {
            Console.WriteLine(MenuMessages.ReadSumMessage);

            try
            {
                ReadInput();
                var sum = double.Parse(Input);

                if (sum >= 0)
                {
                    return sum;
                }

                Console.WriteLine(MenuMessages.InvalidSumMessage);

                return GetSum();
            }
            catch (OverflowException)
            {
                Console.WriteLine(MenuMessages.ErrorInputMessage);

                return GetSum();
            }
            catch (FormatException)
            {
                Console.WriteLine(MenuMessages.ErrorInputMessage);

                return GetSum();
            }
        }

        public string GetName()
        {
            Console.WriteLine(MenuMessages.ReadUserNameMessage);
            ReadInput();
            
            return Input;
        }

        public void ReadInput()
        {
            var input = Console.ReadLine();

            if (input == MenuMessages.CancelValue)
            {
                throw new CancelException();
            }

            Input = input;
        }
    }
}