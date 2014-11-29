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


                // Instancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                docLectura = lec.lectura();
              //  Console.WriteLine("Folio = {0}", docLectura.Folio);
                if (docLectura != null)
                {

                    
                    //instancia Clase de tipo Impresora

                    // Ejecuta metodo que recibe archivo pdf y lo imprime


                    // instancia XML_admin
                    xmlAdmin xml = new xmlAdmin();

                    DateTime thisDay = DateTime.Now;
                    String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

                    // Ejecuta metodo de XML_admin que recibe objeto de la clase documento 
                    // que llena el xml lo firma, lo timbra y devuelve la factura xml lista
                    String TimbreElectr = xml.ted_to_xmlSii(docLectura, fch);

                    String docXmlSign = xml.doc_to_xmlSii(docLectura, TimbreElectr, fch);


                    // intancia objeto de la clase PDF_admin
                    // ejecutar metodo de PDF_admin que recibe objeto de la clase Documento
                    // que genera el archivo pdf
                    Pdf docpdf = new Pdf();

                    //                    docpdf.OpenPdf("sdffffsdfsdfsdfsd", docLectura);

                    docpdf.OpenPdf(TimbreElectr, docLectura);


                    Console.WriteLine(docXmlSign);


                    // instancia objeto de tipo Connect
                    Connect conn = new Connect();

                    // ejecuta metodo de Connect que recibe el xml y lo envía al Core
                    //conn.sendXml(docXmlSign, "Fact1.xml");

                }
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
