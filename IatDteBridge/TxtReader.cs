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
        public void OpenFile()
        {
            StreamReader objReader = new StreamReader("c://file/test.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            foreach (string sOutput in arrText)
                Console.WriteLine(sOutput);
            Console.ReadLine();
        }

        Documento lecturaEnDuro()
        {
            Documento doc = new Documento();
            doc.TipoDte = 33;
            doc.Folio = 1;
            doc.FchEmis = "2014-10-01 00:00:00";
            return doc;
        }

    }
}
