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
  
       
        public Documento lectura(String fileJson, bool moveFile, String dirOrigen)
        {
            Documento doc = new Documento();
            fileAdmin file = new fileAdmin();
            String fileName = String.Empty;

            if (dirOrigen == "") dirOrigen = @"C:\IatFiles\file";


            if (fileJson == "")
            {
                fileName = file.nextFile(dirOrigen, "*.json");
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
                if (doc.RUTEmisor == null)
                {
                    using (StreamReader sr = new StreamReader(@"c:\IatFiles\config\empresa.txt"))
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
                                case 12: doc.NombreCertificado = lineEmisor;
                                    break;
                                case 13: doc.SucurEmisor = lineEmisor;
                                    break;
                                case 14: doc.FchResol = lineEmisor;
                                    break;
                                case 15: doc.RutEnvia = lineEmisor;
                                    break;
                            }


                            i++;
                        }

                        sr.Close();
                    }
            
                }
                else 
                {
                    
                    using (StreamReader sr = new StreamReader(@"c:\IatFiles\config\empresa" + ".txt"))
                    {
                        int i = 1;
                        while ((lineEmisor = sr.ReadLine()) != null)
                        {

                            Console.WriteLine(lineEmisor);
                            switch (i)
                            {

                                case 4: doc.Telefono = lineEmisor;
                                    break;
                                case 5: doc.CorreoEmisor = lineEmisor;
                                    break;
                                case 6: doc.Acteco = Convert.ToInt32(lineEmisor);
                                    break;
                                case 7: doc.CdgSIISucur = Convert.ToInt32(lineEmisor);
                                    break;
                                case 11: doc.DirRegionalSII = lineEmisor;
                                    break;
                                case 12: doc.NombreCertificado = lineEmisor;
                                    break;
                                case 13: doc.SucurEmisor = lineEmisor;
                                    break;
                                case 14: doc.FchResol = lineEmisor;
                                    break;
                                case 15: doc.RutEnvia = lineEmisor;
                                    break;

                            }


                            i++;
                        }

                        sr.Close();
                    }
                }

                objReader.Close();
                ms.Close();
                if (moveFile)
                {
                    file.mvFile(fileName, dirOrigen, "C:/IatFiles/fileProcess/");
                }
                
                Caf caf = new Caf();

                if(!caf.isValid(doc))
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
