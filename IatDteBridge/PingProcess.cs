using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IatDteBridge
{
    class PingProcess
    {
        // This method will be called when the thread is started. 
        public void DoPing()
        {
            int i = 0;
            while (!_shouldStop)
            {
                Console.WriteLine("Ping thread: working...");
                Thread.Sleep(60000);

                // send request ping to server DteBridge
                Connect conn = new Connect();
                string response = conn.ping();
                Console.WriteLine("respuesta = {0}. iteracion {1}", response,i);
                i++;


            }
            Console.WriteLine("Ping thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }

        private volatile bool _shouldStop;


        public void StartPing()
        {
            // Create the thread object. This does not start the thread.
       
            Thread pingThread = new Thread(DoPing);

            // Start the worker thread.
            pingThread.Start();
            Console.WriteLine("main thread: Starting Ping thread...");

            // Loop until worker thread activates. 
            while (!pingThread.IsAlive) ;


        }

        public void StopPing()
        {
 
            RequestStop();

            Console.WriteLine("main thread: Ping thread has terminated.");

        }

    }

   
}
