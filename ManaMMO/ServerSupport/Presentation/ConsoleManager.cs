using System;
using System.Collections.Generic;
using ServerSupport.UserCommands;
using ServerSupport.Workflow;

namespace ServerSupport.Presentation
{
    public static class ConsoleManager
    {
        private static bool _displayEngineMessages = true;
        private static bool _waitForUserInput = false;

        public static void EngineWriteLine(string message)
        {
            if (_displayEngineMessages)
            {
                Console.WriteLine(message);
            }
        }

        public static void PauseEngineMessages()
        {
            _displayEngineMessages = false;
        }

        public static void ResumeEngineMessages()
        {
            _displayEngineMessages = true;
        }

        public static void DisplayUserPrompt()
        {
            PauseEngineMessages();

            DisplayUserPromptHeader();
            WaitForUserCommand();
        }

        private static void DisplayUserPromptHeader()
        {
            Console.Clear();
            Console.WriteLine("************************************************");
            Console.WriteLine("              Server Command Prompt");
            Console.WriteLine("************************************************");
            
            DisplayLineStart();
        }

        private static void DisplayLineStart()
        {
            Console.Write(">");
        }

        private static void WaitForUserCommand()
        {
            _waitForUserInput = true;

            while (_waitForUserInput && !Engine.IsShutDown())
            {
                var command = Console.ReadLine();
                if (command == null)
                {
                     Console.WriteLine("** Command could not be read **");
                    continue;
                }

                CommandManager.ProcessUserCommand(command.ToUpper());
            }
        }

        public static void DisplayUserCommandErrorMessage(string message)
        {
            Console.WriteLine("*** {0} ***", message);

            DisplayLineStart();
        }

        public static void SwitchConsoleToEngineMessages()
        {
            Console.Clear();
            _displayEngineMessages = true;
            _waitForUserInput = false;

            WaitForKeyPress();
        }

        private static void WaitForKeyPress()
        {
            Console.ReadKey();

            DisplayUserPrompt();
        }


    }
}