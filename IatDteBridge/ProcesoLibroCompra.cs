using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class ProcesoLibroCompra
    {


        public void doLibroCompra()
        {
            String signLibroCompra = String.Empty;

            // inatancia txt_Libro
            TxtLiborCompra lib = new TxtLiborCompra();

            LibroCompra libroC = lib.lectura();

            xmlLibroCompra xml = new xmlLibroCompra();

            signLibroCompra = xml.libroCompra_to_xml(libroC);



            signLibroCompra = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + signLibroCompra;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/IatFiles/file/xml/LibroCompra" + "1" + ".xml", false, Encoding.GetEncoding("ISO-8859-1")))
            {
                file.WriteLine(signLibroCompra);
            }
            Console.WriteLine(signLibroCompra);


        }
    }
}

