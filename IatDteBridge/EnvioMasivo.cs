using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace IatDteBridge
{
    class EnvioMasivo
    {
        public void envioMasivo()
        {
                
                String paquete = String.Empty;
                
                // instancia fileadmin, para tener las herramientas para mover archivos
                fileAdmin fileAdm = new fileAdmin();

                // instancia XML_admin
                xmlPaquete xml = new xmlPaquete();

                List<int> tipos = new List<int>();
                // eliminar despues de la simulacion

                
                DateTime thisDay = DateTime.Now;
                String fchName = String.Format("{0:yyyyMMddTHHmmss}", thisDay);

                
                int i = 0;
                String firsRut = String.Empty;

                fileAdmin file = new fileAdmin();

                String fileName = file.nextFile(@"c:\IatFiles\file\xml", "*.xml");

                while (fileName != null)
                {


                    String docXmlSign = String.Empty;
                    using (StreamReader sr = new StreamReader(fileName,System.Text.Encoding.Default,true))
                    {
                        docXmlSign = sr.ReadToEnd();
                    }
            
                    String tip = fileName.Substring(15,2);

                    tipos.Add(Convert.ToInt32(tip));

                    if (i == 0) firsRut = fileName.Substring(4,10);
                    i++;
 
                    // Agrega el DTE timbrado al paquete
                    paquete = paquete + docXmlSign;

                    file.mvFile(fileName, "C:/IatFiles/file/xml", "C:/IatFiles/file/xml/enviado");
                    
                    //Sgte Documento
                    fileName = file.nextFile(@"c:\IatFiles\file\xml", "*.xml");

                    
                }
                


                // Firma POaquete    
                String envio = xml.creaEnvio(paquete,firsRut, "", tipos);

                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");
                String enviox509 = xml.firmarDocumento(envio, cert);

       
                enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;

                String fileNameEnvio = @"C:/IatFiles/file/xml/EnvioMasivo/EnvioMasivoPAck_" + firsRut +"_"+ fchName +".xml";
                using (System.IO.StreamWriter fileP = new System.IO.StreamWriter(fileNameEnvio, false, Encoding.GetEncoding("ISO-8859-1")))
                {
                    fileP.WriteLine(enviox509);
                }
                
                Console.WriteLine(enviox509);
                MessageBox.Show("Xml Masivo Creado!!");
                

            }

     }
    }

