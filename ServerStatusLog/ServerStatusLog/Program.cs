using System;
using System.Threading;

namespace ServerStatusLog
{
    class Program
    {
        private static bool isRunning = false;
        const int TICKS_PER_SEC = 30;
        const int MS_PER_TICK = 30;

        static void Main(string[] args)
        {
            Console.Title = "LOG SERVER STATUS by Haise";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.UtcNow;

            while (isRunning)
            {
                while (_nextLoop < DateTime.UtcNow)
                {

                    Console.WriteLine($"This is a whole loop");

                    _nextLoop = _nextLoop.AddMilliseconds(MS_PER_TICK);

                    if (_nextLoop > DateTime.UtcNow)
                    {
                        Thread.Sleep(_nextLoop - DateTime.UtcNow);
                    }
                }
            }
        }
    }
}
