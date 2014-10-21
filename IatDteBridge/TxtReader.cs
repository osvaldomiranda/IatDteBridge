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
  

        public Documento lecturaEnDuro()
        {
            Documento factura = new Documento();
            factura.TipoDTE = 33;
            factura.Folio = 1;
            factura.RUTRecep = "14193259-4";
            factura.FchEmis = "2014-10-01 00:00:00";
            factura.RznSocRecep = "FABIAN ARNOLDO";
            factura.GiroRecep = "ALMACEN";
            factura.DirRecep = "LEBRELE #03572";
            factura.CmnaRecep = "LO ESPEJO";
            factura.CiudadRecep = "SANTIAGO";
            factura.MntNeto = 15068;
            factura.TasaIVA = 19;
            factura.IVA = 2863;
            factura.MntTotal = 19890;
            // estos son los valore del detalle no cache como hacerlo ayuda pliss???
            //1;2077;BEB ANDINA COCA COLA MINI  BT 250CC;100;167.4242;10;1674;0;0;0;15068.18;INT1;C/U;BEB ANDINA COCA COLA MINI  BT 250CC;
          //  factura.detalle.Last().impuestos.Add(new imp_adicional("", 0, 0));
            return factura;
        }

       
        public Documento lectura()
        {
            Documento doc = new Documento();


            fileAdmin file = new fileAdmin();
            String fileName = file.nextFile(@"c:\IatFiles\file\", "*.json");

            if (fileName != null)
            {
                //Paso la ruta del fichero al constructor 
                StreamReader objReader = new StreamReader(fileName);
                String data = objReader.ReadToEnd();


                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Documento));
                MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(data));
                try
                {
                    doc = (Documento)js.ReadObject(ms);
                }
                catch (Exception e)
                {

                    Console.WriteLine("No hay mas archivos a procesar:");
                    Console.WriteLine(e.Message);
                    MessageBox.Show("No hay mas archivos a procesar:");


                }

                // Datos del Emisor
                String lineEmisor = String.Empty;
                try
                {
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

                            }
                            i++;
                        }

                        sr.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }




                objReader.Close();
                ms.Close();
                file.mvFile(fileName, "C:/IatFiles/file/", "C:/IatFiles/fileProcess/");
                return doc;
            }
            else
            {
                return null;
            }
            


        }

    }

}
