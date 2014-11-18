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

                // inatancia txt_reader
                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();

                // Ejecuta metodo de txt_reader que llena y obtienen Clase Documento
                docLectura = lec.lectura();
                // instancia XML_admin
                xmlPaquete xml = new xmlPaquete();

                List<int> tipos = new List<int>();

                while (docLectura != null)
                {


                    tipos.Add(docLectura.TipoDTE);

                    String docXmlSign = xml.doc_to_xmlSii(docLectura);

                    // Agrega el DTE timbrado al paquete
                    paquete = paquete + docXmlSign;

                    //Sgte Documento
                    docLectura = lec.lectura();
                    
                }
                


                // Firma POaquete    
                String envio = xml.creaEnvio(paquete,"77398570-7", "", tipos);


                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");
                String enviox509 = xml.firmarDocumento(envio, cert);

       
                enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;


                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/IatFiles/file/xml/EnvioPAck_" + "santiago"  + ".xml", false, Encoding.GetEncoding("ISO-8859-1")))


            {
                file.WriteLine(enviox509);
            }
                Console.WriteLine(enviox509);
                

            }

     }

    
}
