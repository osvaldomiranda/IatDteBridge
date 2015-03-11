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
    class xmlLiborVenta
    {
        public String do_libroVentas(LibroVenta libro)
        {
            Documento doc = new Documento();
            String cabeceraLibro =
            "<LibroCompraVenta xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte LibroCV_v10.xsd\" version=\"1.0\" xmlns=\"http://www.sii.cl/SiiDte\">\n"
           + "<EnvioLibro ID=\"ID201204\">\n"
             + "<Caratula>\n"
               + "<RutEmisorLibro>" + libro.RutEmisorLibro + "</RutEmisorLibro>\n"
               + "<RutEnvia>" + libro.RutEnvia + "</RutEnvia>\n"
               + "<PeriodoTributario>" + libro.PeriodoTributario + "</PeriodoTributario>\n"
               + "<FchResol>" + libro.FchResol + "</FchResol>\n"
               + "<NroResol>" + libro.NroResol + "</NroResol>\n"
               + "<TipoOperacion>" + libro.TipoOperacion + "</TipoOperacion>\n"
               + "<TipoLibro>" + libro.TipoLibro + "</TipoLibro>\n"
               + "<TipoEnvio>" + libro.TipoEnvio + "</TipoEnvio>\n"
              // + "<FolioNotificacion>" + libro.FolioNotificacion + "</FolioNotificacion>\n"
               + "</Caratula>\n";


            String resumen = "<ResumenPeriodo>\n";

            String TotalesP = String.Empty;

            foreach (var total in libro.TotalesPeriodo)
            {

                String b = String.Empty;
                String c = String.Empty;
                String d = String.Empty;
                String a = "<TotalesPeriodo>\n" +
                "<TpoDoc>" + total.TpoDoc + "</TpoDoc>\n" +
                "<TotDoc>" + total.TotDoc + "</TotDoc>\n" +
                "<TotMntExe>" + total.TotMntExe + "</TotMntExe>\n" +
                "<TotMntNeto>" + total.TotMntNeto + "</TotMntNeto>\n" +
                "<TotMntIVA>" + total.TotMntIVA + "</TotMntIVA>\n";


                foreach (var otrosimp in total.TotOtrosImp)
                {
                    if (otrosimp.CodImp == 0)
                    {
                        c = "";
                    }
                    else
                    {
                        c = "<TotOtrosImp>\n" +
                          "<CodImp>" + otrosimp.CodImp + "</CodImp>\n" +
                          "<TotMntImp>" + otrosimp.TotMntImp + "</TotMntImp>\n" +
                        "</TotOtrosImp>\n";
                    }
                }

                // si montoivafueraplazo es nulo o cero no se agrega en el xml
                String totIVAFueraPlazo = String.Empty;
                if (total.TotIVAFueraPlazo == 0)
                {
                    totIVAFueraPlazo = "";
                }
                else {

                    totIVAFueraPlazo = "<TotIVAFueraPlazo>" + total.TotIVAFueraPlazo + "</TotIVAFueraPlazo>\n";
                }


                TotalesP += a + b + c + d +
                 totIVAFueraPlazo +
                "<TotMntTotal>" + total.TotMntTotal +"</TotMntTotal>\n" +
              "</TotalesPeriodo>\n";
            }

            String finResumen = "</ResumenPeriodo>\n";


            String detalle = String.Empty;
            String detIvaNo = String.Empty;
            String detOtrosImp = String.Empty;
            String mnttotal = String.Empty;

            if (libro.Detalle == null)
            {
                detalle = "";
                detOtrosImp = "";
            }
            else
            {
                foreach (var det in libro.Detalle)
                {
                    detalle = "<Detalle>\n" +
                     "<TpoDoc>" + det.TpoDoc + "</TpoDoc>\n" +
                     "<NroDoc>" + det.NroDoc + "</NroDoc>\n" +
                     "<TasaImp>" + det.TasaImp + "</TasaImp>\n" +
                     "<FchDoc>" + det.FchDoc + "</FchDoc>\n" +
                     "<RUTDoc>" + det.RUTDoc + "</RUTDoc>\n" +
                     "<RznSoc>" + det.RznSoc + "</RznSoc>\n" +
                     "<MntExe>" + det.MntExe + "</MntExe>\n" +
                     "<MntNeto>" + det.MntNeto + "</MntNeto>\n"+
                     "<MntIVA>" + det.MntIVA + "</MntIVA>\n";

                    foreach (var otros in det.OtrosImp)
                    {
                        detOtrosImp = "<OtrosImp>\n" +
                        "<CodImp>" + otros.CodImp + "</CodImp>\n" +
                        "<TasaImp>" + otros.TasaImp + "</TasaImp>\n" +
                        "<MntImp>" + otros.MntImp + "</MntImp>\n" +
                      "</OtrosImp>\n";
                    }

                   mnttotal =  "<MntTotal>" + det.MntTotal + "</MntTotal>\n";
                }

                detalle += detIvaNo + detOtrosImp +
                mnttotal +
                "</Detalle>\n";
            }

            DateTime thisDay = DateTime.Now;
            String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

            String finLibro =
              "<TmstFirma>" + fch + "</TmstFirma>\n" +
            "</EnvioLibro>\n" +
            "</LibroCompraVenta>\n";

            String LibroCom = cabeceraLibro + resumen + TotalesP + finResumen + detalle + finLibro;

            X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");

            String signLibro = firmarLibroVta(LibroCom, cert);

            return signLibro;
        }

        public string firmarLibroVta(string documento, X509Certificate2 certificado)
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
