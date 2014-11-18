using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class procesoLibroVenta
    {
        public void doLibroVta()
        {
            String signLibroCompra = String.Empty;

            // inatancia txt_Libro
            TxtLibroVenta lib = new TxtLibroVenta();

            LibroVenta libroC = lib.lectura();

            xmlLiborVenta xml = new xmlLiborVenta();

            signLibroCompra = xml.do_libroVentas(libroC);



            signLibroCompra = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + signLibroCompra;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/IatFiles/file/xml/LibroVenta" + "1" + ".xml", false, Encoding.GetEncoding("ISO-8859-1")))
            {
                file.WriteLine(signLibroCompra);
            }
            Console.WriteLine(signLibroCompra);


        }

    }
}
