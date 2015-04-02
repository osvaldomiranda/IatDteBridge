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
            Documento doc = new Documento();
            if (libro != null)
            {
                String cabeceraLibro =
                " <LibroGuia xmlns=\"http://www.sii.cl/SiiDte\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" version=\"1.0\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte LibroGuia_v10.xsd\">"
               + "<EnvioLibro ID=\"IECV201312\">"
                 + "<Caratula>"
                   + "<RutEmisorLibro>" + libro.RutEmisorLibro + "</RutEmisorLibro>"
                   + "<RutEnvia>" + libro.RutEnvia + "</RutEnvia>"
                   + "<PeriodoTributario>" + libro.PeriodoTributario + "</PeriodoTributario>"
                   + "<FchResol>" + libro.FchResol + "</FchResol>"
                   + "<NroResol>" + libro.NroResol + "</NroResol>"
                   + "<TipoLibro>ESPECIAL</TipoLibro>"
                   + "<TipoEnvio>TOTAL</TipoEnvio>"
                   + "<FolioNotificacion>1</FolioNotificacion>"
                   + "</Caratula>";


                String resumen = "<ResumenPeriodo>";


                foreach (var totPer in libro.ResumenPeriodo)
                {
                    resumen += "<TotFolAnulado>" + totPer.TotFolAnulado + "</TotFolAnulado>" +
                    "<TotGuiaAnulada>" + totPer.TotGuiaAnulada + "</TotGuiaAnulada>" +
                    "<TotGuiaVenta>" + totPer.TotGuiaVenta + "</TotGuiaVenta>" +
                    "<TotMntGuiaVta>" + totPer.TotMntGuiaVta + "</TotMntGuiaVta>" +
                    "<TotMntModificado>" + totPer.TotMntModificado + "</TotMntModificado>";

                    foreach (var tras in totPer.TotTraslado)
                    {
                        resumen += "<TotTraslado>" +
                        "<TpoTraslado>" + tras.TpoTraslado + "</TpoTraslado>" +
                        "<CantGuia>" + tras.CantGuia + "</CantGuia>" +
                        "<MntGuia>" + tras.MntGuia + "</MntGuia>" +
                        "</TotTraslado>";
                    }
                }

                String finResumen = "</ResumenPeriodo>";

                String detall = String.Empty;

                foreach (var det in libro.Detalle)
                {
                    String anulado = "<Anulado>" + det.Anulado + "</Anulado>";
                    if (det.Anulado == 0)
                        anulado = "";

                    detall += "<Detalle>" +
                    "<Folio>" + det.Folio + "</Folio>" +
                     anulado +
                    "<Operacion>" + det.Operacion + "</Operacion>" +
                    "<TpoOper>" + det.TpoOper + "</TpoOper>" +
                    "<FchDoc>" + det.FchDoc + "</FchDoc>" +
                    "<RUTDoc>" + det.RUTDoc + "</RUTDoc>" +
                    "<RznSoc>" + det.RznSoc + "</RznSoc>" +
                    "<MntNeto>" + det.MntNeto + "</MntNeto>" +
                    "<TasaImp>" + det.TasaImp + "</TasaImp>" +
                    "<IVA>" + det.IVA + "</IVA>" +
                    "<MntTotal>" + det.MntTotal + "</MntTotal>" +
                    "<MntModificado>" + det.MntModificado + "</MntModificado>" +
                    "</Detalle>";
                }

                DateTime thisDay = DateTime.Now;
                String fch = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", thisDay);

                String finLibro =
                "<TmstFirma>" + fch + "</TmstFirma>" +
                "</EnvioLibro>" +
                "</LibroGuia>";

                String libroGuia = cabeceraLibro + resumen + finResumen + detall + finLibro;


                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("YAMAHL NASER GONZALEZ");

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
