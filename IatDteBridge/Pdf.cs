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
        private string razonSocialEmisor = "COMERCIAL LUBBA LIMITADA";
        private string giroEmisor ="Comercializadora de confites, galletas, helados, bebidas, alimentos, cigarrillos, supermercado,deposito de licores, carniceria, articulos de bazar, ";
        private string direccionEmisor = "Casa Matriz: Abel Gonzalez # 44 - La Cisterna ";
        private string sucursalesEmisor = "Sucursales:\nDomingo Correa 25, Monumento 1963";
        private string datosSii = "S.I.I - SANTIAGO SUR";
        private string fechaDoc = "Santiago, 26 de Agosto de 2014";
        private String[] headerDetalle = { "Item", "Codigo", "Descripción", "Cantidad", "Unidad", "P Unit.", "Dscto.", "Valor" };
        private String[] datosDetalle = { "1", "7890231234", "COCA-COLA", "2", "UNID", "1200", "0", "2400", "2", "7890231234", "FANTA", "2", "UNID", "1200", "0", "2400", "3", "7890231234", "FANTA", "2", "UNID", "1200", "0", "2400", "4", "7890231234", "FANTA", "2", "UNID", "1200", "0", "2400", "5", "7890231234", "FANTA", "2", "UNID", "1200", "0", "2400" };
        private String[] datosHeaderReferencia = { "Tipo de Documento", "Folio", "Fecha", "Razón Referancia" };
        iTextSharp.text.Font fuenteNegra = new Font(iTextSharp.text.Font.FontFamily.HELVETICA,8,iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font fuenteRoja = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.RED);
        public void OpenPdf()
        {

         Timbre timbre1 = new Timbre();
         timbre1.CreaTimbre();
         Console.WriteLine("Timbre creado!!");
        
        Document pdf = new Document(PageSize.LETTER);
        PdfWriter.GetInstance(pdf, new FileStream(ruta, FileMode.OpenOrCreate));

        pdf.Open();

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("logo.jpg");
        logo.ScaleAbsolute(100f, 50f);
        logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
        
        iTextSharp.text.Image timbre = iTextSharp.text.Image.GetInstance("Timbre.jpg");
        timbre.SetAbsolutePosition(10,10);
        timbre.ScaleAbsolute(200f,100f);

        float[] anchosCabecera = new float[] { 210f, 500f, 250f };


        PdfPTable cabecera = new PdfPTable(3);
        cabecera.SetWidths(anchosCabecera);
        cabecera.WidthPercentage = 100;
        cabecera.HorizontalAlignment = 0;

        Paragraph contenedorCabecera = new Paragraph();
        contenedorCabecera.Add(cabecera);
        contenedorCabecera.SpacingAfter = 1500f;


        PdfPCell celdaLogo = new PdfPCell(logo);
        celdaLogo.BorderWidth = 0;
        celdaLogo.VerticalAlignment = 0;
        cabecera.AddCell(celdaLogo);

        PdfPCell celdaDatosEmisor = new PdfPCell(new Paragraph(razonSocialEmisor + "\n" + giroEmisor + "\n" + direccionEmisor,fuenteNegra));
        celdaDatosEmisor.BorderWidth = 0;
        cabecera.AddCell(celdaDatosEmisor);
       
        PdfPCell celdaFolio = new PdfPCell(new Paragraph("R.U.T 77.888.630-8 \n\nFACTURA ELECTRÓNICA \n\nNº1000",fuenteRoja));
        celdaFolio.BorderColor = BaseColor.RED;
        celdaFolio.HorizontalAlignment = 1; 
        celdaFolio.BorderWidth = 2;
        cabecera.AddCell(celdaFolio);


        PdfPCell celdaVacia = new PdfPCell(new Paragraph(""));
        celdaVacia.HorizontalAlignment = 1;
        celdaVacia.BorderWidth = 0;
        cabecera.AddCell(celdaVacia);

        PdfPCell celdaSucursalesEmisor = new PdfPCell(new Paragraph(sucursalesEmisor,fuenteNegra));
        celdaSucursalesEmisor.HorizontalAlignment = 0;
        celdaSucursalesEmisor.BorderWidth = 0;
        cabecera.AddCell(celdaSucursalesEmisor); 
        
        PdfPCell celdaDatosSii = new PdfPCell(new Paragraph(datosSii,fuenteRoja));
        celdaDatosSii.HorizontalAlignment = 1;
        celdaDatosSii.BorderWidth = 0;
        cabecera.AddCell(celdaDatosSii);

        PdfPCell celdaFechaDoc = new PdfPCell(new Paragraph(fechaDoc, fuenteRoja));
        celdaFechaDoc.Colspan = 3;
        celdaFechaDoc.HorizontalAlignment = 2;
        celdaFechaDoc.BorderWidth = 0;
        cabecera.AddCell(celdaFechaDoc);
        


       float[] anchosDatosReceptor = new float[] { 100f, 500f,100f,300f };
        
        PdfPTable datosReceptor = new PdfPTable(4);
        datosReceptor.SetWidths(anchosDatosReceptor);
        datosReceptor.WidthPercentage = 100;
        datosReceptor.HorizontalAlignment = 0;

        PdfPCell celdaEtiquetaSenor = new PdfPCell(new Paragraph("Señor (es): ", fuenteNegra));
        celdaEtiquetaSenor.HorizontalAlignment = 0;
        celdaEtiquetaSenor.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaSenor);

        PdfPCell celdaSenior = new PdfPCell(new Paragraph("MAURICIO JIMENEZ YAÑEZ", fuenteNegra));
        celdaSenior.HorizontalAlignment = 0;
        celdaSenior.BorderWidth= 0;
        datosReceptor.AddCell(celdaSenior);

        PdfPCell celdaEtiquetaRut = new PdfPCell(new Paragraph("Rut: ", fuenteNegra));
        celdaEtiquetaRut.HorizontalAlignment = 0;
        celdaEtiquetaRut.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaRut);

        PdfPCell celdaRutRecep = new PdfPCell(new Paragraph("12.891.016-6", fuenteNegra));
        celdaRutRecep.HorizontalAlignment = 0;
        celdaRutRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaRutRecep);

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Segunda fila +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        PdfPCell celdaEtiquetaDireccion = new PdfPCell(new Paragraph("Dirección: ", fuenteNegra));
        celdaEtiquetaDireccion.HorizontalAlignment = 0;
        celdaEtiquetaDireccion.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaDireccion);

        PdfPCell celdaDireccionRecep = new PdfPCell(new Paragraph("LAGUNA BLANCA # 1641  ", fuenteNegra));
        celdaDireccionRecep.HorizontalAlignment = 0;
        celdaDireccionRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaDireccionRecep);

        PdfPCell celdaEtiquetaComuna = new PdfPCell(new Paragraph("Comuna: ", fuenteNegra));
        celdaEtiquetaComuna.HorizontalAlignment = 0;
        celdaEtiquetaComuna.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaComuna);

        PdfPCell celdaComunaRecep = new PdfPCell(new Paragraph("PEÑALOLEN", fuenteNegra));
        celdaComunaRecep.HorizontalAlignment = 0;
        celdaComunaRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaComunaRecep);

 // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Tercera fila +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        PdfPCell celdaEtiquetaGiroRecep = new PdfPCell(new Paragraph("Giro: ", fuenteNegra));
        celdaEtiquetaGiroRecep.HorizontalAlignment = 0;
        celdaEtiquetaGiroRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaGiroRecep);

        PdfPCell celdaGiroRecep = new PdfPCell(new Paragraph("ADMINISTRACIÓN PARA SISTEMAS DE INFORMACIÓN", fuenteNegra));
        celdaGiroRecep.HorizontalAlignment = 0;
        celdaGiroRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaGiroRecep);

        PdfPCell celdaEtiquetaTelefono = new PdfPCell(new Paragraph("Teléfono: ", fuenteNegra));
        celdaEtiquetaTelefono.HorizontalAlignment = 0;
        celdaEtiquetaTelefono.BorderWidth = 0;
        datosReceptor.AddCell(celdaEtiquetaTelefono);

        PdfPCell celdaTelefonoRecep = new PdfPCell(new Paragraph("2 2278 5567", fuenteNegra));
        celdaTelefonoRecep.HorizontalAlignment = 0;
        celdaTelefonoRecep.BorderWidth = 0;
        datosReceptor.AddCell(celdaTelefonoRecep);

        PdfPTable contenedorDatosReceptor = new PdfPTable(1);
        contenedorDatosReceptor.WidthPercentage = 100;
        PdfPCell celdaContDatRecep = new PdfPCell(datosReceptor);
        contenedorDatosReceptor.AddCell(celdaContDatRecep);

