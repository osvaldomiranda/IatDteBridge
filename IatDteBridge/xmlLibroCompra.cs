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
             "<LibroCompraVenta xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte LibroCV_v10.xsd\" version=\"1.0\" xmlns=\"http://www.sii.cl/SiiDte\">"
            + "<EnvioLibro ID=\"IECV201406\">"
              + "<Caratula>"
                + "<RutEmisorLibro>" + libro.RutEmisorLibro + "</RutEmisorLibro>"
                + "<RutEnvia>"+libro.RutEnvia+"</RutEnvia>"
                + "<PeriodoTributario>"+libro.PeriodoTributario+"</PeriodoTributario>"
                + "<FchResol>"+libro.FchResol+"</FchResol>"
                + "<NroResol>"+libro.NroResol+"</NroResol>"
                + "<TipoOperacion>COMPRA</TipoOperacion>"
                + "<TipoLibro>MENSUAL</TipoLibro>"
                + "<TipoEnvio>TOTAL</TipoEnvio>"
              + "</Caratula>";


            String resumen = "<ResumenPeriodo>";

            String TotalesP = String.Empty;

            foreach(var total in libro.TotalesPeriodo)
            {

                String b=String.Empty;
                String c=String.Empty;
                String d=String.Empty;
              String a = "<TotalesPeriodo>" +
                "<TpoDoc>"+total.TotDoc+"</TpoDoc>" +
                "<TotDoc>"+total.TotDoc+"</TotDoc>" +
                "<TotMntExe>"+ total.TotMntExe +"</TotMntExe>" +
                "<TotMntNeto>"+ total.TotMntNeto+"</TotMntNeto>" +
                "<TotMntIVA>"+ total.TotMntIVA+"</TotMntIVA>";

                foreach(var ivano in total.TotIVANoRec){
                    b = "<TotIVANoRec>" +
                    "<CodIVANoRec>"+ivano.CodIVANoRec+"</CodIVANoRec>" +
                    "<TotOpIVANoRec>"+ivano.TotOpIVANoRec+"</TotOpIVANoRec>" +
                    "<TotMntIVANoRec>"+ivano.TotMntIVANoRec+"</TotMntIVANoRec>" +
                    "</TotIVANoRec>" ;
                }

                foreach(var otrosimp in total.TotOtrosImp){
                    c = "<TotOtrosImp>" +
                      "<CodImp>" + otrosimp.CodImp + "</CodImp>" +
                      "<TotMntImp>" + otrosimp.TotMntImp + "</TotMntImp>" +
                    "</TotOtrosImp>";
                }

                TotalesP = a+b+c+d+
                "<TotIVAFueraPlazo>" + total.TotIVAFueraPlazo + "</TotIVAFueraPlazo>" +
                "<TotMntTotal></TotMntTotal>" +
              "</TotalesPeriodo>";
             }

            String finResumen = "</ResumenPeriodo>";


            String detalle = String.Empty;
            String detIvaNo = String.Empty;
            String detOtrosImp = String.Empty;

            foreach (var det in libro.Detalle)
            {
                 detalle = "<Detalle>" +
                  "<TpoDoc>"+det.TpoDoc+"</TpoDoc>" +
                  "<NroDoc>"+det.NroDoc+"</NroDoc>" +
                  "<TpoImp>"+det.TpoImp+"</TpoImp>" +
                  "<TasaImp>"+det.TasaImp+"</TasaImp>" +
                  "<FchDoc>"+det.FchDoc+"</FchDoc>" +
                  "<RUTDoc>"+det.RUTDoc+"</RUTDoc>" +
                  "<RznSoc>"+det.RznSoc+"</RznSoc>" +
                  "<MntExe>"+det.MntExe+"</MntExe>" +
                  "<MntNeto>"+det.MntNeto+"</MntNeto>" ;

                  foreach(var iva in det.IVANoRec){
                   detIvaNo = "<IVANoRec>" +
                    "<CodIVANoRec>"+iva.CodIVANoRec+"</CodIVANoRec>" +
                    "<MntIVANoRec>"+iva.MntIVANoRec+"</MntIVANoRec>" +
                  "</IVANoRec>" ;
                  }

                  foreach(var otros in det.OtrosImp){
                    detOtrosImp = "<OtrosImp>" +
                    "<CodImp>"+otros.CodImp+"</CodImp>" +
                    "<TasaImp>"+otros.TasaImp+"</TasaImp>" +
                    "<MntImp>"+otros.MntImp+"</MntImp>" +
                  "</OtrosImp>" ;
                  }

                  detalle+= detIvaNo+ detOtrosImp +
                  "<MntTotal>"+det.MntTotal+"</MntTotal>" +
                  "</Detalle>";
            }

            DateTime thisDay = DateTime.Now;
            String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

            String finLibro =
              "<TmstFirma>"+fch+"</TmstFirma>" +
            "</EnvioLibro>" +
            "</LibroCompraVenta>";

            String LibroCom = cabeceraLibro + resumen + finResumen + TotalesP + detalle + finLibro;

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
