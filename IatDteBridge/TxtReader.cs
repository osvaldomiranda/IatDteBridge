using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;



namespace IatDteBridge
{
    class TxtReader
    {
        public String nextFile()
        {
            String fileName = String.Empty;

            string currentDirName = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirName);

            if (!System.IO.Directory.Exists(@"C:\file\"))
            {
                System.IO.Directory.CreateDirectory(@"C:\file\");
            }

            System.IO.Directory.SetCurrentDirectory(@"C:\file\");

            currentDirName = System.IO.Directory.GetCurrentDirectory();
            
            string[] files = System.IO.Directory.GetFiles(currentDirName, "*.txt");

            string s = files.First();
            
            System.IO.FileInfo fi = null;
            try
            {
               fi = new System.IO.FileInfo(s);
            }
            catch (System.IO.FileNotFoundException e)
            {
                    Console.WriteLine(e.Message);
            }

            fileName = fi.Name;
            Console.WriteLine("{0} : {1}", fi.Name, fi.Directory);
            
            return fileName;
        }


        public void mvFile(String fileName)
        {

            string path = @"c:\file\"+fileName;
            string path2 = @"c:\fileProcess\"+fileName;
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    using (FileStream fs = System.IO.File.Create(path)) { }
                }

             
                if (System.IO.File.Exists(path2))
                    System.IO.File.Delete(path2);

                System.IO.File.Move(path, path2);
                Console.WriteLine("{0} was moved to {1}.", path, path2);

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


        }


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

            // Datos del Emisor
            String lineEmisor = String.Empty;
            try
            {
                using (StreamReader sr = new StreamReader("c://file/empresa" + ".txt"))
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

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }



            String fileName = nextFile();  
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

            mvFile(fileName);
            return doc;
        }

    }

}
