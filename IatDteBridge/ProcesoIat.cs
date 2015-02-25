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
            int j = 0;
            while (!_shouldStop)
            {
                Console.WriteLine("ProcessIat thread: working...");

                Thread.Sleep(3000);


                i++;

                String paquete = String.Empty;
                String dirCurrentFile = String.Empty;

                // instancia fileadmin, para tener las herramientas para mover archivos
                fileAdmin fileAdm = new fileAdmin();

                // inatancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                j++;
                
                if (j == 4)
                {
                    docLectura = lec.lectura("",false, @"C:\IatFiles\file");
                    dirCurrentFile = @"C:\IatFiles\file";
                }
                else
                {
                    docLectura = lec.lectura("",false, @"C:\IatFiles\cajas\caj" + j + @"\");
                    dirCurrentFile = @"C:\IatFiles\cajas\caj" + j + @"\";
                }
                
      
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
                    // Proceso de ReImpresión
                    // ir a directorio procesados y buscar el archivo docLectura.filename 
                    if (System.IO.File.Exists(@"C:\IatFiles\fileprocess\"+docLectura.fileName)) // si ya existe, reimprimir
                    {
                        String fileNamePDFRePrint = @"C:/IatFiles/file/pdf/PRINT_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + ".pdf";

                        if(System.IO.File.Exists(fileNamePDFRePrint))
                        {
                            FuncionesComunes fc = new FuncionesComunes();
                            String impresora = String.Empty;
                            switch (j)
                            {
                                case 1: impresora = fc.GetDefaultPrinter();
                                    break;
                                case 2: impresora = fc.GetPrinter(1);
                                    break;
                                case 3: impresora = fc.GetPrinter(2);
                                    break;
                                case 4: impresora = fc.GetPrinter(3);
                                    break;
                            }

                            fc.printPdf(fileNamePDFRePrint, impresora);
                        }
                        fileAdm.mvFile(docLectura.fileName, dirCurrentFile, @"C:\IatFiles\fileprocess\");
                    }
                    else // si no procesar
                    {
                        fileAdm.mvFile(docLectura.fileName, dirCurrentFile, @"C:\IatFiles\fileprocess\");

                        log.addLog("Inicio proceso TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");
                        tipos.Add(docLectura.TipoDTE);

                        String TimbreElec = xml.ted_to_xmlSii(docLectura, fch);
                        log.addLog("Crea Timbre TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        String docXmlSign = xml.doc_to_xmlSii(docLectura, TimbreElec, fch);
                        log.addLog("Crea XML Envio TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

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
                        log.addLog("Crea PDF Trib TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        String fileNamePDFCed = @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + "CEDIBLE.pdf";

                        if (docLectura.TipoDTE == 33 || docLectura.TipoDTE == 34)
                        {
                            docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDFCed, "CEDIBLE");
                        }

                        if (docLectura.TipoDTE == 52)
                        {
                            docpdf.OpenPdf(TimbreElec, docLectura, fileNamePDFCed, "CEDIBLE CON SU FACTURA");
                        }
                        log.addLog("Crea PDF C TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        // para otro tipo de impresion
                        // FuncionesComunes f = new FuncionesComunes();
                        // f.PrintDocument(@"CutePDF Writer", @"C:/IatFiles/file/pdf/DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf");

                        //Imprime pdf

                        String fileNamePDFPrint = @"C:/IatFiles/file/pdf/PRINT_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + ".pdf";
                        docpdf.OpenPdfPrint(TimbreElec, docLectura, fileNamePDFPrint);
                        log.addLog("Crea PDF PRINT TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        FuncionesComunes fc = new FuncionesComunes();
                        String impresora = String.Empty;
                        switch (j)
                        {
                            case 1: impresora = fc.GetDefaultPrinter();
                                break;
                            case 2: impresora = fc.GetPrinter(1);
                                break;
                            case 3: impresora = fc.GetPrinter(2);
                                break;
                            case 4: impresora = fc.GetPrinter(3);
                                break;
                        }

                        fc.printPdf(fileNamePDFPrint, impresora);
                        log.addLog("IMPRIME TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        // Agrega el DTE timbrado al paquete
                        paquete = paquete + docXmlSign;

                        //Estrae el rut del emisor de la primera factura del paquete
                        if (i == 0) firsRut = docLectura.RUTEmisor;
                        i++;


                        // Firma POaquete unitario   

                        String envio = xml.creaEnvio(paquete, docLectura.RUTEmisor, docLectura.RUTRecep, tipos, docLectura.RutEnvia, docLectura.FchResol);

                        X509Certificate2 cert = FuncionesComunes.obtenerCertificado(docLectura.NombreCertificado);
                        String enviox509 = xml.firmarDocumento(envio, cert);
                        log.addLog("FIRMA ENVIO TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");

                        enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;


                        String fileNameEnvio = @"C:/IatFiles/file/xml/enviounitario/EnvioUnit_" + docLectura.RUTEmisor + "_" + docLectura.Folio + "_" + fchName + ".xml";

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNameEnvio, false, Encoding.GetEncoding("ISO-8859-1")))
                        {
                            file.WriteLine(enviox509);
                        }


                        // *************  Envía json a server
                        Connect conn = new Connect();
                        String trib = @"DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + ".pdf";
                        String envU = @"EnvioUnit_" + docLectura.RUTEmisor + "_" + docLectura.Folio + "_" + fchName + ".xml";
                        String ced = String.Empty;
                        if (docLectura.TipoDTE != 61)
                        {
                            ced = @"DTE_" + docLectura.RUTEmisor + "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + fchName + "CEDIBLE.pdf";
                        }

                        //conn.sendInvoice(docLectura, trib, ced, envU, "S");
                        // *************  Envía json a server
                        log.addLog("Envia CORE TipoDTE :" + docLectura.TipoDTE + " Folio :" + docLectura.Folio, "OK");
                    }
                }
                if (j == 4) { j = 0; } 
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
