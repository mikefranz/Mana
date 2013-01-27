using System;
using System.Threading;
using ServerSupport.Presentation;

namespace ServerSupport.Workflow
{
    public class StartEngine
    {
        private Thread _engineThread;
        private Engine _engine;

        public void Run()
        {
            StartEngineThread();

            while (!Engine.IsShutDown())
            {
                ConsoleManager.DisplayUserPrompt();
            }

            EnsureEngineIsShutDown();
        }

        private void StartEngineThread()
        {
            Console.WriteLine("Starting Engine");

            _engine = new Engine();
            _engineThread = new Thread(_engine.StartEngine);
            _engineThread.Start();
        }

        public void EnsureEngineIsShutDown()
        {
            if (Engine.IsShutDown())
            {
                return;
            }

            Console.WriteLine("Shutting down engine");

            Engine.ShutDown();

            Thread.Sleep(1000);

            if (Engine.IsShutDown()) 
                return;

            ForceEngineShutdown();
            
        }

        private void ForceEngineShutdown()
        {
            Console.WriteLine("Aborting engine thread");

            if (_engineThread != null)
            {
                _engineThread.Abort();
            }
        }
    }
}