using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using System.Data.SQLite;



namespace IatDteBridge
{
    class TxtReader
    {
        String strConn = @"Data Source=C:/IatFiles/iatDB.sqlite;Pooling=true;FailIfMissing=false;Version=3";
       
        public Documento lectura(String fileJson, bool moveFile, String dirOrigen)
        {
            Documento doc = new Documento();
            fileAdmin file = new fileAdmin();
            String fileName = String.Empty;

            if (dirOrigen == "")
            {
                dirOrigen = @"C:\IatFiles\file";
            }
            

            if (fileJson == "")
            {
                fileName = file.nextFile(dirOrigen, "*.json");
            }
            else
            {
                fileName = dirOrigen + fileJson;
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
                // Cargo datos en laclase Documento desde sqlite

                if (doc.RUTEmisor == null)
                {
                    try
                    {

                        SQLiteConnection myConn = new SQLiteConnection(strConn);
                        myConn.Open();

                        string sql = "select * from empresa";
                        SQLiteCommand command = new SQLiteCommand(sql, myConn);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            doc.RUTEmisor = reader["RutEmisor"].ToString();
                            doc.RznSoc = reader["RznSoc"].ToString();
                            doc.GiroEmis = reader["GiroEmis"].ToString();
                            doc.Telefono = reader["Telefono"].ToString();
                            doc.CorreoEmisor = reader["CorreoEmisor"].ToString();
                            doc.Acteco = Convert.ToInt32(reader["Acteco"]);
                            doc.CdgSIISucur = Convert.ToInt32(reader["CdgSIISucur"]);
                            doc.DirMatriz = reader["DirMatriz"].ToString();
                            doc.NombreCertificado = reader["NomCertificado"].ToString();
                            doc.SucurEmisor = reader["SucurEmisor"].ToString();
                            doc.FchResol = reader["FchResol"].ToString();
                            doc.RutEnvia = reader["RutCertificado"].ToString();
                            doc.NumResol = reader["NumResol"].ToString();
                            doc.CondEntrega = reader["CondEntrega"].ToString();

                        }
                        myConn.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e.ToString());
                    }
                }
                else
                {
                    try
                    {

                        SQLiteConnection myConn = new SQLiteConnection(strConn);
                        myConn.Open();

                        string sql = "select * from empresa";
                        SQLiteCommand command = new SQLiteCommand(sql, myConn);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            doc.Telefono = reader["Telefono"].ToString();
                            doc.CorreoEmisor = reader["CorreoEmisor"].ToString();
                            doc.Acteco = Convert.ToInt32(reader["Acteco"]);
                            doc.CdgSIISucur = Convert.ToInt32(reader["CdgSIISucur"]);
                            doc.DirRegionalSII = reader["sucurSII"].ToString();
                            doc.NombreCertificado = reader["NomCertificado"].ToString();
                            doc.SucurEmisor = reader["SucurEmisor"].ToString();
                            doc.FchResol = reader["FchResol"].ToString();
                            doc.RutEnvia = reader["RutCertificado"].ToString();
                            doc.NumResol = reader["NumResol"].ToString();
                            doc.CondEntrega = reader["CondEntrega"].ToString();
                        }

                        myConn.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: {0}", e.ToString());
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

                if (fileJson == "")
                {
                    doc.fileName = fileName;
                }
                else
                {
                    doc.fileName = fileJson;
                }
                return doc;
            }
            else
            {
                return null;
            }
            


        }

    }

}
