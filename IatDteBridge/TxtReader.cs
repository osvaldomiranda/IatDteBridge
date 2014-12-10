using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;



namespace IatDteBridge
{
    class TxtReader
    {
  
       
        public Documento lectura(String fileJson, bool moveFile)
        {
            Documento doc = new Documento();
            fileAdmin file = new fileAdmin();
            String fileName = String.Empty;

            if (fileJson == "")
            {
                fileName = file.nextFile(@"c:\IatFiles\file\", "*.json");
            }
            else
            {
                fileName = fileJson;
            }

           

           

            if (fileName != null)
            {
                StreamReader objReader = new StreamReader(fileName,System.Text.Encoding.Default,true);
                objReader.ToString();
                String data = objReader.ReadToEnd();
        
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Documento));
                
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));

                try
                {
                    doc = (Documento)js.ReadObject(ms);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    MessageBox.Show("Error de lectura JSON"+ e.Message);


                }

   
                // Datos del Emisor
                String lineEmisor = String.Empty;
                //try
               // {
                    using (StreamReader sr = new StreamReader(@"c:\IatFiles\config\empresa" + ".txt"))
                    {
                        int i = 1;
                        while ((lineEmisor = sr.ReadLine()) != null)
                        {

                            Console.WriteLine(lineEmisor);
                            switch (i)
                            {
                                case 1: doc.RUTEmisor = lineEmisor;
                                    break;
                                case 2: doc.RznSoc = lineEmisor;
                                    break;
                                case 3: doc.GiroEmis = lineEmisor;
                                    break;
                                case 4: doc.Telefono = lineEmisor;
                                    break;
                                case 5: doc.CorreoEmisor = lineEmisor;
                                    break;
                                case 6: doc.Acteco = Convert.ToInt32(lineEmisor);
                                    break;
                                case 7: doc.CdgSIISucur = Convert.ToInt32(lineEmisor);
                                    break;
                                case 8: doc.DirOrigen = lineEmisor;
                                    break;
                                case 9: doc.CmnaOrigen = lineEmisor;
                                    break;
                                case 10: doc.CiudadOrigen = lineEmisor;
                                    break;
                                case 11: doc.DirRegionalSII = lineEmisor;
                                    break;


                            }
                            

                            // recupero las sucursales del txt y los cargo en el list
                           /* if (i >= 12)
                            {

                                Console.WriteLine("i es {0} {1}", i, lineEmisor);
                                doc.sucursalesempresa.Add(new Sucursal(lineEmisor) {datosucursal = lineEmisor });
                               

                            }*/
                            i++;
                        }





                        sr.Close();
                    }
                /*}
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }*/


                objReader.Close();
                ms.Close();
                if (moveFile)
                {
                    file.mvFile(fileName, "C:/IatFiles/file/", "C:/IatFiles/fileProcess/");
                }
                

                // *************  Envía json a server
                
                Connect conn = new Connect();
                conn.sendInvoice(doc);

                // *************  Envía json a server


                Caf caf = new Caf();

                if(!caf.isValid(doc.FchEmis,doc.TipoDTE,doc.Folio))
                {
                    doc = null;
                }

                doc.fileName = fileName;
                return doc;
            }
            else
            {
                return null;
            }
            


        }

    }

}
