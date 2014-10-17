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
                "<DscItem>" + det.DscItem+"<DscItem/> \n" +
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
                  "<NroLinRef>"+ refe.NroLinRef + "<NroLinRef> \n" + 
                  "<TpoDocRef>" + refe.TpoDocRef +"<TpoDocRef> \n" +
                  "<IndGlobal>"+ refe.IndGlobal +"<IndGlobal>\n" +
                  "<FolioRef>"+ refe.FolioRef +"<FolioRef> \n" +
                  "<RUTOtr>" + refe.RUTOtr + "<RUTOtr> \n" +
                  "<IdAdicOtr>" + refe.IdAdicOtr +  "<IdAdicOtr> \n" +
                  "<FchRef>" + refe.FchRef + "<FchRef>\n" +
                  "<CodRef>" + refe.CodRef +  "<CodRef>\n" +
                  "<RazonRef>" + refe.RazonRef+ "<RazonRef>\n" +

                "</Referencia> \n";

                documento = documento + referencia;
            } 
            
            // nodo DD
            String dd = "<TED version=\"1.0\"> \n" +
                "<DD> " +
                    "<RE>" + doc.RUTEmisor + "</RE> \n" +
                    "<TD>" + doc.TipoDTE + "</TD> \n" +
                    "<F>" + doc.Folio + "</F> \n" +
                    "<FE>" + doc.FchEmis + "</FE> \n" +
                    "<RR>" + doc.RUTRecep + "</RR> \n" +
                    "<RSR>" + doc.RznSocRecep + "</RSR> \n" +
                    "<MNT>" + doc.MntTotal + "</MNT> \n" +
                    
                    // acá agregar el primer detalle
                    "<IT1>" + firstNmbItem + "</IT1> \n" +

                    getXmlFolio("CAF") +

                    "<TSTED>2014-05-28T09:33:20</TSTED> \n" +
                "</DD> ";

            String firma = "<FRMT algoritmo=\"SHA1withRSA\">" + firmaNodoDD(dd) + "\n</FRMT> \n";
            String finTed = "</TED>\n";

            String fechaFirma = "<TmstFirma>2003-10-13T09:33:20</TmstFirma>\n";
            String findocumenro = "</Documento>\n";

            String plantillaFirma = "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"> \n" +
                               " <SignedInfo> "+
                               " <CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"/> \n" +
                               " <SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"/> \n" +
                               "  <Reference URI=\"\"> \n" +
                               "   <Transforms> \n" +
                               "    <Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"/> \n" +
                               "   </Transforms> \n" +
                               "    <DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"/> \n" +
                               "     <DigestValue/> \n" +
                               "  </Reference> \n" +
                               "</SignedInfo> \n" +
                               "<SignatureValue/> \n" +
                               "<KeyInfo> \n" +
                               " <KeyValue/> \n" +
                               " <X509Data > \n" +
                               "  <X509SubjectName/> \n" +
                               "  <X509IssuerSerial/> \n" +
                               "  <X509Certificate/> \n" +
                               " </X509Data> \n" +
                               "</KeyInfo> \n" +
                               "</Signature> \n";

            String findte = "</DTE> \n";

            documento = documento + dd +firma + finTed + fechaFirma + findocumenro + plantillaFirma + findte;

            return documento;

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




    }
}
