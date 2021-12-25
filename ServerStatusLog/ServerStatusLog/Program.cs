using System;
using System.IO;
using System.Threading;

namespace ServerStatusLog
{
    class Program
    {
        private static bool isRunning = false;
        const int TICKS_PER_MIN = 1;
        const int MS_PER_TICK = 900000; //15min

        static void Main(string[] args)
        {
            Console.Title = "log-server-status by haise";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

        }

        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {TICKS_PER_MIN} tick per 15 minutes.");
            DateTime _nextLoop = DateTime.UtcNow;

            while (isRunning)
            {
                while (_nextLoop < DateTime.UtcNow)
                {

                    //Console.WriteLine($"This is a whole loop");
                    SaveLastStatusCheck();

                    _nextLoop = _nextLoop.AddMilliseconds(MS_PER_TICK);

                    if (_nextLoop > DateTime.UtcNow)
                    {
                        Thread.Sleep(_nextLoop - DateTime.UtcNow);
                    }
                }
            }
        }

        public static void SaveLastStatusCheck()
        {
            string fileName = "StatusLog.txt";
            string pathString = @"\var\www\server-status-log";
            pathString = Path.Combine(pathString, fileName);

            //file doesn't exist, create file
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(pathString);
                //Write a line of text
                DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone

                sw.WriteLine("Last server uptime: " + serverTime);
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Status check logged-");
            }
        }
    }
}
