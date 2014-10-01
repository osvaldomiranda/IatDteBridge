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

    
        public string firmatimbre(String nodoDD) 
        {

            return " ";
        }

    }
}
