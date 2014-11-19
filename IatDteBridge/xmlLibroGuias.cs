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
    class xmlLibroGuias
    {
        public String do_libroGuias(LibroGuias libro)
        {

            if (libro != null)
            {
                String cabeceraLibro =
                " <LibroGuia xmlns=\"http://www.sii.cl/SiiDte\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" version=\"1.0\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte LibroGuia_v10.xsd\">\n"
               + "<EnvioLibro ID=\"IECV201312\">\n"
                 + "<Caratula>\n"
                   + "<RutEmisorLibro>" + libro.RutEmisorLibro + "</RutEmisorLibro>\n"
                   + "<RutEnvia>" + libro.RutEnvia + "</RutEnvia>\n"
                   + "<PeriodoTributario>" + libro.PeriodoTributario + "</PeriodoTributario>\n"
                   + "<FchResol>" + libro.FchResol + "</FchResol>\n"
                   + "<NroResol>" + libro.NroResol + "</NroResol>\n"
                   + "<TipoLibro>ESPECIAL</TipoLibro>\n"
                   + "<TipoEnvio>TOTAL</TipoEnvio>\n"
                   + "<FolioNotificacion>1</FolioNotificacion>\n"
                   + "</Caratula>\n";


                String resumen = "<ResumenPeriodo>\n";


                foreach (var totPer in libro.ResumenPeriodo)
                {
                    resumen += "<TotFolAnulado>" + totPer.TotFolAnulado + "</TotFolAnulado>\n" +
                    "<TotGuiaAnulada>" + totPer.TotGuiaAnulada + "</TotGuiaAnulada>\n" +
                    "<TotGuiaVenta>" + totPer.TotGuiaVenta + "</TotGuiaVenta>\n" +
                    "<TotMntGuiaVta>" + totPer.TotMntGuiaVta + "</TotMntGuiaVta>\n" +
                    "<TotMntModificado>" + totPer.TotMntModificado + "</TotMntModificado>\n";

                    foreach (var tras in totPer.TotTraslado)
                    {
                        resumen += "<TotTraslado>\n" +
                        "<TpoTraslado>" + tras.TpoTraslado + "</TpoTraslado>\n" +
                        "<CantGuia>" + tras.CantGuia + "</CantGuia>\n" +
                        "<MntGuia>" + tras.MntGuia + "</MntGuia>\n" +
                        "</TotTraslado>\n";
                    }
                }

                String finResumen = "</ResumenPeriodo>\n";

                String detall = String.Empty;

                foreach (var det in libro.Detalle)
                {
                    detall += "<Detalle>\n" +
                    "<Folio>" + det.Folio + "</Folio>\n" +
                    "<Operacion>" + det.Operacion + "</Operacion>\n" +
                    "<TpoOper>" + det.TpoOper + "</TpoOper>\n" +
                    "<FchDoc>" + det.FchDoc + "</FchDoc>\n" +
                    "<RUTDoc>" + det.RUTDoc + "</RUTDoc>\n" +
                    "<RznSoc>" + det.RznSoc + "</RznSoc>\n" +
                    "<MntNeto>" + det.MntNeto + "</MntNeto>\n" +
                    "<TasaImp>" + det.TasaImp + "</TasaImp>\n" +
                    "<IVA>" + det.IVA + "</IVA>\n" +
                    "<MntTotal>" + det.MntTotal + "</MntTotal>\n" +
                    "<MntModificado>" + det.MntModificado + "</MntModificado>\n" +
                    "</Detalle>\n";
                }

                DateTime thisDay = DateTime.Now;
                String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

                String finLibro =
                "<TmstFirma>" + fch + "</TmstFirma>\n" +
                "</EnvioLibro>\n" +
                "</LibroGuia>";

                String libroGuia = cabeceraLibro + resumen + finResumen + detall + finLibro;


                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");

                String signLibro = firmarLibroGuias(libroGuia, cert);

                return signLibro;
            }
            else
            {
                return null;
            }
        }

        public string firmarLibroGuias(string documento, X509Certificate2 certificado)
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
