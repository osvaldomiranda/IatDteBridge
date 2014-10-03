using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;



namespace IatDteBridge
{
    class xmlAdmin
    {


        public void doc_to_xmlSii() //recibir parametro de tipo documento
        {
            String dte = "<DTE version=\"1.0\"> " +
	                     "<Documento ID=\"F1T33\"> ";
		    
            String encabezado = "<Encabezado> "+
			    "<IdDoc> "+
				    "<TipoDTE>33</TipoDTE> "+
				    "<Folio>1</Folio> "+
				    "<FchEmis>2014-05-28</FchEmis> "+
			    "</IdDoc> ";
			
            String emisor = "<Emisor> "+
				    "<RUTEmisor>10207640-0</RUTEmisor> "+
				    "<RznSoc>JUAN CARLOS AGUIRRE RODRIGUEZ</RznSoc> "+
				    "<GiroEmis>Insumos de Computacion</GiroEmis> "+
				    "<Acteco>31341</Acteco> "+
				    "<CdgSIISucur>1234</CdgSIISucur> "+
				    "<DirOrigen>Teatinos 120, Piso 4</DirOrigen> "+
				    "<CmnaOrigen>Santiago</CmnaOrigen> "+
				    "<CiudadOrigen>Santiago</CiudadOrigen> "+
			    "</Emisor> ";
			    
            String receptor="<Receptor> "+
				    "<RUTRecep>77777777-7</RUTRecep> "+
				    "<RznSocRecep>EMPRESA  LTDA</RznSocRecep> "+
				    "<GiroRecep>COMPUTACION</GiroRecep> "+
				    "<DirRecep>SAN DIEGO 2222</DirRecep> "+
				    "<CmnaRecep>LA FLORIDA</CmnaRecep> "+
				    "<CiudadRecep>SANTIAGO</CiudadRecep> "+
			    "</Receptor> ";
			    
            String totales = "<Totales> "+
				    "<MntNeto>100000</MntNeto> "+
				    "<TasaIVA>19</TasaIVA> "+
				    "<IVA>19000</IVA> "+
				    "<MntTotal>119000</MntTotal> "+
			     "</Totales> ";
		    String finencabezado="</Encabezado>";

            //arma encabezado en documento
            String documento = dte + encabezado + emisor + receptor + totales + finencabezado;

            
            // for para crear detalles y agregarlos al documento
            
            String detalle;
            
            detalle = "<Detalle> "+
			    "<NroLinDet>1</NroLinDet> "+
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
            String dd = "<TED version=\"1.0\"> "+
			    "<DD> "+
				    "<RE>97975000-5</RE> "+
				    "<TD>33</TD> "+
				    "<F>1</F> "+
				    "<FE>2014-05-28</FE> "+
				    "<RR>77777777-7</RR> "+
				    "<RSR>EMPRESA  LTDA</RSR> "+
				    "<MNT>119000</MNT> "+
				    "<IT1>Parlantes Multimedia 180W.</IT1> "+
				
				    
                    /* Agregar caf leido desde archivo    
                    <CAF version="1.0">
				    <DA>
				        <RE>10207640-0</RE>
				        <RS>JUAN CARLOS AGUIRRE RODRIGUEZ</RS>
				        <TD>33</TD>
				        <RNG><D>1</D><H>50</H></RNG>
				        <FA>2014-05-26</FA>
				        <RSAPK><M>uJ+OZ5qO9diB/c9MoZuwPs9ltKGAS1IbEymF7W3X3ZTq6ElExVkrlfp7uDoGR0DiBndor6Vyc+X4MRbsk6KC9w==</M><E>Aw==</E></RSAPK>
				        <IDK>100</IDK>
				    </DA>
				    <FRMA algoritmo="SHA1withRSA">SGKR9otZoN8/5sIaKFJIbo08Jbt95UBh76fcFv21lfNsgauAcyzUF0FARrMyphMagJ0zzChJzU7R/Q0mrDvYvQ==</FRMA>
				    </CAF>
                    */

				    "<TSTED>2014-05-28T09:33:20</TSTED> "+
			    "</DD> "+
                 /* acá va la firma 
			     <FRMT algoritmo="SHA1withRSA">GK7FRnNjgHLyRspdygg2WudvqqJ+OQchn8k/6TUrndBBNHsFHInEN34+KLTy\nFgRG/bmDIjclV4VTlgs3TIg/7A==\n</FRMT>
                 */ 
             "</TED>";
        
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

            documento = documento + dd + fechaFirma + findocumenro + plantillaFirma + findte;

        

        }

 
        public static void PruebaTimbreDD()
        {

            ////
            //// Contenido del nodo TED del ejemplo. 
            //// Este es el formato que debe tener los datos
            //// 
            string DD = string.Empty;
            DD += "<DD><RE>97975000-5</RE><TD>33</TD><F>27</F><FE>2003-09-08</FE>";
            DD += "<RR>8414240-9</RR><RSR>JORGE GONZALEZ LTDA</RSR><MNT>502946</M";
            DD += "NT><IT1>Cajon AFECTO</IT1><CAF version=\"1.0\"><DA><RE>97975000-";
            DD += "5</RE><RS>RUT DE PRUEBA</RS><TD>33</TD><RNG><D>1</D><H>200</H>";
            DD += "</RNG><FA>2003-09-04</FA><RSAPK><M>0a4O6Kbx8Qj3K4iWSP4w7KneZYe";
            DD += "J+g/prihYtIEolKt3cykSxl1zO8vSXu397QhTmsX7SBEudTUx++2zDXBhZw==<";
            DD += "/M><E>Aw==</E></RSAPK><IDK>100</IDK></DA><FRMA algoritmo=\"SHA1";
            DD += "withRSA\">g1AQX0sy8NJugX52k2hTJEZAE9Cuul6pqYBdFxj1N17umW7zG/hAa";
            DD += "vCALKByHzdYAfZ3LhGTXCai5zNxOo4lDQ==</FRMA></CAF><TSTED>2003-09";
            DD += "-08T12:28:31</TSTED></DD>";

            ////
            //// Representa la clave privada rescatada desde el CAF que envía el SII
            //// para la prueba propuesta por ellos.
            ////
            string pk = string.Empty;
            pk += "MIIBOwIBAAJBANGuDuim8fEI9yuIlkj+MOyp3mWHifoP6a4oWLSBKJSrd3MpEsZd";
            pk += "czvL0l7t/e0IU5rF+0gRLnU1Mfvtsw1wYWcCAQMCQQCLyV9FxKFLW09yWw7bVCCd";
            pk += "xpRDr7FRX/EexZB4VhsNxm/vtJfDZyYle0Lfy42LlcsXxPm1w6Q6NnjuW+AeBy67";
            pk += "AiEA7iMi5q5xjswqq+49RP55o//jqdZL/pC9rdnUKxsNRMMCIQDhaHdIctErN2hC";
            pk += "IP9knS3+9zra4R+5jSXOvI+3xVhWjQIhAJ7CF0R0S7SIHHKe04NUURf/7RvkMqm1";
            pk += "08k74sdnXi3XAiEAlkWk2vc2HM+a1sCqQxNz/098ketqe7NuidMKeoOQObMCIQCk";
            pk += "FAMS9IcPcMjk7zI2r/4EEW63PSXyN7MFAX7TYe25mw==";


            //// 
            //// Este es el resultado que el SII indica debe obtenerse despues de crear
            //// el timbre sobre los datos expuestos.
            ////
            const string HTIMBRE = "pqjXHHQLJmyFPMRvxScN7tYHvIsty0pqL2LLYaG43jMmnfiZfllLA0wb32lP+HBJ/tf8nziSeorvjlx410ZImw==";


            //// //////////////////////////////////////////////////////////////////
            //// Generar timbre sobre los datos del tag DD utilizando la clave 
            //// privada suministrada por el SII en el archivo CAF
            //// //////////////////////////////////////////////////////////////////

            ////
            //// Calcule el hash de los datos a firmar DD
            //// transformando la cadena DD a arreglo de bytes, luego con
            //// el objeto 'SHA1CryptoServiceProvider' creamos el Hash del
            //// arreglo de bytes que representa los datos del DD
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);

            ////
<<<<<<< HEAD
            //// Cree el objeto Rsa para poder firmar el hashValue creado
            //// en el punto anterior. La clase FuncionesComunes.crearRsaDesdePEM()
            //// Transforma la llave rivada del CAF en formato PEM a el objeto
            //// Rsa necesario para la firma.
            RSACryptoServiceProvider rsa = FuncionesComunes.crearRsaDesdePEM(pk);
=======
            //// Agregue la referencia al objeto signature
            XMLSignature.SignedInfo.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
           // keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));
>>>>>>> Mauricio

            ////
            //// Firme el HashValue ( arreglo de bytes representativo de DD )
            //// utilizando el formato de firma SHA1, lo cual regresará un nuevo 
            //// arreglo de bytes.
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");

            ////
            //// Recupere la representación en base 64 de la firma, es decir de
            //// el arreglo de bytes 
            string FRMT1 = Convert.ToBase64String(bytesSing);

            ////
            //// Comprobación del timbre generado por nuestra rutina contra el
            //// valor 
            if (HTIMBRE.Equals(FRMT1))
            {
                Console.WriteLine("Comprobacion OK");
            }
            else
            {
                Console.WriteLine("Comprobacion NOK");
            }

        }


    }
}
