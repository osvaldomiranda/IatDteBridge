using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class ProcesoLibroGuias
    {
        public void doLibroGuias()
        {
            String signLibroGuias = String.Empty;

            // inatancia txt_Libro
            TxtLibroGuias lib = new TxtLibroGuias();

            LibroGuias libroC = lib.lectura();

            xmlLibroGuias xml = new xmlLibroGuias();

            signLibroGuias = xml.do_libroGuias(libroC);



            signLibroGuias = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + signLibroGuias;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/IatFiles/file/xml/Libroguias" + "1" + ".xml", false, Encoding.GetEncoding("ISO-8859-1")))
            {
                file.WriteLine(signLibroGuias);
            }
            Console.WriteLine(signLibroGuias);


        }

    }
}
