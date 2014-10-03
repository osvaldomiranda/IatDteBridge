using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;



namespace IatDteBridge
{
    class xmlAdmin
    {


        public void doc_to_xmlSii() //recibir parametro de tipo documento
        {
            // Emisor
            // Receptor
            // Firma
            // Timbre
        }

    
        public static void firmarDocumentoXml(ref XmlDocument xmldocument, X509Certificate2 certificado, string referenciaUri)
        {
            ////
            //// Cree el objeto SignedXml donde xmldocument
            //// representa el documento DTE preparado para
            //// ser firmado. Recuerde que debe ser abierto 
            //// con la propiedad PreserveWhiteSpace = true
            SignedXml signedXml = new SignedXml(xmldocument);

            ////
            //// Agregue la clave privada al objeto signedXml
            signedXml.SigningKey = certificado.PrivateKey;

            ////
            //// Recupere el objeto signature desde signedXml
            Signature XMLSignature = signedXml.Signature;

            ////
            //// Cree la refrerencia al documento DTE
            //// recuerde que la referencia tiene el 
            //// formato '#reference'
            //// ejemplo '#DTE001'
            Reference reference = new Reference();
            reference.Uri = referenciaUri;

            ////
            //// Agregue la referencia al objeto signature
            XMLSignature.SignedInfo.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
           // keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            ////
            //// Agregar información del certificado x509
            keyInfo.AddClause(new KeyInfoX509Data(certificado));
            XMLSignature.KeyInfo = keyInfo;

            ////
            //// Calcule la firma y recupere la representacion
            //// de la firma en un objeto xmlElement
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            ////
            //// Inserte la firma en el documento DTE
            xmldocument.DocumentElement.AppendChild(xmldocument.ImportNode(xmlDigitalSignature, true));

        }



        public string timbrar(String nodoDD) 
        {

            return " ";
        }

    }
}
