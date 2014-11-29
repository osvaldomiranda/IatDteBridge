using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace IatDteBridge
{
    class ProcesoPaquete
    {
        public void procesoPaquete()
        {
                
                String paquete = String.Empty;
                
                // instancia fileadmin, para tener las herramientas para mover archivos
                fileAdmin fileAdm = new fileAdmin();

                // inatancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                docLectura = lec.lectura();
                // instancia XML_admin
                xmlPaquete xml = new xmlPaquete();

                List<int> tipos = new List<int>();

                DateTime thisDay = DateTime.Now;
                String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);
                String fchName = String.Format("{0:yyyyMMddTHHmmss}", thisDay);

                int i = 0;
                String firsRut = String.Empty;
                while (docLectura != null)
                {

                    tipos.Add(docLectura.TipoDTE);

                    String TimbreElec = xml.ted_to_xmlSii(docLectura,fch);
                    String docXmlSign = xml.doc_to_xmlSii(docLectura,TimbreElec,fch);
                    
                    // Guarda DTE xml
                    String DTE = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + docXmlSign;
                    String fileNameXML = @"C:/IatFiles/file/xml/DTE_"+docLectura.RUTEmisor+"_"+ docLectura.TipoDTE+"_"+ docLectura.Folio  +"_"+docLectura.FchEmis + ".xml";
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNameXML, false, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        file.WriteLine(docXmlSign);
                    }


                    //Generar PDF                   
                    Pdf docpdf = new Pdf();

                    String fileNamePDF = @"C:/IatFiles/file/pdf/DTE_" +docLectura.RUTEmisor+ "_" + docLectura.TipoDTE + "_" + docLectura.Folio + "_" + docLectura.FchEmis + ".pdf";
                    docpdf.OpenPdf(TimbreElec, docLectura,fileNamePDF);

                  

                    // Agrega el DTE timbrado al paquete
                    paquete = paquete + docXmlSign;

                    //Estrae el rut del emisor de la primera factura del paquete
                    if (i == 0) firsRut = docLectura.RUTEmisor;
                    i++;

                    //Sgte Documento
                    docLectura = lec.lectura();

                    // Verifica que el siguiente documento sea del mismo emisor
                    while (docLectura.RUTEmisor != firsRut)
                    {
                        // si no tiene el mismo rut
                        // lo saca del directorio
                        fileAdm.mvFile(docLectura.fileName, @"c:\IatFiles\file\", @"c:\IatFiles\file\noincluidos\");
                       
                        //Sgte Documento
                        docLectura = lec.lectura();
                    }
                }
                


                // Firma POaquete    
                String envio = xml.creaEnvio(paquete,firsRut, "", tipos);


                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");
                String enviox509 = xml.firmarDocumento(envio, cert);

       
                enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;

                String fileNameEnvio = @"C:/IatFiles/file/xml/EnvioPAck_" + firsRut +"_"+ fchName +".xml";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNameEnvio, false, Encoding.GetEncoding("ISO-8859-1")))
                {
                    file.WriteLine(enviox509);
                }
                
                Console.WriteLine(enviox509);
                

            }

     }

    
}
