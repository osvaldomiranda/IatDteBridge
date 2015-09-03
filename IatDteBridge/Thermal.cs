using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Globalization;
namespace IatDteBridge
{
    class Thermal
    {
        public Documento doc {set; get;}
        public String dd { set; get; }

        public void OpenThermal(object sender, PrintPageEventArgs ev)
        {
            String tipoCopia = String.Empty;
            String nombreDocumento = String.Empty;
            Timbre timbre1 = new Timbre();
            timbre1.CreaTimbre(this.dd);
            Rectangle rectangulo = new Rectangle(10, 1, 260, 100);
            Pen p = new Pen(Color.Black, 5);
            ev.Graphics.DrawRectangle(p, rectangulo);
            StringFormat alignCenter = new StringFormat();
            alignCenter.Alignment = StringAlignment.Center;

            StringFormat alignRight = new StringFormat();
            alignRight.Alignment = StringAlignment.Near;

            StringFormat alignLeft = new StringFormat();
            alignLeft.Alignment = StringAlignment.Far;

            ev.Graphics.DrawRectangle(p, rectangulo);

            switch (this.doc.TipoDTE)
            {
                case 30: nombreDocumento = "FACTURA";
                    break;
                case 33: nombreDocumento = "FACTURA ELECTRÓNICA";
                    break;
                case 34: nombreDocumento = "FACTURA NO AFECTA O EXENTA ELECTRÓNICA";
                    break;
                case 61: nombreDocumento = "NOTA DE CRÉDITO ELECTRÓNICA";
                    break;
                case 56: nombreDocumento = "NOTA DE DÉBITO ELECTRÓNICA";
                    break;
                case 52: nombreDocumento = "GUÍA DE DESPACHO ELECTRÓNICA";
                    break;

            }
            // Agrega separadores al rut

            String rutemisor = this.doc.RUTEmisor;
            rutemisor = rutemisor.Insert(2, ".");
            rutemisor = rutemisor.Insert(6, ".");

            ev.Graphics.DrawString("R.U.T.: " + rutemisor, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Rectangle(10, 5, 260, 20), alignCenter);
            ev.Graphics.DrawString(nombreDocumento, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Rectangle(10, 30, 260, 50), alignCenter);
            ev.Graphics.DrawString("Nº "+ this.doc.Folio, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Rectangle(10, 60, 260, 100), alignCenter);
            ev.Graphics.DrawString(this.doc.DirRegionalSII, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, new Rectangle(10, 105, 260, 20), alignCenter);
            ev.Graphics.DrawString(this.doc.RznSoc, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Rectangle(10, 130, 260, 20), alignCenter);
            ev.Graphics.DrawString(this.doc.GiroEmis, new Font("Arial", 8, FontStyle.Italic), Brushes.Black, new Rectangle(10, 150, 260, 40), alignCenter);
            // Datos del Emisor
            int lineaCabecera = 180;
            ev.Graphics.DrawString("FONOS: " + this.doc.Telefono, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 40));
            lineaCabecera += 13;
            ev.Graphics.DrawString("CASA MATRIZ: \n" + this.doc.DirMatriz, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 40));
            lineaCabecera += 39; //TODO esta linea cambia segun las sucursales de la empresa
            // Agrego las sucursales
            string sucu = string.Empty;
            string[] sucuremisor = doc.SucurEmisor.Split(new char[] { ';' });
            foreach (string s in sucuremisor)
            {
                Console.WriteLine(s);
                sucu += s + "\n";
            }
            ev.Graphics.DrawString("SUCURSALES: \n" + sucu, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 280, 60));
            lineaCabecera += 3 + 65;// TODO esta linea cambia segun las sucursales de la empresa
            // convierte fecha
            DateTime fechaemis = Convert.ToDateTime(this.doc.FchEmis);
            int dia = fechaemis.Day;
            string mesletra = fechaemis.ToString("MMMMM");
            int ano = fechaemis.Year;
            // Datos del Receptor
            Rectangle recReceptor = new Rectangle(3, lineaCabecera - 1, 270, 133);
            Pen p2 = new Pen(Color.Black, 1);
            ev.Graphics.DrawRectangle(p2, recReceptor);
            ev.Graphics.DrawString("Fecha: Santiago, " + dia + " de " + mesletra + " de " + ano, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Señor(es): " + this.doc.RznSocRecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Dirección: " + doc.DirRecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            // Agrega separadores al rut
            String rutrecep = this.doc.RUTRecep;
            rutrecep = rutrecep.Insert(2, ".");
            rutrecep = rutrecep.Insert(6, ".");
            ev.Graphics.DrawString("R.U.T.: " + rutrecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            //controla el largo de Giro 
            String giroRecep = String.Empty;
            if (doc.GiroRecep.Length <= 40)
            {
                giroRecep = doc.GiroRecep;
            }
            else
            {
                giroRecep = doc.GiroRecep.Substring(0, 35);
            }
            ev.Graphics.DrawString("Giro: " + giroRecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Comuna: " + doc.CmnaRecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Teléfono: " + doc.TelRecep, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Vendedor: " + doc.CdgVendedor + " - " + doc.NomVendedor, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(5, lineaCabecera, 300, 60));
            lineaCabecera += 5+39;
            // Titulos de columnas de detalle
            ev.Graphics.DrawString("Cantidad", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, lineaCabecera, 280, 15), alignRight);
            ev.Graphics.DrawString("Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, lineaCabecera, 280, 15), alignLeft);
            ev.Graphics.DrawString("Código", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(80, lineaCabecera, 280, 15));
            ev.Graphics.DrawString("Precio", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(160, lineaCabecera, 280, 15));
            lineaCabecera += 13;
            ev.Graphics.DrawString("Descripción", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(10, lineaCabecera, 280, 15));
            ev.Graphics.DrawLine(p2, 0, lineaCabecera +15, 300, lineaCabecera+15); // linea de separacion
            lineaCabecera += 3+13;

            //--------------------------------------------- DETALLE ------------------------------------------------------------------------------
            //Captura el codigo de referencia
            String codigoreferencia = String.Empty;
            foreach (var codref in doc.Referencia)
            {
                codigoreferencia = codref.CodRef.ToString();
            }

            int next = lineaCabecera; // 30
            int linea = lineaCabecera+15; //15
            String nmbitem = String.Empty;
            foreach (var det in doc.detalle)
            {
                ev.Graphics.DrawString(Convert.ToString(det.QtyItem)+" X", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, next, 280, 15), alignRight);
                if (doc.PrnMtoNeto == "True")
                {
                    ev.Graphics.DrawString("$ "+det.MontoItem.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, next, 280, 15), alignLeft);
                }
                else
                {
                    ev.Graphics.DrawString("$ " + det.MontoBruItem.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, next, 280, 15), alignLeft);
                }
                ev.Graphics.DrawString(Convert.ToString(det.VlrCodigo), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(80, next, 280, 15));
                if (doc.PrnMtoNeto == "True")
                {
                    ev.Graphics.DrawString("$ " + det.PrcItem.ToString("N4", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(160, next, 280, 15));
                }
                else
                {
                    ev.Graphics.DrawString("$ " + det.PrcBruItem.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(160, next, 280, 15));
                }
                //controla el largo de nombre item
                if (det.NmbItem.Length <= 40)
                {
                    nmbitem = det.NmbItem;
                }
                else
                {
                    nmbitem = det.NmbItem.Substring(0, 30);
                }
                if (codigoreferencia == "2")
                {
                    nmbitem = det.NmbItem;
                }
                ev.Graphics.DrawString(Convert.ToString(nmbitem), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Rectangle(10, linea, 280, 15));

                next += 30;
                linea += 30;
            }
            
            //-------------------------------------------Referencias---------------------------------------------------

            //-------------------------------------------TOTALES---------------------------------------------------

            int total = linea + 50;
            ev.Graphics.DrawString("Neto:", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(150, linea, 280, 15));
            ev.Graphics.DrawString("$ " + doc.MntNeto.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, linea, 280, 15), alignLeft);
            linea += 15;
            ev.Graphics.DrawString("I.V.A:", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(150, linea, 280, 15));
            ev.Graphics.DrawString("$ " + doc.IVA.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, linea, 280, 15), alignLeft);
            linea += 15;
            ev.Graphics.DrawString("Total:", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(150, linea, 280, 15));
            ev.Graphics.DrawString("$ " + doc.MntTotal.ToString("N0", CultureInfo.CreateSpecificCulture("es-ES")), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, linea, 280, 15), alignLeft);
            linea += 30;
            //Timbre
            ev.Graphics.DrawImage(Image.FromFile(@"Timbre.jpg"), new Rectangle(0, linea, 275, 123));
            linea += 130;
            ev.Graphics.DrawString("Timbre Electronico S.I.I.", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Rectangle(0, linea, 280, 15), alignCenter);

        }


        	
    }
}
