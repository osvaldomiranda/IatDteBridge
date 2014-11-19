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
    class TxtLibroGuias
    {
        public LibroGuias lectura()
        {
            LibroGuias lib = new LibroGuias();


            fileAdmin file = new fileAdmin();
            String fileName = file.nextFile(@"c:\IatFiles\file\libroguia\", "*.json");

            if (fileName != null)
            {
                //Paso la ruta del fichero al constructor 
                StreamReader objReader = new StreamReader(fileName, System.Text.Encoding.Default, true);
                objReader.ToString();
                String data = objReader.ReadToEnd();

                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(LibroGuias));

                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));

                try
                {
                    lib = (LibroGuias)js.ReadObject(ms);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    MessageBox.Show("Error de lectura JSON" + e.Message);


                }


                objReader.Close();
                ms.Close();
                file.mvFile(fileName, "C:/IatFiles/file/libroguia/", "C:/IatFiles/fileProcess/");
                return lib;
            }
            else
            {
                return null;
            }



        }

    }
}
