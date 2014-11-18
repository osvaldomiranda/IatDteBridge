using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;

namespace IatDteBridge
{
    class xmlLibroCompra 
    {

        public String libroCompra_to_xml(LibroCompra libro)
        {

            String cabeceraLibro =
             "<LibroCompraVenta xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte LibroCV_v10.xsd\" version=\"1.0\" xmlns=\"http://www.sii.cl/SiiDte\">\n"
            + "<EnvioLibro ID=\"IECV201406\">\n"
              + "<Caratula>\n"
                + "<RutEmisorLibro>" + libro.RutEmisorLibro + "</RutEmisorLibro>\n"
                + "<RutEnvia>"+libro.RutEnvia+"</RutEnvia>\n"
                + "<PeriodoTributario>"+libro.PeriodoTributario+"</PeriodoTributario>\n"
                + "<FchResol>"+libro.FchResol+"</FchResol>\n"
                + "<NroResol>"+libro.NroResol+"</NroResol>\n"
                + "<TipoOperacion>"+libro.TipoOperacion+"</TipoOperacion>\n"
                + "<TipoLibro>"+libro.TipoLibro+"</TipoLibro>\n"
                + "<TipoEnvio>"+libro.TipoEnvio+"</TipoEnvio>\n"
              + "</Caratula>\n";


            String resumen = "<ResumenPeriodo>\n";

            String TotalesP = String.Empty;

            foreach(var total in libro.TotalesPeriodo)
            {
                String totopivausocomun = "<TotOpIVAUsoComun>" + total.TotOpIVAUsoComun + "</TotOpIVAUsoComun>\n";
                if (total.TotOpIVAUsoComun == 0)
                    totopivausocomun = "";
               
                String totivausocomun = "<TotIVAUsoComun>" + total.TotIVAUsoComun + "</TotIVAUsoComun>\n";
                if (total.TotIVAUsoComun == 0)
                    totivausocomun = "";

                String fctprop = "<FctProp>" + total.FctProp + "</FctProp>\n";
                if (total.FctProp == 0)
                    fctprop = "";
                String totcredivausocomun = "<TotCredIVAUsoComun>" + total.TotCredIVAUsoComun + "</TotCredIVAUsoComun>\n";
                if (total.TotCredIVAUsoComun == 0)
                    totcredivausocomun = "";

                String b=String.Empty;
                String c=String.Empty;
                String d=String.Empty;
                String a = "<TotalesPeriodo>\n" +
                  "<TpoDoc>" + total.TpoDoc + "</TpoDoc>\n" +
                  "<TotDoc>" + total.TotDoc + "</TotDoc>\n" +
                  "<TotMntExe>" + total.TotMntExe + "</TotMntExe>\n" +
                  "<TotMntNeto>" + total.TotMntNeto + "</TotMntNeto>\n" +
                  "<TotMntIVA>" + total.TotMntIVA + "</TotMntIVA>\n" +
                  totopivausocomun +
                  totivausocomun +                  
                  fctprop +
                  totcredivausocomun;

              if (total.TotIVANoRec == null)
              {
                  b = "";
              }
              else
              {
                  foreach (var ivano in total.TotIVANoRec)
                  {
                      b = "<TotIVANoRec>\n" +
                      "<CodIVANoRec>" + ivano.CodIVANoRec + "</CodIVANoRec>\n" +
                      "<TotOpIVANoRec>" + ivano.TotOpIVANoRec + "</TotOpIVANoRec>\n" +
                      "<TotMntIVANoRec>" + ivano.TotMntIVANoRec + "</TotMntIVANoRec>\n" +
                      "</TotIVANoRec>\n";
                  }
              }

              if (total.TotOtrosImp == null)
              {
                  c = "";
              }
              else
              {
                  foreach (var otrosimp in total.TotOtrosImp)
                  {
                      c = "<TotOtrosImp>\n" +
                        "<CodImp>" + otrosimp.CodImp + "</CodImp>\n" +
                        "<TotMntImp>" + otrosimp.TotMntImp + "</TotMntImp>\n" +
                      "</TotOtrosImp>\n";
                  }
              }

                TotalesP += a+b+c+d+
            //    "<TotIVAFueraPlazo>" + total.TotIVAFueraPlazo + "</TotIVAFueraPlazo>\n" +
                "<TotMntTotal>"+total.TotMntTotal+"</TotMntTotal>\n" +
              "</TotalesPeriodo>\n";
             }

            String finResumen = "</ResumenPeriodo>\n";


            String detalle = String.Empty;
            String detIvaNo = String.Empty;
            String detOtrosImp = String.Empty;
            String detalleaux = String.Empty;

            foreach (var det in libro.Detalle)
            {   
                if(detIvaNo == null)
                    detIvaNo ="";
                detalle = "<Detalle>\n" +
                  "<TpoDoc>" + det.TpoDoc + "</TpoDoc>\n" +
                  "<NroDoc>" + det.NroDoc + "</NroDoc>\n" +
                  "<TpoImp>" + det.TpoImp + "</TpoImp>\n" +
                  "<TasaImp>" + det.TasaImp + "</TasaImp>\n" +
                  "<FchDoc>" + det.FchDoc + "</FchDoc>\n" +
                  "<RUTDoc>" + det.RUTDoc + "</RUTDoc>\n" +
                  "<RznSoc>" + det.RznSoc + "</RznSoc>\n" +
                  "<MntExe>" + det.MntExe + "</MntExe>\n" +
                  "<MntNeto>" + det.MntNeto + "</MntNeto>\n"+ 
                  "<MntIVA>" + det.MntIVA+ "</MntIVA>\n";
                 if (det.IVANoRec == null)
                 {
                     detIvaNo = "";
                 }
                 else
                 {
                     foreach (var iva in det.IVANoRec)
                     {
                         detIvaNo = "<IVANoRec>\n" +
                          "<CodIVANoRec>" + iva.CodIVANoRec + "</CodIVANoRec>\n" +
                          "<MntIVANoRec>" + iva.MntIVANoRec + "</MntIVANoRec>\n" +
                        "</IVANoRec>\n";
                     }
                 }

                 String ivausocomun = "<IVAUsoComun>" + det.IVAUsoComun + "</IVAUsoComun>\n";
                 if (det.IVAUsoComun == 0)
                     ivausocomun = "";

                 if (det.OtrosImp == null)
                 {
                     detOtrosImp = "";
                 }
                 else
                 {

                     foreach (var otros in det.OtrosImp)
                     {
                         detOtrosImp = "<OtrosImp>\n" +
                         "<CodImp>" + otros.CodImp + "</CodImp>\n" +
                         "<TasaImp>" + otros.TasaImp + "</TasaImp>\n" +
                         "<MntImp>" + otros.MntImp + "</MntImp>\n" +
                       "</OtrosImp>\n";
                     }
                 }
                     detalleaux += detalle + detIvaNo + ivausocomun+ detOtrosImp +
                     "<MntTotal>" + det.MntTotal + "</MntTotal>\n" +
                     "</Detalle>\n";
                 
            }

            DateTime thisDay = DateTime.Now;
            String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

            String finLibro =
<<<<<<< HEAD
              "<TmstFirma>"+fch+"</TmstFirma>" +
            "</EnvioLibro>" +
            "</LibroCompraVenta>";
=======
              "<TmstFirma>2014-06-20T09:22:12</TmstFirma>\n" +
            "</EnvioLibro>\n" +
            "</LibroCompraVenta>\n";
>>>>>>> Mauricio

            String LibroCom = cabeceraLibro + resumen  + TotalesP +  finResumen + detalleaux + finLibro;

            X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");

            String signLibro = firmarLibro(LibroCom, cert);

            return signLibro;
        }

        public string firmarLibro(string documento, X509Certificate2 certificado)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(documento);

            SignedXml signedXml = new SignedXml(doc);

            signedXml.SigningKey = certificado.PrivateKey;

            Signature XMLSignature = signedXml.Signature;

            Reference reference = new Reference("");

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            XMLSignature.SignedInfo.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            XMLSignature.KeyInfo = keyInfo;

            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();

            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }

            return doc.InnerXml;

        }
    }
}
