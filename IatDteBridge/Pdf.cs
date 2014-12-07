using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;

namespace IatDteBridge
{
    class Pdf
    {

        private string sucursalesEmisor = "";
        private String[] headerDetalle = { "Item", "Codigo", "Descripción", "Cantidad", "Unidad", "P Unit.", "Dscto.", "Valor" };
        private String[] datosDetalle = new String[300];
        private String[] datosHeaderReferencia = { "Tipo de Documento", "Folio", "Fecha", "Razón Referancia" };
        iTextSharp.text.Font fuenteNegra = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font fuenteBold = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fuenteRoja = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);
        // iTextSharp.text.Font fuenteNegrita = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font., BaseColor.RED);


        public Document OpenPdf(String dd, Documento doc, String fileName, String tipoCopia)//OpenPdf(Documento doc, String dd)
        {

            String nombreDocumento = String.Empty;

            switch (doc.TipoDTE)
            {
                case 33: nombreDocumento = "FACTURA ELECTRONICA";
                break;
                case 34: nombreDocumento = "FACTURA EXENTA ELECTRONICA";
                break;
                case 61: nombreDocumento = "NOTA DE CREDITO ELECTRONICA";
                break;
                case 56: nombreDocumento = "NOTA DE DEBITO ELECTRONICA";
                break;
                case 52: nombreDocumento = "GUIA DESPACHO ELECTRONICA";
                break;

            }



            Timbre timbre1 = new Timbre();
            timbre1.CreaTimbre(dd);
            Console.WriteLine("Timbre creado!!");

            Document pdf = new Document(PageSize.LETTER,10f,10f,10f,10f);
            PdfWriter.GetInstance(pdf, new FileStream(fileName, FileMode.OpenOrCreate));


            pdf.Open();

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"C:\IatFiles\config\logo.jpg");//
            logo.ScaleAbsolute(100f, 50f);
            logo.Alignment = iTextSharp.text.Image.ALIGN_LEFT;

            iTextSharp.text.Image timbre = iTextSharp.text.Image.GetInstance("Timbre.jpg");
            timbre.SetAbsolutePosition(10, 10);
            timbre.ScaleAbsolute(200f, 100f);

            float[] anchosCabecera = new float[] { 200f, 500f, 300f };


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

            PdfPCell celdaDatosEmisor = new PdfPCell(new Paragraph(doc.RznSoc + "\n" + doc.GiroEmis + "\n" + "FONOS: " + doc.Telefono + "\n" + "CASA MATRIZ: " + doc.DirOrigen + 
                "\n" + "SUCURSALES: \n" + doc.SucursalesEmpresa, fuenteNegra));
            celdaDatosEmisor.BorderWidth = 0;
            cabecera.AddCell(celdaDatosEmisor);

            // Agraga separadores al rut

            String rutemisor = doc.RUTEmisor;
            rutemisor = rutemisor.Insert(2, ".");
            rutemisor = rutemisor.Insert(6, ".");

            PdfPCell celdaFolio = new PdfPCell(new Paragraph("R.U.T " + rutemisor + " \n\n"+ nombreDocumento +" \n\nNº " + doc.Folio + "\n\n", fuenteRoja));
            celdaFolio.BorderColor = BaseColor.RED;
            celdaFolio.HorizontalAlignment = 1;
            celdaFolio.BorderWidth = 2;
            cabecera.AddCell(celdaFolio);


            PdfPCell celdaVacia = new PdfPCell(new Paragraph(""));
            celdaVacia.HorizontalAlignment = 1;
            celdaVacia.BorderWidth = 0;
            cabecera.AddCell(celdaVacia);

            PdfPCell celdaSucursalesEmisor = new PdfPCell(new Paragraph(sucursalesEmisor, fuenteNegra));
            celdaSucursalesEmisor.HorizontalAlignment = 0;
            celdaSucursalesEmisor.BorderWidth = 0;
            cabecera.AddCell(celdaSucursalesEmisor);

            PdfPCell celdaDatosSii = new PdfPCell(new Paragraph(doc.DirRegionalSII, fuenteRoja));
            celdaDatosSii.HorizontalAlignment = 1;
            celdaDatosSii.BorderWidth = 0;
            cabecera.AddCell(celdaDatosSii);

