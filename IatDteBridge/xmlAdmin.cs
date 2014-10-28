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
    class xmlAdmin
    {

        public String doc_to_xmlSii(Documento doc) 
        {

            String dte = "<DTE version=\"1.0\"> \n" +
                         "<Documento ID=\"F" + doc.Folio + "T" + doc.TipoDTE + "\"> \n";

            String encabezado = "<Encabezado> \n" +
                "<IdDoc> \n" +
                    "<TipoDTE>" + doc.TipoDTE + "</TipoDTE> \n" +
                    "<Folio>" + doc.Folio + "</Folio> \n" +
                    "<FchEmis>" + doc.FchEmis + "</FchEmis> \n" +
                "</IdDoc> \n";

            String emisor = "<Emisor> \n" +
                    "<RUTEmisor>" + doc.RUTEmisor + "</RUTEmisor> \n" +
                    "<RznSoc>" + doc.RznSoc + "</RznSoc> \n" +
                    "<GiroEmis>" + doc.GiroEmis + "</GiroEmis> \n" +
                    "<Acteco>" + doc.Acteco + "</Acteco> \n" +
                    "<CdgSIISucur>" + doc.CdgSIISucur + "</CdgSIISucur> \n" +
                    "<DirOrigen>" + doc.DirOrigen + "</DirOrigen> \n" +
                    "<CmnaOrigen>" + doc.CmnaOrigen + "</CmnaOrigen> \n" +
                    "<CiudadOrigen>" + doc.CiudadOrigen + "</CiudadOrigen> \n" +
                "</Emisor> \n";

            String receptor = "<Receptor> \n" +
                    "<RUTRecep>" + doc.RUTRecep + "</RUTRecep> \n" +
                    "<RznSocRecep>" + doc.RznSocRecep + "</RznSocRecep> \n" +
                    "<GiroRecep>" + doc.GiroRecep + "</GiroRecep> \n" +
                    "<DirRecep>" + doc.DirRecep + "</DirRecep> \n" +
                    "<CmnaRecep>" + doc.CmnaRecep + "</CmnaRecep> \n" +
                    "<CiudadRecep>" + doc.CiudadRecep + "</CiudadRecep> \n" +
                "</Receptor> \n";

            String totales = "<Totales> \n" +
                    "<MntNeto>" + doc.MntNeto + "</MntNeto> \n" +
                    "<TasaIVA>" + doc.TasaIVA + "</TasaIVA> \n" +
                    "<IVA>" + doc.IVA + "</IVA> \n" +
                    "<MntTotal>" + doc.MntTotal + "</MntTotal> \n" +
                 "</Totales> \n";
            String finencabezado = "</Encabezado> \n";

            //arma encabezado en documento
            String documento = dte + encabezado + emisor + receptor + totales + finencabezado;


            // for para crear detalles y agregarlos al documento
            String detalle;
            String firstNmbItem = String.Empty;
            int i = 0;
            foreach (var det in doc.detalle)
            {

                detalle = "<Detalle> \n" +
                "<NroLinDet>" + det.NroLinDet + "</NroLinDet> \n" +
                "<CdgItem> \n" +
                "  <TpoCodigo>" + det.TpoCodigo + "</TpoCodigo> \n" +
                "  <VlrCodigo>" + det.VlrCodigo + "</VlrCodigo> \n" +
                "</CdgItem> \n" +
                "<NmbItem>" + det.NmbItem + "</NmbItem> \n" +
                "<DscItem>" + det.DscItem+"</DscItem> \n" +
                "<QtyItem>" + det.QtyItem + "</QtyItem> \n" +
                "<PrcItem>" + det.PrcItem + "</PrcItem> \n" +
                "<MontoItem>" + det.MontoItem + "</MontoItem> \n" +
                "</Detalle> \n";

                documento = documento + detalle;
                if (i == 0) firstNmbItem = det.NmbItem; 
                i++;
            }


            // for para crear detalles y agregarlos al documento
            String referencia;
          
            foreach (var refe in doc.Referencia)
            {

                referencia = "<Referencia> \n" +
                  "<NroLinRef>"+ refe.NroLinRef + "</NroLinRef> \n" + 
                  "<TpoDocRef>" + refe.TpoDocRef +"</TpoDocRef> \n" +
                  "<IndGlobal>"+ refe.IndGlobal +"</IndGlobal>\n" +
                  "<FolioRef>"+ refe.FolioRef +"</FolioRef> \n" +
                  "<RUTOtr>" + refe.RUTOtr + "</RUTOtr> \n" +
                 // "<IdAdicOtr>" + refe.IdAdicOtr +  "</IdAdicOtr> \n" +
                  "<FchRef>" + refe.FchRef + "</FchRef>\n" +
                  "<CodRef>" + refe.CodRef +  "</CodRef>\n" +
                  "<RazonRef>" + refe.RazonRef+ "</RazonRef>\n" +

                "</Referencia> \n";

                documento = documento + referencia;
            }


            DateTime thisDay = DateTime.Today;
            String fch = String.Format("{0:yyyy-M-dTHH:mm:ss}", thisDay);

            // nodo DD
            String dd = "<TED version=\"1.0\">" +
                    "<DD>\r\n" +
                    "<RE>" + doc.RUTEmisor + "</RE>\r\n" +
                    "<TD>" + doc.TipoDTE + "</TD>\r\n" +
                    "<F>" + doc.Folio + "</F>\r\n" +
                    "<FE>" + doc.FchEmis + "</FE>\r\n" +
                    "<RR>" + doc.RUTRecep + "</RR>\r\n" +
                    "<RSR>" + doc.RznSocRecep + "</RSR>\r\n" +
                    "<MNT>" + doc.MntTotal + "</MNT>\r\n" +
                    
                    // acá agregar el primer detalle
                    "<IT1>" + firstNmbItem + "</IT1>\r\n" +

                    getXmlFolio("CAF") + "\r\n"+

                    "<TSTED>" + fch + "</TSTED>\r\n" +
                "</DD>\r\n";

            String firma = "<FRMT algoritmo=\"SHA1withRSA\">\r\n" + firmaNodoDD(dd) + "\r\n</FRMT>\r\n";
            String finTed = "</TED>";

            String fechaFirma = "<TmstFirma>"+fch+"</TmstFirma>";
            String findocumenro = "</Documento>";

            String findte = "</DTE>";

            documento = documento + dd +firma + finTed + fechaFirma + findocumenro + 
                //plantillaFirma + 
                findte;

            X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");


            String signDte = firmarDocumento(documento, cert);


            String envio = creaEnvio(signDte, doc.RUTEmisor, doc.RUTRecep, doc.TipoDTE.ToString());

            String enviox509 = firmarDocumento(envio, cert);

            enviox509 = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n" + enviox509;

            return enviox509;

        }



        public String creaEnvio(String dte, String rutEmisor, String RutReceptor, String tipo)
        { 
            

            String envio_xml = "<EnvioDTE xmlns=\"http://www.sii.cl/SiiDte\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sii.cl/SiiDte EnvioDTE_v10.xsd\" version=\"1.0\">\r\n";
            envio_xml += "<SetDTE ID=\"SetDoc\">\r\n";
            envio_xml += "<Caratula version=\"1.0\">\r\n";
            envio_xml += "<RutEmisor>"+rutEmisor+"</RutEmisor>\r\n";
            //TO DO: Rutenvia, obtener desde certificado
            envio_xml += "<RutEnvia>"+"5682509-6"+"</RutEnvia>\r\n";
            envio_xml += "<RutReceptor>"+RutReceptor+"</RutReceptor>\r\n";

            //TO DO: cambiar fecha de resolución
            envio_xml += "<FchResol>2014-09-10</FchResol>\r\n"; 
            envio_xml += "<NroResol>0</NroResol>\r\n";
            //***********************


            envio_xml += "<TmstFirmaEnv>2014-10-22T22:25:00</TmstFirmaEnv>\r\n";
            envio_xml += "<SubTotDTE>\r\n";
            envio_xml += "<TpoDTE>"+tipo+"</TpoDTE>\r\n";
            envio_xml += "<NroDTE>1</NroDTE>\r\n";
            envio_xml += "</SubTotDTE>\r\n";
            envio_xml += "</Caratula>\r\n";

            envio_xml += dte;

            envio_xml += "</SetDTE>\r\n";
            envio_xml += "</EnvioDTE>\r\n";

            return envio_xml;

        }
       
 
        public String firmaNodoDD(String DD)
        {

            string pk = getXmlFolio("RSA");
            
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);

            RSACryptoServiceProvider rsa = FuncionesComunes.crearRsaDesdePEM(pk);
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");

            string FRMT1 = Convert.ToBase64String(bytesSing);

            return FRMT1;

        }


        public String getXmlFolio(String nodo)
        {

            string nodoValue = string.Empty;
          
            string caf = string.Empty;
            string rsa = string.Empty;
            string line = string.Empty;
            bool cafline = false;
            bool rsaline = false;
            try
              {
                //TO DO : falta tomar el nombre del archivo de una variable global
                  using (StreamReader sr = new StreamReader(@"c:\IatFiles\cafs\Factura\FoliosSII7739857033120141081332.xml"))
                  {
           
                      while ((line = sr.ReadLine()) != null)
                      {
                          if (line == "<CAF version=\"1.0\">") cafline = true;
                          if (line == "</CAF>") 
                          {
                              caf += line;
                              cafline = false; 
                          }

                          if (line == "<RSASK>-----BEGIN RSA PRIVATE KEY-----") 
                          { 
                              rsaline = true;
                              line = sr.ReadLine();
                          }
                          if (line == "-----END RSA PRIVATE KEY-----") rsaline = false;

                          if (cafline) caf += line;
                          if (rsaline) rsa += line;  
                      }
                      sr.Close();   
                  }
              }
              catch (Exception e)
              {
                  Console.WriteLine("The file could not be read:");
                  Console.WriteLine(e.Message);
              }

              if (nodo == "CAF") { nodoValue = caf; } else { nodoValue = rsa; }
              return nodoValue;
        }




        public static string firmarDocumento(string documento, X509Certificate2 certificado)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
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
