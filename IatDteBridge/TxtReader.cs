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
            Documento factura = new Documento();
            factura.TipoDte = 33;
            factura.Folio = 1;
            factura.RUTRecep = "14193259-4";
            factura.FchEmis = "2014-10-01 00:00:00";
            factura.RznSocRecep = "FABIAN ARNOLDO";
            factura.GiroRecep = "ALMACEN";
            factura.DirRecep = "LEBRELE #03572";
            factura.CmnaRecep ="LO ESPEJO";
            factura.CiudadRecep = "SANTIAGO";
            factura.MntNeto = 15068;
            factura.TasaIVA = 19;
            factura.IVA = 2863;
            factura.MntTotal = 19890;
            // estos son los valore del detalle no cache como hacerlo ayuda pliss???
            //1;2077;BEB ANDINA COCA COLA MINI  BT 250CC;100;167.4242;10;1674;0;0;0;15068.18;INT1;C/U;BEB ANDINA COCA COLA MINI  BT 250CC;






            return doc;
        }

    }
}