//+++++++++++++++++++++++++++++++++++++++++++++++++++++ Detalle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        PdfPTable detalle = new PdfPTable(8);
        detalle.WidthPercentage = 100;
               
            
       foreach (string j in headerDetalle)
        {
            PdfPCell celda = new PdfPCell(new Paragraph(j, fuenteNegra));;
            celda.BackgroundColor = BaseColor.GRAY;
            celda.HorizontalAlignment = 1;
            celda.BorderWidth = 0;
            detalle.AddCell(celda);
 
        }

       foreach (string a in datosDetalle)
       {
           PdfPCell celda = new PdfPCell(new Paragraph(a, fuenteNegra)); ;
           celda.HorizontalAlignment = 1;
           celda.BorderWidth = 0;
           detalle.AddCell(celda);

       }


       PdfPTable contenedorDetalle = new PdfPTable(1);
       contenedorDetalle.WidthPercentage = 100;
       PdfPCell celdaContenedorDetalle = new PdfPCell(detalle);
       celdaContenedorDetalle.MinimumHeight = 300f;
       contenedorDetalle.AddCell(celdaContenedorDetalle);
//++++++++++++++++++++++++++++++++++++++++++++++++++++ referencias +++++++++++++++++++++++++++++++++++++
       PdfPTable referencias = new PdfPTable(4);
       referencias.WidthPercentage = 100;

       PdfPCell headerReferncia = new PdfPCell(new Paragraph("Referencia a otros Documentos",fuenteNegra));
       headerReferncia.Colspan = 4;
       headerReferncia.HorizontalAlignment = 1;
       headerReferncia.BackgroundColor = BaseColor.GRAY;
       headerReferncia.BorderWidth = 0;
       referencias.AddCell(headerReferncia);

       foreach (string b in datosHeaderReferencia)
       {
           PdfPCell celda = new PdfPCell(new Paragraph(b, fuenteNegra)); ;
           celda.BackgroundColor = BaseColor.GRAY;
           celda.HorizontalAlignment = 1;
           celda.BorderWidth = 0;
           referencias.AddCell(celda);

       }
