using System;
using System.Threading;
using ServerSupport.Presentation;

namespace ServerSupport.Workflow
{
    public class Engine
    {
        public static string MessageTest = "";
        
        private static bool _enableShutdown = false;
        private static bool _isShutDown = false;

        public Engine()
        {
            
        }

        ~Engine()
        {
            ConsoleManager.EngineWriteLine("Engine shutting down");
        }

        public void StartEngine()
        {
            ConsoleManager.EngineWriteLine("Engine Started");

            DoSomething();
        }

        public void DoSomething()
        {
            while (!_enableShutdown)
            {
                ConsoleManager.EngineWriteLine("Hello");

                Thread.Sleep(1000);
            }

            _isShutDown = true;
        }

        public static void ShutDown()
        {
            _enableShutdown = true;
        }

        public static bool IsShutDown()
        {
            return _isShutDown;
        }
    }
}