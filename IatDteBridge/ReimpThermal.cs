using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;


namespace IatDteBridge
{
    class ReimpThermal
    {
        public void reimp(Documento docLectura, String xmlFilename, String impresora)
        {
            GetTed ted = new GetTed();
            String TimbreElec = ted.getTed(xmlFilename);

            Thermal thermal = new Thermal();
            thermal.doc = docLectura;
            thermal.dd = TimbreElec;
            //  
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = new PaperSize("", 284, 1600);
            pd.PrintPage += new PrintPageEventHandler(thermal.OpenThermal);
            pd.PrinterSettings.PrinterName = impresora;
            Console.WriteLine(pd.ToString());
            pd.Print();

        }
    }
}