            // convierte fecha
            DateTime fechaemis = Convert.ToDateTime(doc.FchEmis);
            int dia = fechaemis.Day;
            string mesletra = fechaemis.ToString("MMMMM");
            int ano = fechaemis.Year;




            PdfPCell celdaFechaDoc = new PdfPCell(new Paragraph("Santiago, " + dia + " de " + mesletra + " de " + ano, fuenteRoja));
            celdaFechaDoc.Colspan = 3;
            celdaFechaDoc.HorizontalAlignment = 2;
            celdaFechaDoc.BorderWidth = 0;
            cabecera.AddCell(celdaFechaDoc);



            float[] anchosDatosReceptor = new float[] { 100f, 500f, 100f, 300f };

            PdfPTable datosReceptor = new PdfPTable(4);
            datosReceptor.SetWidths(anchosDatosReceptor);
            datosReceptor.WidthPercentage = 100;
            datosReceptor.HorizontalAlignment = 0;

            PdfPCell celdaEtiquetaSenor = new PdfPCell(new Paragraph("Señor (es): ", fuenteNegra));
            celdaEtiquetaSenor.HorizontalAlignment = 0;
            celdaEtiquetaSenor.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaSenor);

            PdfPCell celdaSenior = new PdfPCell(new Paragraph(doc.RznSocRecep, fuenteNegra));
            celdaSenior.HorizontalAlignment = 0;
            celdaSenior.BorderWidth = 0;
            datosReceptor.AddCell(celdaSenior);

            PdfPCell celdaEtiquetaRut = new PdfPCell(new Paragraph("Rut: ", fuenteNegra));
            celdaEtiquetaRut.HorizontalAlignment = 0;
            celdaEtiquetaRut.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaRut);

            // Agraga separadores al rut

            String rutrecep = doc.RUTRecep;
            rutrecep = rutrecep.Insert(2, ".");
            rutrecep = rutrecep.Insert(6, ".");

            PdfPCell celdaRutRecep = new PdfPCell(new Paragraph(rutrecep, fuenteNegra));
            celdaRutRecep.HorizontalAlignment = 0;
            celdaRutRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaRutRecep);

            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Segunda fila +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            PdfPCell celdaEtiquetaDireccion = new PdfPCell(new Paragraph("Dirección: ", fuenteNegra));
            celdaEtiquetaDireccion.HorizontalAlignment = 0;
            celdaEtiquetaDireccion.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaDireccion);

            PdfPCell celdaDireccionRecep = new PdfPCell(new Paragraph(doc.DirRecep, fuenteNegra));
            celdaDireccionRecep.HorizontalAlignment = 0;
            celdaDireccionRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaDireccionRecep);

            PdfPCell celdaEtiquetaComuna = new PdfPCell(new Paragraph("Comuna: ", fuenteNegra));
            celdaEtiquetaComuna.HorizontalAlignment = 0;
            celdaEtiquetaComuna.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaComuna);

            PdfPCell celdaComunaRecep = new PdfPCell(new Paragraph(doc.CmnaRecep, fuenteNegra));
            celdaComunaRecep.HorizontalAlignment = 0;
            celdaComunaRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaComunaRecep);

            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ Tercera fila +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            PdfPCell celdaEtiquetaGiroRecep = new PdfPCell(new Paragraph("Giro: ", fuenteNegra));
            celdaEtiquetaGiroRecep.HorizontalAlignment = 0;
            celdaEtiquetaGiroRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaGiroRecep);

            PdfPCell celdaGiroRecep = new PdfPCell(new Paragraph(doc.GiroRecep, fuenteNegra));
            celdaGiroRecep.HorizontalAlignment = 0;
            celdaGiroRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaGiroRecep);

            PdfPCell celdaEtiquetaTelefono = new PdfPCell(new Paragraph("Teléfono: ", fuenteNegra));
            celdaEtiquetaTelefono.HorizontalAlignment = 0;
            celdaEtiquetaTelefono.BorderWidth = 0;
            datosReceptor.AddCell(celdaEtiquetaTelefono);

            PdfPCell celdaTelefonoRecep = new PdfPCell(new Paragraph("", fuenteNegra));
            celdaTelefonoRecep.HorizontalAlignment = 0;
            celdaTelefonoRecep.BorderWidth = 0;
            datosReceptor.AddCell(celdaTelefonoRecep);

            PdfPTable contenedorDatosReceptor = new PdfPTable(1);
            contenedorDatosReceptor.WidthPercentage = 100;
            PdfPCell celdaContDatRecep = new PdfPCell(datosReceptor);
            contenedorDatosReceptor.AddCell(celdaContDatRecep);

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++ Detalle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            float[] anchosDetalle = new float[] { 30f, 30f, 200f, 30f, 30f, 30f, 30f, 30f };

            PdfPTable detalle = new PdfPTable(8);
            detalle.SetWidths(anchosDetalle);
            detalle.WidthPercentage = 100;


            foreach (string j in headerDetalle)
            {
                PdfPCell celda = new PdfPCell(new Paragraph(j, fuenteNegra)); ;
                celda.BackgroundColor = BaseColor.GRAY;
                celda.HorizontalAlignment = 1;
                celda.BorderWidth = 0;
                detalle.AddCell(celda);

            }

            //Captura el codigo de referencia
            String codigoreferencia = String.Empty;
            foreach (var codref in doc.Referencia)
            {
                codigoreferencia = codref.CodRef.ToString(); 
            }


            int puntero = 0;
            String nmbitem = String.Empty;
            foreach (var det in doc.detalle)
            {

                datosDetalle[puntero] = Convert.ToString(det.NroLinDet);
                puntero = puntero + 1;
                datosDetalle[puntero] = Convert.ToString(det.VlrCodigo);
                //controla el largo de nombre item
                if (det.NmbItem.Length <= 40)
                    nmbitem = det.NmbItem;
                else
                    nmbitem = det.NmbItem.Substring(0, 40);
                if (codigoreferencia == "2")
                    nmbitem = det.NmbItem;
                puntero = puntero + 1;
                datosDetalle[puntero] = Convert.ToString(nmbitem);
                puntero = puntero + 1;
                datosDetalle[puntero] = Convert.ToString(det.QtyItem);
                puntero = puntero + 1;
                datosDetalle[puntero] = Convert.ToString(det.UnmdItem);
                puntero = puntero + 1;
                datosDetalle[puntero] = det.PrcItem.ToString("N0",CultureInfo.CreateSpecificCulture("es-ES"));
                puntero = puntero + 1;
                datosDetalle[puntero] = Convert.ToString(det.DescuentoMonto);
                puntero = puntero + 1;
                datosDetalle[puntero] = det.MontoItem.ToString("N0",CultureInfo.CreateSpecificCulture("es-ES"));
                puntero = puntero + 1;

            }

            foreach (String a in datosDetalle)
            {
                PdfPCell celda = new PdfPCell(new Paragraph(a, fuenteNegra));
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

            PdfPTable datosReferencias = new PdfPTable(4);
            datosReferencias.WidthPercentage = 100;



            if (doc.Referencia.Count > 0)
            {


                PdfPCell headerReferncia = new PdfPCell(new Paragraph("Referencia a otros Documentos", fuenteNegra));
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
                    celda.BorderWidth = 1;
                    referencias.AddCell(celda);

                }

                //"Tipo de Documento", "Folio", "Fecha", "Razón Referancia"


                    foreach (var b in doc.Referencia)
                    {
                        if (b.NroLinRef == 0)
                        {
                        }
                        else
                        {
                            String tipoDocRef = String.Empty;

                            if (b.TpoDocRef == "SET")
                            {
                                tipoDocRef = "SET";
                            }
                            else
                            {

                                switch (Convert.ToInt32(b.TpoDocRef))
                                {
                                    case 33: tipoDocRef = "FACTURA ELECTRONICA";
                                        break;
                                    case 34: tipoDocRef = "FACTURA EXENTA ELECTRONICA";
                                        break;
                                    case 61: tipoDocRef = "NOTA DE CREDITO ELECTRONICA";
                                        break;
                                    case 56: tipoDocRef = "NOTA DE DEBITO ELECTRONICA";
                                        break;
                                    case 52: tipoDocRef = "GUIA DESPACHO ELECTRONICA";
                                        break;

                                }
                            }

                            PdfPCell celda0 = new PdfPCell(new Paragraph(tipoDocRef, fuenteNegra));
                            celda0.HorizontalAlignment = 1;
                            celda0.BorderWidth = 1;
                            datosReferencias.AddCell(celda0);

                            PdfPCell celda1 = new PdfPCell(new Paragraph(b.FolioRef, fuenteNegra));
                            celda1.HorizontalAlignment = 1;
                            celda1.BorderWidth = 1;
                            datosReferencias.AddCell(celda1);

                            PdfPCell celda2 = new PdfPCell(new Paragraph(b.FchRef, fuenteNegra));
                            celda2.HorizontalAlignment = 1;
                            celda2.BorderWidth = 1;
                            datosReferencias.AddCell(celda2);

                            PdfPCell celda3 = new PdfPCell(new Paragraph(b.RazonRef, fuenteNegra));
                            celda3.HorizontalAlignment = 1;
                            celda3.BorderWidth = 1;
                            datosReferencias.AddCell(celda3);
                        }
                    }

                                   

            }
            

            
            //++++++++++++++++++++++++++++++++++++++++++++++++++ Pie de pagina ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            PdfPTable tablaDTimbre = new PdfPTable(1);
            tablaDTimbre.WidthPercentage = 100;

            PdfPCell celdaTimbre = new PdfPCell(timbre);
            celdaTimbre.BorderWidth = 0;
            celdaTimbre.MinimumHeight = 100;
            celdaTimbre.HorizontalAlignment = 1;
            tablaDTimbre.AddCell(celdaTimbre);

            PdfPCell celdaTxtTimbre1 = new PdfPCell(new Paragraph("Timbre Electrónico S.I.I.", fuenteNegra));
            celdaTxtTimbre1.BorderWidth = 0;
            celdaTxtTimbre1.MinimumHeight = 12;
            celdaTxtTimbre1.HorizontalAlignment = 1;
            tablaDTimbre.AddCell(celdaTxtTimbre1);

            PdfPCell celdaTxtTimbre2 = new PdfPCell(new Paragraph("Resolución Ex. SII Nº 0 del 2010 verifique documento:", fuenteNegra));
            celdaTxtTimbre2.BorderWidth = 0;
            celdaTxtTimbre2.MinimumHeight = 12;
            celdaTxtTimbre2.HorizontalAlignment = 1;
            tablaDTimbre.AddCell(celdaTxtTimbre2);

            PdfPCell celdaTxtTimbre3 = new PdfPCell(new Paragraph("www.sii.cl", fuenteNegra));
            celdaTxtTimbre3.BorderWidth = 0;
            celdaTxtTimbre3.MinimumHeight = 12;
            celdaTxtTimbre3.HorizontalAlignment = 1;
            tablaDTimbre.AddCell(celdaTxtTimbre3);

          //  footer.AddCell(tablaTimbre);


            PdfPTable totales = new PdfPTable(2);
            totales.HorizontalAlignment = 0;
            totales.WidthPercentage = 80;

            PdfPCell celdaEtiquetaDescuento = new PdfPCell(new Paragraph("Descuento: ", fuenteNegra));
            celdaEtiquetaDescuento.BorderWidth = 1;
            celdaEtiquetaDescuento.HorizontalAlignment = 2;
            totales.AddCell(celdaEtiquetaDescuento);

            PdfPCell celdaDescuento = new PdfPCell(new Paragraph("$ 0", fuenteNegra));
            celdaDescuento.BorderWidth = 1;
            celdaDescuento.HorizontalAlignment = 2;
            totales.AddCell(celdaDescuento);

            PdfPCell celdaEtiquetaSubTotal = new PdfPCell(new Paragraph("Sub Total: ", fuenteNegra));
            celdaEtiquetaSubTotal.BorderWidth = 1;
            celdaEtiquetaSubTotal.HorizontalAlignment = 2;
            totales.AddCell(celdaEtiquetaSubTotal);

            PdfPCell celdaSubTotal = new PdfPCell(new Paragraph("$ " + doc.MntNeto.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), fuenteNegra));
            celdaSubTotal.BorderWidth = 1;
            celdaSubTotal.HorizontalAlignment = 2;
            totales.AddCell(celdaSubTotal);

            PdfPCell celdaEtiquetaMontoExento = new PdfPCell(new Paragraph("Monto Exento:  ", fuenteNegra));
            celdaEtiquetaMontoExento.BorderWidth = 1;
            celdaEtiquetaMontoExento.HorizontalAlignment = 2;
            totales.AddCell(celdaEtiquetaMontoExento);

            PdfPCell celdaMontoExento = new PdfPCell(new Paragraph("$ " + doc.MntExe.ToString("N0",CultureInfo.CreateSpecificCulture("es-ES")), fuenteNegra));
            celdaMontoExento.HorizontalAlignment = 2;
            celdaMontoExento.BorderWidth = 1;
            totales.AddCell(celdaMontoExento);

            PdfPCell celdaEtiquetaIva = new PdfPCell(new Paragraph("IVA (" + doc.TasaIVA + "%):  ", fuenteNegra));
            celdaEtiquetaIva.BorderWidth = 1;
            celdaEtiquetaIva.HorizontalAlignment = 2;
            totales.AddCell(celdaEtiquetaIva);

            PdfPCell celdaIva = new PdfPCell(new Paragraph("$ " + doc.IVA.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), fuenteNegra));
            celdaIva.BorderWidth = 1;
            celdaIva.HorizontalAlignment = 2;
            totales.AddCell(celdaIva);

            // si los impuestos adicionales vienen
            if (doc.imptoReten != null)
            { 

                // agrega Porcentage Impuesto Adicional
                String prcimpadic = String.Empty;
                foreach (var prc in doc.imptoReten)
                {
                    prcimpadic = Convert.ToString(prc.TasaImp);
                

                PdfPCell celdaEtiquetaIla = new PdfPCell(new Paragraph("Imp. Adic. (" + prcimpadic + "%):  ", fuenteNegra));
                celdaEtiquetaIla.BorderWidth = 1;
                celdaEtiquetaIla.HorizontalAlignment = 2;
                totales.AddCell(celdaEtiquetaIla);
                
                // agrega Monto Impuesto Adicional

                String mtoimpadic = String.Empty;


                mtoimpadic = prc.MontoImp.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES"));


                    PdfPCell celdaIla = new PdfPCell(new Paragraph("$ " + mtoimpadic, fuenteNegra));
                celdaIla.BorderWidth = 1;
                celdaIla.HorizontalAlignment = 2;
                totales.AddCell(celdaIla);
                }
            }
            PdfPCell celdaEtiquetaMontoTotal = new PdfPCell(new Paragraph("Monto Total:  ", fuenteBold));
            celdaEtiquetaMontoTotal.BorderWidth = 1;
            celdaEtiquetaMontoTotal.HorizontalAlignment = 2;
            totales.AddCell(celdaEtiquetaMontoTotal);


            PdfPCell celdaMontoTotal = new PdfPCell(new Paragraph("$ " + doc.MntTotal.ToString("N0",CultureInfo.CreateSpecificCulture("es-ES")), fuenteBold));
            celdaMontoTotal.BorderWidth = 1;
            celdaMontoTotal.HorizontalAlignment = 2;
            totales.AddCell(celdaMontoTotal);

            PdfPCell celdaTotales = new PdfPCell(totales);
            celdaTotales.BorderWidth = 1;
           // celdaTotales.MinimumHeight = 20f;
         //   footer.AddCell(celdaTotales);

            // ++++++++++++++++++ tabla recibi conforme +++++++++++++++++++++++++++++++++++++++++++++++++++++

            PdfPTable tablaRecibido = new PdfPTable(1);
            tablaRecibido.WidthPercentage = 100;    

            PdfPCell celdaRecibido0 = new PdfPCell(new Paragraph("NOMBRE: ________________________________", fuenteNegra));
            celdaRecibido0.BorderWidth = 0;
            celdaRecibido0.MinimumHeight = 15;
            celdaRecibido0.HorizontalAlignment = 0;
            tablaRecibido.AddCell(celdaRecibido0);

            PdfPCell celdaRecibido1 = new PdfPCell(new Paragraph("RUT:_______________ FECHA: ____________", fuenteNegra));
            celdaRecibido1.BorderWidth = 0;
            celdaRecibido1.MinimumHeight = 15;
            celdaRecibido1.HorizontalAlignment = 0;
            tablaRecibido.AddCell(celdaRecibido1);

            PdfPCell celdaRecibido2 = new PdfPCell(new Paragraph("RECINTO:___________________ FIRMA: ____________", fuenteNegra));
            celdaRecibido2.BorderWidth = 0;
            celdaRecibido2.MinimumHeight = 15;
            celdaRecibido2.HorizontalAlignment = 0;
            tablaRecibido.AddCell(celdaRecibido2);

            PdfPCell celdaRecibido3 = new PdfPCell(new Paragraph("El acuse de recibo que se declara en este acto, de acuerdo a lo dispuesto en la letra b) del Art. 4º y letra c) del Art. 5º de la ley 19383, acredita la entrega de mercaderia(s) o servicio(s).", fuenteNegra));
            celdaRecibido3.BorderWidth = 0;
            celdaRecibido3.MinimumHeight = 20;
            celdaRecibido3.HorizontalAlignment = 0;
            tablaRecibido.AddCell(celdaRecibido3);

            PdfPCell celdaCedible = new PdfPCell(new Paragraph(tipoCopia, fuenteBold));
            celdaCedible.BorderWidth = 0;
            celdaCedible.MinimumHeight = 10f;
            celdaCedible.HorizontalAlignment = 2;
         

   
          
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            // A = tabla
            PdfPTable tablaAfooter = new PdfPTable(2);
            tablaAfooter.WidthPercentage = 100;

            // D = Tabla
            // esta tabla ya está creada tablaDtimbre

            //B = Celda
            PdfPCell celdaBTimbre = new PdfPCell(tablaDTimbre);
            celdaBTimbre.BorderWidth = 0;
            celdaBTimbre.MinimumHeight = 100;
            celdaBTimbre.HorizontalAlignment = 1;

            // E = Tabla
            PdfPTable tablaETotalyRecibo = new PdfPTable(1);
            tablaDTimbre.WidthPercentage = 100;


            // C = Celda
            PdfPCell celdaCTotalyRecibo = new PdfPCell(tablaETotalyRecibo);
            celdaCTotalyRecibo.BorderWidth = 0;
            celdaCTotalyRecibo.MinimumHeight = 100;
            celdaCTotalyRecibo.HorizontalAlignment = 1;


            // F = Celda
            PdfPCell celdaFTotal = new PdfPCell(totales); // agregar la tabla totales
            celdaFTotal.BorderWidth = 1;
            celdaFTotal.MinimumHeight = 100;
            celdaFTotal.HorizontalAlignment = 1;

            // G = Celda
            PdfPCell celdaGRecibo = new PdfPCell(tablaRecibido); // agregar tabla recibo 
            celdaGRecibo.BorderWidth = 1;
            celdaGRecibo.MinimumHeight = 70;
            celdaGRecibo.HorizontalAlignment = 1;

            // armamos el árbol
            //Llenamos las celdas antes de agregarlas a la tabla footer
            tablaETotalyRecibo.AddCell(celdaFTotal);
            tablaETotalyRecibo.AddCell(new Paragraph(" "));

            if (tipoCopia != " ")
            {
                tablaETotalyRecibo.AddCell(celdaGRecibo);
            }
            
            tablaETotalyRecibo.AddCell(celdaCedible);


            tablaAfooter.AddCell(celdaBTimbre);
            tablaAfooter.AddCell(celdaCTotalyRecibo);

            
            
            pdf.Add(cabecera);
            pdf.Add(contenedorDatosReceptor);
            pdf.Add(new Paragraph(" "));
            pdf.Add(contenedorDetalle);
           // pdf.Add(new Paragraph(" "));
            pdf.Add(referencias);
            pdf.Add(datosReferencias);
            pdf.Add(new Paragraph(" "));
            pdf.Add(tablaAfooter);
            pdf.NewPage();
            pdf.Close();



            Console.WriteLine("Pdf Cerrado!!");

           // System.Diagnostics.Process.Start(@"C:\IatFiles\file\pdf\" + doc.TipoDTE + "_" + doc.Folio + ".pdf");

            /* ProcessStartInfo info = new ProcessStartInfo();
              info.Verb = "print";
              info.FileName = "C://IatFiles/file/pdf/" + doc.TipoDTE + "_" + doc.Folio+".pdf";
              info.CreateNoWindow = true;
              info.WindowStyle = ProcessWindowStyle.Hidden;

              Process p = new Process();
              p.StartInfo = info;
              p.Start();

               p.WaitForInputIdle();
              System.Threading.Thread.Sleep(10000);
              if (false == p.CloseMainWindow())
                  p.Kill();*/

            return pdf;





        }

        // crear un metodo que un Documento

    }
}
