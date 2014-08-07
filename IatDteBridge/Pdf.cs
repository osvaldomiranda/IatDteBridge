using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace IatDteBridge
{
    class Pdf
    {
        public void CreatePdf()
        {

        Document pdf = new Document();
        PdfWriter.GetInstance(pdf, new FileStream("prueba.pdf", FileMode.OpenOrCreate));

        pdf.Open();
        pdf.Add(new Paragraph("Prueba DE Pdf Dte Bridge"));
        pdf.Close();


        }
    }
}