//++++++++++++++++++++++++++++++++++++++++++++++++++ Pie de pagina ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
       PdfPTable footer = new PdfPTable(2);
       footer.WidthPercentage = 100;
       PdfPCell celdaTimbre = new PdfPCell(timbre);
       celdaTimbre.BorderWidth = 0;
       celdaTimbre.MinimumHeight = 200;
       footer.AddCell(celdaTimbre);

       PdfPTable totales = new PdfPTable(2);
       totales.WidthPercentage = 100;

       PdfPCell celdaEtiquetaDescuento = new PdfPCell(new Paragraph("Descuento: ", fuenteNegra));
       celdaEtiquetaDescuento.BorderWidth = 0;
       totales.AddCell(celdaEtiquetaDescuento);

       PdfPCell celdaDescuento = new PdfPCell(new Paragraph("$ 00000000", fuenteNegra));
       celdaDescuento.BorderWidth = 0;
       totales.AddCell(celdaDescuento); 
 
       PdfPCell celdaEtiquetaSubTotal = new PdfPCell(new Paragraph("Sub Total: ",fuenteNegra));
       celdaEtiquetaSubTotal.BorderWidth = 0;
       totales.AddCell(celdaEtiquetaSubTotal);

       PdfPCell celdaSubTotal = new PdfPCell(new Paragraph("$ 00000000", fuenteNegra));
       celdaSubTotal.BorderWidth = 0;
       totales.AddCell(celdaSubTotal);

       PdfPCell celdaEtiquetaMontoExento = new PdfPCell(new Paragraph("Monto Exento:  ", fuenteNegra));
       celdaEtiquetaMontoExento.BorderWidth = 0;
       totales.AddCell(celdaEtiquetaMontoExento);

       PdfPCell celdaMontoExento = new PdfPCell(new Paragraph("$ 00000000", fuenteNegra));
       celdaMontoExento.BorderWidth = 0;
       totales.AddCell(celdaMontoExento);
 
       PdfPCell celdaEtiquetaIva = new PdfPCell(new Paragraph("IVA (19%):  ", fuenteNegra));
       celdaEtiquetaIva.BorderWidth = 0;
       totales.AddCell(celdaEtiquetaIva);

       PdfPCell celdaIva = new PdfPCell(new Paragraph("$ 00000000", fuenteNegra));
       celdaIva.BorderWidth = 0;
       totales.AddCell(celdaIva);
 
       PdfPCell celdaEtiquetaMontoTotal = new PdfPCell(new Paragraph("Monto Total:  ", fuenteNegra));
       celdaEtiquetaMontoTotal.BorderWidth = 0;
       totales.AddCell(celdaEtiquetaMontoTotal);

       PdfPCell celdaMontoTotal = new PdfPCell(new Paragraph("$ 00000000", fuenteNegra));
       celdaMontoTotal.BorderWidth = 0;
       totales.AddCell(celdaMontoTotal);
      
       PdfPCell celdaTotales = new PdfPCell(totales);
       celdaTotales.BorderWidth = 0;
       celdaTotales.MinimumHeight = 200;
       footer.AddCell(celdaTotales);
      
        pdf.Add(cabecera);
        pdf.Add(contenedorDatosReceptor);
        pdf.Add(new Paragraph(" "));
        pdf.Add(contenedorDetalle);
        pdf.Add(new Paragraph(" "));
        pdf.Add(referencias);
        pdf.Add(new Paragraph(" "));
        pdf.Add(footer);
        pdf.NewPage();
        pdf.Close();

        Console.WriteLine("Pdf creado!!");
        System.Diagnostics.Process.Start("prueba.pdf");

        }

    }
}
