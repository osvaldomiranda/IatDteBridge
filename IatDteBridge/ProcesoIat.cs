using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Diagnostics;






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


                i++;

                String paquete = String.Empty;

                // instancia fileadmin, para tener las herramientas para mover archivos
                fileAdmin fileAdm = new fileAdmin();

                // inatancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                docLectura = lec.lectura("", true);
                // instancia XML_admin
                xmlPaquete xml = new xmlPaquete();

                List<int> tipos = new List<int>();

                DateTime thisDay = DateTime.Now;
                String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);
                String fchName = String.Format("{0:yyyyMMddTHHmmss}", thisDay);

                Log log = new Log();

                String firsRut = String.Empty;
                if (docLectura != null)
                {

                    log.addLog("Inicio proceso TipoDTE :"+ docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK" );
                    tipos.Add(docLectura.TipoDTE);


                    String TimbreElec = xml.ted_to_xmlSii(docLectura, fch);
                    String docXmlSign = xml.doc_to_xmlSii(docLectura, TimbreElec, fch);

                    // Guarda DTE xml
                    String DTE = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + docXmlSign;
                    String fileNameXML = @"C:/IatFiles/file/xml/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".xml";
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNameXML, false, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        file.WriteLine(docXmlSign);
                    }

                    //Generar PDF                   
                    Pdf docpdf = new Pdf();

                    String fileNamePDF = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf";
                    docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDF, " ");


                    docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDF, " ");

                    String fileNamePDFCed = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + "CEDIBLE.pdf";
  
                    if (docLectura.TipoDTE == 33 || docLectura.TipoDTE == 34)
                    {
                        docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDFCed, "CEDIBLE");
                    }

                    if (docLectura.TipoDTE == 52)
                    {
                        docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDFCed, "CEDIBLE CON SU FACTURA");
                    }

                  
                    
                    //Imprime pdf

                        ProcessStartInfo copiaOriginal = new ProcessStartInfo();
                        copiaOriginal.Verb = "print";
                        copiaOriginal.FileName = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + "CEDIBLE.pdf";
                        copiaOriginal.CreateNoWindow = true;
                        copiaOriginal.WindowStyle = ProcessWindowStyle.Hidden;

                        Process p = new Process();
                        p.StartInfo = copiaOriginal;
                        p.Start();

                        p.WaitForInputIdle();
                       
                        System.Threading.Thread.Sleep(10000);
                        if (false == p.CloseMainWindow())
                        {
                            p.Kill();
                        }
                       

                        ProcessStartInfo copiaOriginal2 = new ProcessStartInfo();
                        copiaOriginal2.Verb = "print";
                        copiaOriginal2.FileName = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf";
                        copiaOriginal2.CreateNoWindow = true;
                        copiaOriginal2.WindowStyle = ProcessWindowStyle.Hidden;

                        Process p2 = new Process();
                        p2.StartInfo = copiaOriginal2;
                        p2.Start();

                        p2.WaitForInputIdle();
                       
                        System.Threading.Thread.Sleep(10000);
                        if (false == p2.CloseMainWindow())
                        {
                             p2.Kill();
                        }
                         

                        ProcessStartInfo copiaCedible = new ProcessStartInfo();
                        copiaCedible.Verb = "print";
                        copiaCedible.FileName = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf";
                        copiaCedible.CreateNoWindow = true;
                        copiaCedible.WindowStyle = ProcessWindowStyle.Hidden;

                        Process p3 = new Process();
                        p3.StartInfo = copiaCedible;
                        p3.Start();

                        p3.WaitForInputIdle();
                        
                        System.Threading.Thread.Sleep(10000);
                        if (false == p.CloseMainWindow())
                        {
                             p.Kill();
                        }
                         


                    // Agrega el DTE timbrado al paquete
                    paquete = paquete + docXmlSign;

                    //Estrae el rut del emisor de la primera factura del paquete
                    if (i == 0) firsRut = docLectura.RUTEmisor;
                    i++;

                    
                    // Firma POaquete unitario   

                    String envio = xml.creaEnvio(paquete, docLectura.RUTEmisor, docLectura.RUTRecep, tipos);



                    X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");
                    String enviox509 = xml.firmarDocumento(envio, cert);


                    enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;


                    String fileNameEnvio = @"C:/IatFiles/file/xml/envioUnitario/EnvioUnit_" + docLectura.RUTEmisor + "_" + docLectura.Folio + "_" + fchName + ".xml";

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNameEnvio, false, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        file.WriteLine(enviox509);
                    }


                    // *************  Envía json a server
                    Connect conn = new Connect();

                    conn.sendInvoice(docLectura, @"DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf", @"DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + "CEDIBLE.pdf", @"EnvioUnit_" + docLectura.RUTEmisor + "_" + docLectura.Folio + "_" + fchName + ".xml", "N");
                    // *************  Envía json a server


                }
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
