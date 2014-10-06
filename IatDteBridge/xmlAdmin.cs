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
            String dte = "<DTE version=\"1.0\"> " +
	                     "<Documento ID=\"F"+doc.Folio+"T"+doc.TipoDte+"\"> ";
		    
            String encabezado = "<Encabezado> "+
			    "<IdDoc> "+
				    "<TipoDTE>"+doc.TipoDte+"</TipoDTE> "+
				    "<Folio>"+ doc.Folio+"</Folio> "+
				    "<FchEmis>"+doc.FchEmis+"</FchEmis> "+
			    "</IdDoc> ";
			
            String emisor = "<Emisor> "+
				    "<RUTEmisor>"+doc.RUTEmisor+"</RUTEmisor> "+
				    "<RznSoc>"+doc.RznSoc+"</RznSoc> "+
				    "<GiroEmis>"+doc.GiroEmis+"</GiroEmis> "+
				    "<Acteco>"+doc.Acteco+"</Acteco> "+
				    "<CdgSIISucur>"+doc.CdgSIISucur+"</CdgSIISucur> "+
				    "<DirOrigen>"+doc.DirOrigen+"</DirOrigen> "+
				    "<CmnaOrigen>"+doc.CmnaOrigen+"</CmnaOrigen> "+
				    "<CiudadOrigen>"+doc.CiudadOrigen+"</CiudadOrigen> "+
			    "</Emisor> ";
			    
            String receptor="<Receptor> "+
				    "<RUTRecep>"+doc.RUTRecep+"</RUTRecep> "+
				    "<RznSocRecep>"+doc.RznSocRecep+"</RznSocRecep> "+
				    "<GiroRecep>"+doc.GiroRecep+"</GiroRecep> "+
				    "<DirRecep>"+doc.DirRecep+"</DirRecep> "+
				    "<CmnaRecep>"+doc.CmnaRecep+"</CmnaRecep> "+
				    "<CiudadRecep>"+doc.CiudadRecep+"</CiudadRecep> "+
			    "</Receptor> ";
			    
            String totales = "<Totales> "+
				    "<MntNeto>"+doc.MntNeto+"</MntNeto> "+
				    "<TasaIVA>"+doc.TasaIVA+"</TasaIVA> "+
				    "<IVA>"+doc.IVA+"</IVA> "+
				    "<MntTotal>"+doc.MntTotal+"</MntTotal> "+
			     "</Totales> ";
		    String finencabezado="</Encabezado>";

            //arma encabezado en documento
            String documento = dte + encabezado + emisor + receptor + totales + finencabezado;

            
            // for para crear detalles y agregarlos al documento
            
            String detalle;

            det_documento det = new det_documento();
            det = doc.detalle[1];

            detalle = "<Detalle> "+
			    "<NroLinDet>"+"</NroLinDet> "+
			    "<CdgItem> "+
				    "<TpoCodigo>INT1</TpoCodigo> "+
				    "<VlrCodigo>011</VlrCodigo> "+
			    "</CdgItem> "+
			    "<NmbItem>Parlantes Multimedia 180W.</NmbItem> "+
			    "<DscItem/> "+
			    "<QtyItem>20</QtyItem> "+
			    "<PrcItem>4500</PrcItem> "+
			    "<MontoItem>90000</MontoItem> "+
		    "</Detalle> ";
		    
            documento = documento + detalle;


            // nodo DD
            String dd = "<TED version=\"1.0\"> " +
                "<DD> " +
                    "<RE>"+doc.RUTEmisor+"</RE> " +
                    "<TD>"+doc.TipoDte+"</TD> " +
                    "<F>"+doc.Folio+"</F> " +
                    "<FE>"+doc.FchEmis+"</FE> " +
                    "<RR>"+doc.RUTRecep+"</RR> " +
                    "<RSR>"+doc.RznSocRecep+"</RSR> " +
                    "<MNT>"+doc.MntTotal+"</MNT> " +

                    
                    // acá agregar el promer detalla
                    "<IT1>Parlantes Multimedia 180W.</IT1> " +


                    getCaf() + 

                    "<TSTED>2014-05-28T09:33:20</TSTED> " +
                "</DD> ";

            String firma = "<FRMT algoritmo=\"SHA1withRSA\">" + firmaNodoDD(dd) +"\n</FRMT> ";    
            String finTed = "</TED>";
        
            String fechaFirma="<TmstFirma>2003-10-13T09:33:20</TmstFirma>";
            String findocumenro = "</Documento>";

            String plantillaFirma= "<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"> "+
                               " <SignedInfo> "+
                               " <CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"/> "+
                               " <SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"/> "+
                               "  <Reference URI=\"\"> "+
                               "   <Transforms> "+
                               "    <Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"/> "+
                               "   </Transforms> "+
                               "    <DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"/> "+
                               "     <DigestValue/> "+
                               "  </Reference> "+
                               "</SignedInfo> "+
                               "<SignatureValue/> "+
                               "<KeyInfo> "+
                               " <KeyValue/> "+
                               " <X509Data > "+
                               "  <X509SubjectName/> "+
                               "  <X509IssuerSerial/> "+
                               "  <X509Certificate/> "+
                               " </X509Data> "+
                               "</KeyInfo> "+
                               "</Signature> ";

            String findte = "</DTE>";

            documento = documento + dd +firma + finTed + fechaFirma + findocumenro + plantillaFirma + findte;

            return documento;
        

        }

 
        public String firmaNodoDD(String DD)
        {

            string pk = getPkCaf();
            
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);

            RSACryptoServiceProvider rsa = FuncionesComunes.crearRsaDesdePEM(pk);
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");

            string FRMT1 = Convert.ToBase64String(bytesSing);

            return FRMT1;

        }

        public String getPkCaf()
        {

            // obtener desde la clave privade del CAF entregado por SII


            string pk = string.Empty;
            //TO DO : Agregar algoritmo para extraer solo la PK del CAF 
            /* 
      
              try
              {
                  using (StreamReader sr = new StreamReader("TestFile.txt"))
                  {
                      pk = sr.ReadToEnd();
                      Console.WriteLine(line);
                  }
              }
              catch (Exception e)
              {
                  Console.WriteLine("The file could not be read:");
                  Console.WriteLine(e.Message);
              }
              */
            
            pk += "MIIBOwIBAAJBANGuDuim8fEI9yuIlkj+MOyp3mWHifoP6a4oWLSBKJSrd3MpEsZd";
            pk += "czvL0l7t/e0IU5rF+0gRLnU1Mfvtsw1wYWcCAQMCQQCLyV9FxKFLW09yWw7bVCCd";
            pk += "xpRDr7FRX/EexZB4VhsNxm/vtJfDZyYle0Lfy42LlcsXxPm1w6Q6NnjuW+AeBy67";
            pk += "AiEA7iMi5q5xjswqq+49RP55o//jqdZL/pC9rdnUKxsNRMMCIQDhaHdIctErN2hC";
            pk += "IP9knS3+9zra4R+5jSXOvI+3xVhWjQIhAJ7CF0R0S7SIHHKe04NUURf/7RvkMqm1";
            pk += "08k74sdnXi3XAiEAlkWk2vc2HM+a1sCqQxNz/098ketqe7NuidMKeoOQObMCIQCk";
            pk += "FAMS9IcPcMjk7zI2r/4EEW63PSXyN7MFAX7TYe25mw==";

            return pk;
        }


        public String getCaf()
        {

            // obtener desde el CAF entregado por SII

            string caf = string.Empty;
            //TO DO : Agregar algoritmo para extraer solo el encabezado del CAF 
            /* 
              try
              {
                  using (StreamReader sr = new StreamReader("TestFile.txt"))
                  {
                      pk = sr.ReadToEnd();
                      Console.WriteLine(line);
                  }
              }
              catch (Exception e)
              {
                  Console.WriteLine("The file could not be read:");
                  Console.WriteLine(e.Message);
              }
              */

            caf = "<CAF version=\"1.0\"> " +
                    "<DA> " +
                        "<RE>10207640-0</RE> " +
                        "<RS>JUAN CARLOS AGUIRRE RODRIGUEZ</RS> " +
                        "<TD>33</TD> " +
                        "<RNG><D>1</D><H>50</H></RNG> " +
                        "<FA>2014-05-26</FA> " +
                        "<RSAPK><M>uJ+OZ5qO9diB/c9MoZuwPs9ltKGAS1IbEymF7W3X3ZTq6ElExVkrlfp7uDoGR0DiBndor6Vyc+X4MRbsk6KC9w==</M><E>Aw==</E></RSAPK> " +
                        "<IDK>100</IDK> " +
                    "</DA> " +
                    "<FRMA algoritmo=\"SHA1withRSA\">SGKR9otZoN8/5sIaKFJIbo08Jbt95UBh76fcFv21lfNsgauAcyzUF0FARrMyphMagJ0zzChJzU7R/Q0mrDvYvQ==</FRMA> " +
                    "</CAF> ";

            return caf;
        }




    }
}
