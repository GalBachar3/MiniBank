using System;
using System.Reflection;
using System.Resources;
using MiniBank.Exceptions;
using MiniBank.Resources;

namespace MiniBank.Client
{
    public class Menu
    {
        public ResourceManager ResourceManager { get; set; }

        public Menu()
        {
            ResourceManager = new ResourceManager("MiniBank.Resources.MenuMessages", Assembly.GetExecutingAssembly());
        }

        public int GetInputWithMessage(string valueName)
        {
            PrintMessageFromMenuMessages(valueName);

            return ReadIntInput();
        }

        public int ReadIntInput()
        {
            try
            {
                return int.Parse(ReadInput());
            }
            catch (FormatException)
            {
                return GetInputWithMessage("ErrorInputMessage");
            }
            catch (OverflowException)
            {
                return GetInputWithMessage("ErrorInputMessage");
            }
        }

        public double GetSum()
        {
            Console.WriteLine(MenuMessages.ReadSumMessage);

            try
            {
                var sum = double.Parse(ReadInput());

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

            return ReadInput();
        }

        public void PrintMessageFromMenuMessages(string valueName)
        {
            Console.WriteLine(ResourceManager.GetString(valueName));
        }

        public string ReadInput()
        {
            var input = Console.ReadLine();

            if (input != MenuMessages.CancelValue)
            {
                return input;
            }

            throw new CancelException();
        }
    }
}