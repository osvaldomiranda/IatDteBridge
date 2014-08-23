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
        private string ruta = "prueba.pdf";
        iTextSharp.text.Font fuente = new Font(iTextSharp.text.Font.FontFamily.HELVETICA,8,iTextSharp.text.Font.NORMAL,BaseColor.RED);

        public void OpenPdf()
        {

         Timbre timbre1 = new Timbre();
         timbre1.CreaTimbre();
         Console.WriteLine("Timbre creado!!");
        
        Document pdf = new Document(PageSize.LETTER,10,10,10,10);
        PdfWriter.GetInstance(pdf, new FileStream(ruta, FileMode.OpenOrCreate));

        pdf.Open();
        pdf.SetMargins(10,10,10,10);

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("logo.jpg");
        logo.ScaleAbsolute(100f, 50f);
        logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        
        iTextSharp.text.Image timbre = iTextSharp.text.Image.GetInstance("Timbre.jpg");
        timbre.SetAbsolutePosition(10,10);
        timbre.ScaleAbsolute(200f,100f);
        

        
        PdfPTable tabla = new PdfPTable(1);
        tabla.WidthPercentage = 30;
        tabla.HorizontalAlignment = 1;
   
       
        PdfPCell celda = new PdfPCell(new Paragraph("R.U.T 77.888.630-8 \n\nFACTURA ELECTRÓNICA \n\nNº1000",fuente));
        celda.BorderColor = BaseColor.RED;
        celda.HorizontalAlignment = 1;
        tabla.AddCell(celda);

 
        pdf.Add(logo);
        pdf.Add(timbre);
        pdf.Add(tabla);
    
        pdf.Add(new Paragraph("Razon social:"));
        pdf.Add(new Paragraph("Giro:"));
        pdf.Add(new Paragraph("Casa Matriz:"));
        pdf.Add(new Paragraph("Fonos:"));


        pdf.NewPage();
        pdf.Close();

        Console.WriteLine("Pdf creado!!");
        System.Diagnostics.Process.Start("prueba.pdf");

        }

        public void CreatePdf()
        {

        }
    }
}
