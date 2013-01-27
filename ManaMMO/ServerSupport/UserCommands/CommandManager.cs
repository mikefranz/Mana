using System;
using System.Collections.Generic;
using ServerSupport.Presentation;
using ServerSupport.Workflow;

namespace ServerSupport.UserCommands
{
    public class CommandManager
    {
        public static void ProcessUserCommand(string command)
        {
            if (command == null)
            {
                return;
            }

            var commands = command.Split(' ');
            var commandName = commands[0];

            var commandArgs = new string[0];
            if (commands.Length > 1)
            {
                commandArgs = new string[commands.Length - 1];
                Array.Copy(commands, 1, commandArgs, 0, commands.Length - 1);
            }

            switch (commandName)
            {
                case "SHOW":
                    ProcessShowCommand(commandArgs);
                    break;
                case "SHUTDOWN":
                    ProcessShutdownCommand();
                    break;
                default:
                    ConsoleManager.DisplayUserCommandErrorMessage("Command not recognized");
                    break;
            }
        }

        private static void ProcessShutdownCommand()
        {
            Engine.ShutDown();
            Console.Clear();
            ConsoleManager.ResumeEngineMessages();
        }

        private static void ProcessShowCommand(IList<string> commandArgs)
        {
            if (commandArgs.Count == 0)
            {
                ConsoleManager.DisplayUserCommandErrorMessage("What did you want to show? (Engine)");
                return;
            }

            switch (commandArgs[0])
            {
                case "ENGINE":
                    ConsoleManager.SwitchConsoleToEngineMessages();
                    break;
                default:
                    ConsoleManager.DisplayUserCommandErrorMessage("The object you are trying to show was not recognized.");
                    break;
            }
        }
    }
}