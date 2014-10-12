﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Json;



namespace IatDteBridge
{
    class TxtReader
    {
  

        public Documento lecturaEnDuro()
        {
            Documento factura = new Documento();
            factura.TipoDte = 33;
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
            factura.detalles.Last().impuestos.Add(new imp_adicional("", 0, 0));
            return factura;
        }

       
        public Documento lectura()
        {
            Documento doc = new Documento();

          /*  // Datos del Emisor
            String lineEmisor = String.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(@"c:\IatFiles\config\empresa" + ".txt"))
                {
                    int i = 1;
                    while ((lineEmisor = sr.ReadLine()) != null)
                    {
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
                            case 6: doc.DirOrigen = lineEmisor;
                            break;
                            case 7: doc.CmnaOrigen = lineEmisor;
                            break;
                            case 8: doc.CiudadOrigen = lineEmisor;
                            break;

                        }
                         
                    }

                    sr.Close();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            fileAdmin file = new fileAdmin();
            String fileName = file.nextFile(@"c:\IatFiles\file\","*.txt");  
            //Paso la ruta del fichero al constructor 
            StreamReader objReader = new StreamReader(fileName);

            string line = string.Empty;
           
            
            while ((line = objReader.ReadLine()) != null)
            {

                if (line == "->Encabezado<-")
                {
                    line = objReader.ReadLine();
                    List<String> lineEncabezado = line.Split(';').ToList();
                    int i = 1;
                    foreach (var itemEncabezado in lineEncabezado)
                    {
                        Console.WriteLine(itemEncabezado);
                        switch (i)
                        {
                            case 1:
                                doc.TipoDte = Convert.ToInt32(itemEncabezado);
                                break;
                            case 2:
                                doc.Folio = Convert.ToInt32(itemEncabezado);
                                break;
                            case 3:
                                doc.FchEmis = itemEncabezado;
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                doc.RUTRecep = itemEncabezado;
                                break;
                            case 7:
                                doc.RznSocRecep = itemEncabezado;
                                break;
                            case 8:
                                doc.GiroRecep = itemEncabezado;
                                break;
                            case 9:
                                doc.DirRecep = itemEncabezado;
                                break;
                            case 10:
                                doc.CmnaRecep = itemEncabezado;
                                break;
                            case 11:
                                doc.CiudadRecep = itemEncabezado;
                                break;
                            case 12:
                                break;

                        }
                        i++;
                    }

                }

                if (line == "->Totales<-")
                {
                    line = objReader.ReadLine();
                    List<String> lineEncabezado = line.Split(';').ToList();
                    int i = 1;
                    foreach (var itemEncabezado in lineEncabezado)
                    {
                        switch (i)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                doc.MntNeto = Convert.ToInt32(itemEncabezado);
                                break;
                            case 6:
                                break;
                            case 7:
                                doc.TasaIVA = Convert.ToInt32(itemEncabezado);
                                break;
                            case 8:
                                doc.IVA = Convert.ToInt32(itemEncabezado);
                                break;
                            case 9:
                                doc.MntTotal = Convert.ToInt32(itemEncabezado);
                                break;

                        }
                        i++;
                    }
                }


                if (line == "->Detalle<-")
                    {
                        line = objReader.ReadLine();

                        doc.detalles.Add(new det_documento(line));

                    }

                
            }

            objReader.Close();

            file.mvFile(fileName, @"c:\IatFiles\file\",@"c:\IatFiles\fileProcess\");
         */
            
            fileAdmin file = new fileAdmin();
            String fileName = file.nextFile(@"c:\IatFiles\file\", "*.json");
            //Paso la ruta del fichero al constructor 
            StreamReader objReader = new StreamReader(fileName);

            String data = objReader.ReadToEnd();


            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Documento));
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(data));

            doc = (Documento)js.ReadObject(ms);


            Console.WriteLine("Documento");
            Console.WriteLine("Rut Receptor: " + doc.RUTRecep);

            objReader.Close();
            ms.Close();
            

            return doc;
        }

    }

}
