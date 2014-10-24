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
                Thread.Sleep(10000);


                // Instancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                docLectura = lec.lectura();
              //  Console.WriteLine("Folio = {0}", docLectura.Folio);
                if (docLectura != null)
                {

                    // intancia objeto de la clase PDF_admin




                    // ejecutar metodo de PDF_admin que recibe objeto de la clase Documento
                    // que genera el archivo pdf
                    Pdf docpdf = new Pdf();

<<<<<<< HEAD
//                    docpdf.OpenPdf("sdffffsdfsdfsdfsd", docLectura);
=======
                    docpdf.OpenPdf("<TED version='1.0'><DD><RE>97975000-5</RE><TD>33</TD><F>1</F><FE>2014-05-28</FE><RR>77777777-7</RR><RSR>Pc Factory</RSR><MNT>119000</MNT><IT1>Parlantes Multimedia 180W.</IT1><CAF version='1.0'><DA><RE>10207640-0</RE><RS>MAURICIO JIMENEZ</RS><TD>33</TD><RNG><D>1</D><H>50</H></RNG><FA>2014-05-26</FA><RSAPK><M>uJ+OZ5qO9diB/c9MoZuwPs9ltKGAS1IbEymF7W3X3ZTq6ElExVkrlfp7uDoGR0DiBndor6Vyc+X4MRbsk6KC9w==</M><E>Aw==</E></RSAPK><IDK>100</IDK></DA><FRMA algoritmo='SHA1withRSA'>SGKR9otZoN8/5sIaKFJIbo08Jbt95UBh76fcFv21lfNsgauAcyzUF0FARrMyphMagJ0zzChJzU7R/Q0mrDvYvQ==</FRMA></CAF><TSTED>2014-05-28T09:33:20</TSTED></DD><FRMT algoritmo='SHA1withRSA'>GK7FRnNjgHLyRspdygg2WudvqqJ+OQchn8k/6TUrndBBNHsFHInEN34+KLTy\nFgRG/bmDIjclV4VTlgs3TIg/7A==\n</FRMT></TED>", docLectura);
>>>>>>> Mauricio

                    //instancia Clase de tipo Impresora

                    // Ejecuta metodo que recibe archivo pdf y lo imprime


                    // instancia XML_admin
                    xmlAdmin xml = new xmlAdmin();

                    // Ejecuta metodo de XML_admin que recibe objeto de la clase documento 
                    // que llena el xml lo firma, lo timbra y devuelve la factura xml lista
                    String docXmlSign = xml.doc_to_xmlSii(docLectura);

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
