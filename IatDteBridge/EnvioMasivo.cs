using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Threading;

namespace IatDteBridge
{
    class EnvioMasivo
    {
        public void envioMasivo()
        {
                
                
                // instancia fileadmin, para tener las herramientas para mover archivos
                fileAdmin fileAdm = new fileAdmin();

                TxtReader lec = new TxtReader();

                Documento docLectura = new Documento();
                
                fileAdmin file = new fileAdmin();

                Connect con = new Connect();


                String filePdft = file.nextFile(@"c:\IatFiles\pdf", "*.pdf");


                String filexml;
                String filexmlap;
                String filePdfc;
                String fileJson;

                while (filePdft != null)
                {
                    int largo = filePdft.Length - 4; 
                    filePdfc = filePdft.Substring(0, largo) +"CEDIBLE.pdf";
                    filexmlap="EnvioUnit" + filePdft.Substring(3, 12)+filePdft.Substring(18, 5);
                    filexml = file.fileAprox(filexmlap,@"c:\IatFiles\enviounitario","*.xml");
                    fileJson =@"c:\IatFiles\fileProcess\Fac_"+ filePdft.Substring(18, 4)+".json";

                    docLectura = lec.lectura(fileJson,false);

                    con.sendInvoice(docLectura,filePdft,filePdfc,filexml,"N");

                    
     
                    file.mvFile(filePdfc, "C:/IatFiles/pdf", "C:/IatFiles/file/pdf");
                    file.mvFile(filePdft, "C:/IatFiles/pdf", "C:/IatFiles/file/pdf");
                    
                    //Sgte Documento
                    filePdft = file.nextFile(@"c:\IatFiles\pdf", "*.pdf");

                    
                }
                

            }

     }
    }

