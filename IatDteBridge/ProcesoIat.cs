using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IatDteBridge
{
    class ProcesoIat
    {

        public void DoProcessIat()
        {
            int i = 0;
            while (!_shouldStop)
            {
                Console.WriteLine("ProcessIat thread: working...");
                Thread.Sleep(5000);

                /*
                 * Implementar el siguiente ciclo en un Thread con un ciclo iterativo
                 */

                // Instancia txt_admin

                // Ejecuta metodo de txt_admin que llena y obtienen Clase Documento

                // intancia objeto de la clase PDF_admin

                // ejecutar metodo de PDF_admin que recibe objeto de la clase Documento
                // que genera el archivo pdf

                //instancia Clase de tipo Impresora

                // Ejecuta metodo que recibe archivo pdf y lo imprime


                // instancia XML_admin

                // Ejecuta metodo de XML_admin que recibe objeto de la clase documento 
                // que llena el xml lo firma, lo timbra y devuelve la factura xml lista


                // instancia objeto de tipo Connect

                // ejecuta metodo de Connect que recibe el xml y lo envía al Core


                // Continuar con siguiente documento


                Console.WriteLine("Iteracion = {0}", i);
                i++;


            }
            Console.WriteLine("ProcessIat thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }

        private volatile bool _shouldStop;


        public void StartProcessIat()
        {
            // Create the thread object. This does not start the thread.

            Thread processIatThread = new Thread(DoProcessIat);

            // Start the worker thread.
            processIatThread.Start();
            Console.WriteLine("main thread: Starting ProcessIat thread...");

            // Loop until worker thread activates. 
            while (!processIatThread.IsAlive) ;


        }

        public void StopProcessIat()
        {

            RequestStop();

            Console.WriteLine("main thread: ProcessIat thread has terminated.");

        }




   



    }
}
