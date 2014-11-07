using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace IatDteBridge
{
    [DataContract]
    class Documento
    {
        [DataMember]
        public int TipoDTE { get; set; } //
        [DataMember] 
        public int Folio{get;set;}
        [DataMember]
        public String FchEmis { get; set; }
        [DataMember] 
        public int IndNoRebaja { get; set; }
        [DataMember]
        public int TipoDespacho { get; set; }
        [DataMember] 
        public int IndTraslado { get; set; }
        [DataMember] 
        public String TpoImpresion { get; set; }
        [DataMember]
        public int IndServicio { get; set; }
        [DataMember]
        public int MntBruto { get; set; }
        [DataMember]
        public int FmaPago { get; set; }
        [DataMember]
        public List<MntPagos> mntpagos = new List<MntPagos>();
        [DataMember]
        public string FchVenc { get; set; }


//#################################### Area Emisor ####################################################################
        [DataMember] 
        public string RUTEmisor { get; set; }
        [DataMember] 
        public string RznSoc { get; set; }
        [DataMember] 
        public string GiroEmis { get; set; }
        [DataMember] 
        public string Telefono { get; set; }
        [DataMember] 
        public string CorreoEmisor { get; set; }
        [DataMember] 
        public int Acteco { get; set; } // Actividad Economica
        [DataMember] 
        public int CdgTraslado { get; set; } //solo para guia de despacho
        [DataMember] 
        public int FolioAut { get; set; } //Solo para guia de despacho.
        [DataMember] 
        public string FchAut { get; set; } //
        [DataMember] 
        public string Sucursal { get; set; } //Nombre de la sucursal que emite el documento
        [DataMember] 
        public int CdgSIISucur { get; set; } // Codigo de sucursal que emite el documento
        [DataMember] 
        public string CodAdicSucur { get; set; } //Codigo para uso libre
        [DataMember] 
        public string DirOrigen { get; set; } // Direccion desde donde se despachan
        [DataMember] 
        public string CmnaOrigen { get; set; } // Analogo a direccion de origen
        [DataMember] 
        public string CiudadOrigen { get; set; } // Analogo a direccion de origen
        [DataMember] 
        public int CdgVendedor { get; set; } // Identificador del vendedor
        [DataMember] 
        public string IdAdicEmisor { get; set; } // adicional para uso libre
        [DataMember]
        public string RUTMandante { get; set; }
//################################### Area Receptor ############################################################################
        
        [DataMember]
        public string RUTRecep { get; set; } // rut del cliente en la factura de compra se referencia al vendedor
        [DataMember] 
        public string CdgIntRecep { get; set;  } // para identificacion interna de receptor
        [DataMember]
        public string RznSocRecep { get; set; } // Razon Social Receptor
        [DataMember] 
        public string NumId { get; set; } // Numero o codigo de identificacion personal del receptor extrangero otorgado por la adm. tributaria
        [DataMember] 
        public string Nacionalidad { get; set; } // Nacionalidad del extrangero
        [DataMember] 
        public string IdAdicRecep { get; set; } // solo para exportacion uso libre
        [DataMember]
        public string GiroRecep { get; set;  } // glosa giro del receptor
        [DataMember] 
        public string Contacto { get; set;  } // Glosa con nombre o telefono del contacto de la empresa receptor "Atencion a:"
        [DataMember] 
        public string CorreoRecep { get; set; } // e-mail de contacto en empresa del receptor (para registrar el “Atención A:”)
        [DataMember]
        public string DirRecep { get; set;  } // Dirección Legal del Receptor (registrada en el SII) En caso de documentos de exportación, corresponde a la dirección en el extranjero del Receptor
        [DataMember]
        public string CmnaRecep { get; set; } // Análogo a Dirección Receptor
        [DataMember]
        public string CiudadRecep { get; set; } //Análogo a Dirección Receptor
        [DataMember] 
        public string DirPostal { get; set; } // Análogo a Dirección Recepto
        [DataMember] 
        public string CmnaPostal { get; set; } // Análogo a Dirección Receptor
        [DataMember] 
        public string CiudadPostal { get; set; } // Análogo a Dirección Receptor
        [DataMember]
        public string RUTSolicita { get; set; } // En casos de venta a público. Es obligatorio si es distinto de Rut receptor o Rut Receptor es persona jurídica. Con guión y dígito verificador
//################################# Area Transporte #############################################################################       
        [DataMember] 
        public string Patente { get; set; } // 
        [DataMember]
        public string RUTTrans { get; set; } // Con guión y dígito verificador Indicador Tipo de Despacho es 2 o 3
        [DataMember] 
        public string RUTChofer { get; set; } // 
        [DataMember]
        public string NombreChofer { get; set; } // 
        [DataMember]    
        public string DirDest { get; set; } // Datos correspondientes a Dirección destino en documento que acompaña productos o a la Dirección en que se otorga el servicio en caso de Servicios periódicos.
        [DataMember]
        public string CmnaDest { get; set; } // Análogo Dirección Destino
        [DataMember] 
        public string CiudadDest { get; set; } // Análogo Dirección Destino
        [DataMember] 
        public int CodModVenta { get; set; } // Para doctos. utilizados en exportación. Se refiere a si la exportación se realiza bajo venta, En consignación, a firme, en Consignación con mínimo afirme, etc.)
        [DataMember] 
        public int CodClauVenta { get; set; } // Se refiere a la cláusula de venta indicada en el DUS ( FOB, CIF, etc.)
        [DataMember] 
        public int TotClauVenta { get; set; } // Corresponde al valor total de la exportación a pagar por el importador según la cláusula de venta acordada entre las partes y que se indica en el DUS. (No incluye comisiones ni otros gastos deducibles en el exterior)
        [DataMember] 
        public int CodViaTransp { get; set; } // Corresponde a la vía de transporte por donde se envía la mercadería (aéreo, terrestre, marítimo, etc) al Extranjero
        [DataMember] 
        public string NombreTransp { get; set; } // Corresponde al nombre o glosa de la nave transportista.
        [DataMember] 
        public string RUTCiaTransp { get; set; } // Para doctos. utilizados en exportación. Señale el Rol Unico Tributario (RUT) de la compañía transportista indicada en el DUS. Si ésta es extranjera, señale el RUT de la Agencia que la representa en Chile.
        [DataMember] 
        public string NomCiaTransp { get; set; } // Nombre de la Cía. transportadora declarada en el DUS.
        [DataMember] 
        public string IdAdicTransp { get; set; } // Identificación adicional para uso libre
        [DataMember] 
        public string Booking { get; set; } // Número de Booking o Reserva del operador
        [DataMember] 
        public string Operador { get; set; } // Código de Operador
        [DataMember] 
        public int CodPtoEmbarque { get; set; } // Puerto de embarque de mercancías
        [DataMember] 
        public string IdAdicPtoEmb { get; set; } // Identificación adicional para uso libre
        [DataMember] 
        public string CodPtoDesemb { get; set; } // 
        [DataMember] 
        public string IdAdicPtoDesemb { get; set; } // Identificación adicional para uso libre
        [DataMember] 
        public int Tara { get; set; }//
        [DataMember] 
        public int CodUnidMedTara { get; set; } // Indique la unidad de medida en la que se encuentra expresado la Tara
        [DataMember] 
        public int PesoBruto { get; set; } // Señale con dos decimales, la sumatoria de los pesos brutos de todos los ítems del documento. 
        [DataMember] 
        public int CodUnidPesoBruto { get; set; } // Indique la unidad de medida en la que se encuentra el peso bruto de la mercadería
        [DataMember] 
        public int PesoNeto { get; set; } // Señale con dos decimales, la sumatoria del peso neto de todos los ítems del documento.
        [DataMember] 
        public int CodUnidPesoNeto { get; set; } // Indique la unidad de medida en la que se encuentra el peso neto de la mercadería
        [DataMember] 
        public int TotItems { get; set; } // Indique el total de ítems del documento
        [DataMember] 
        public int TotBultos { get; set; } // Señale la cantidad total de bultos que ampara el documento.

// ############################# Area Totales ####################################################################################
        [DataMember] 
        public string TpoMoneda { get; set;  } // Moneda en que se registra la transacción de exportación.
        [DataMember]
        public int MntNeto { get; set; } // Suma de valores total de ítems afectos -descuentos globales + recargos globales (Asignados a ítems afectos). Si está encendido el Indicador de Montos Brutos (=1) entonces el resultado anterior se debe dividir por (1 + tasa de IVA)
        [DataMember]
        public int MntExe { get; set; } // Suma de valores total de ítems no afectos o exentos -descuentos globales + recargos globales (Asignados a ítems exentos o no afectos)
        [DataMember]
        public int MntBase { get; set; } // Monto informado
        [DataMember]
        public int MntMargenCom { get; set; } // Monto informado
        [DataMember]
        public float TasaIVA { get; set; } // 
        [DataMember]
        public int IVA { get; set; } // 
        [DataMember]           
        public int IVAProp { get; set; } //Las empresas que venden por cuenta de un mandatario, pueden opcional separar el IVA en propio y de terceros. En todos estos casos el campo “IVA” debe contener el IVA total de la Factura
        [DataMember] 
        public int IVATerc { get; set; } // Ídem al anterior
        // impuestos adicionales puede ser mas de uno por ese motivo se crea una clase
        [DataMember]
        public List<ImptoReten> imptoReten = new List<ImptoReten>();
        [DataMember] 
        public int IVANoRet { get; set; } // Sólo en facturas de Compra en que hay retención de IVA por el emisor y Notas de Crédito o débito que referencian facturas de compra. No se registra si es igual a 0.
        [DataMember] 
        public int CredEC { get; set; } // Artículo 21 del decreto ley N° 910/75. Este Es el único código que opera en forma opuesta al resto, ya que se resta al IVA general
        [DataMember] 
        public int GrntDep { get; set; } // Sólo para empresas que usen envases en forma habitual, por su giro principal. Art.28,Inc3 Reglamento DL 825) (Cervezas, Jugos, Aguas Minerales, Bebidas Analcohólicas u otros autorizados por Resolución especial). Corresponde a la Sumatoria de las líneas de detalle que indican Indicador de facturación/ exención = 3        
        [DataMember] 
        public int ValComNeto { get; set; }  // Suma de detalle de Valores de Comisiones y Otros Cargos
        [DataMember] 
        public int ValComExe { get; set; } // Suma de detalles de valores de comisiones y otros cargos no afectos o exentos
        [DataMember] 
        public int ValComIVA { get; set; } // Suma de detalle de IVA de Valor de Comisiones y Otros Cargos
        [DataMember]
        public int MntTotal { get; set; } // Monto neto + Monto no afecto o  exento + IVA + Impuestos Adicionales + Impuestos Específicos + Iva Margen Comercialización +IVA Anticipado + Garantía por depósito de envases o embalajes - Crédito empresas constructoras- IVA Retenido productos (en caso de facturas de compra) -  Valor Neto Comisiones y Otros Cargos- IVA Comisiones y Otros Cargos - Valor Comisiones y Otros Cargos No Afectos o Exentos. (Los Impuestos Adicionales y el IVA Anticipado están detallados en la TABLA de Impuestos Adicionales y Retenciones)
        [DataMember] 
        public int MontoNF { get; set; } //Suma de montos de bienes o servicios con Indicador de facturación/ exención = 2 menos Suma de montos de bienes o servicios con Indicador de facturación/ exención = 6

 //############################## Area Detalle #################################################################################
        [DataMember]
        public List<Detalle> detalle = new List<Detalle>();

        [DataMember]
        public List<DscRcgGlobal> dscRcgGlobal = new List<DscRcgGlobal>();

        [DataMember]
        public List<ReferenciaDoc>  Referencia = new List<ReferenciaDoc>();

        [DataMember]
        public List<Comisiones> comisiones = new List<Comisiones>();


        public Documento(String Data)
        {

            if (Data.Length == 0) return;

            TipoDTE      = Convert.ToInt32(extraeValorJson(Data, "TipoDTE",3, 3));
            Folio        = Convert.ToInt32(extraeValorJson(Data, "Folio",3, 3));
            FchEmis      = extraeValorJson(Data, "FchEmis",4, 10);
            IndNoRebaja  = Convert.ToInt32(extraeValorJson(Data, "IndNoRebaja", 3, 3));
            TipoDespacho = Convert.ToInt32(extraeValorJson(Data, "TipoDespacho", 3, 3));
            IndTraslado  = Convert.ToInt32(extraeValorJson(Data, "IndTraslado", 3, 3));
            TpoImpresion = extraeValorJson(Data, "TpoImpresion", 4, 3);
            IndServicio  = Convert.ToInt32(extraeValorJson(Data, "IndServicio", 3, 3));
            MntBruto     = Convert.ToInt32(extraeValorJson(Data, "MntBruto", 3, 3));
            FmaPago      = Convert.ToInt32(extraeValorJson(Data, "FmaPago", 3, 3));
    
           
            FchVenc = extraeValorJson(Data, "FchVenc", 4, 10);


            // EMISOR
            /*
            RUTEmisor     = extraeValorJson(Data, "RUTEmisor", 3);
            RznSoc        = extraeValorJson(Data, "RznSoc", 3);
            GiroEmis      = extraeValorJson(Data, "GiroEmis", 3);
            Telefono      = extraeValorJson(Data, "Telefono", 3);
            CorreoEmisor  = extraeValorJson(Data, "CorreoEmisor", 3);
            Acteco        = Convert.ToInt32(extraeValorJson(Data, "Acteco", 3));
            CdgTraslado   = Convert.ToInt32(extraeValorJson(Data, "CdgTraslado", 3));
            FolioAut      = Convert.ToInt32(extraeValorJson(Data, "FolioAut", 3));
            FchAut        = extraeValorJson(Data, "FchAut", 3);
            Sucursal      = extraeValorJson(Data, "Sucursal", 3);
            CdgSIISucur   = Convert.ToInt32(extraeValorJson(Data, "CdgSIISucur", 3));
            CodAdicSucur  = extraeValorJson(Data, "CodAdicSucur", 3);
            DirOrigen     = extraeValorJson(Data, "DirOrigen", 3);
            CmnaOrigen    = extraeValorJson(Data, "CmnaOrigen", 3);
            CiudadOrigen  = extraeValorJson(Data, "CiudadOrigen", 3);
            CdgVendedor   = Convert.ToInt32(extraeValorJson(Data, "CdgVendedor", 3));
            IdAdicEmisor  = extraeValorJson(Data, "IdAdicEmisor", 3);
            RUTMandante   = extraeValorJson(Data, "RUTMandante", 3);
            */

           // Area Receptor 
            RUTRecep        = extraeValorJson(Data, "RUTRecep", 4, 10);
            CdgIntRecep     = extraeValorJson(Data, "CdgIntRecep",4, 3);
            RznSocRecep     = extraeValorJson(Data, "RznSocRecep", 4, 3);
            NumId           = extraeValorJson(Data, "NumId", 4, 3);
            Nacionalidad    = extraeValorJson(Data, "Nacionalidad", 4, 3);
            IdAdicRecep     = extraeValorJson(Data, "IdAdicRecep", 4, 3);
            GiroRecep       = extraeValorJson(Data, "GiroRecep", 4, 3);
            Contacto        = extraeValorJson(Data, "Contacto", 4, 3);
            CorreoRecep     = extraeValorJson(Data, "CorreoRecep", 4, 3);
            DirRecep        = extraeValorJson(Data, "DirRecep", 4, 3);
            CmnaRecep       = extraeValorJson(Data, "CmnaRecep", 4, 3);
            CiudadRecep     = extraeValorJson(Data, "CiudadRecep", 4, 3);
            DirPostal       = extraeValorJson(Data, "DirPostal", 4, 3);
            CmnaPostal      = extraeValorJson(Data, "CmnaPostal", 4, 3);
            CiudadPostal    = extraeValorJson(Data, "CiudadPostal", 4, 3);
            RUTSolicita     = extraeValorJson(Data, "RUTSolicita", 4, 3);

            //Area Transporte      
             Patente        = extraeValorJson(Data, "Patente", 4, 3);
             RUTTrans       = extraeValorJson(Data, "RUTTrans", 4, 3); 
             RUTChofer      = extraeValorJson(Data, "RUTChofer", 4, 3); 
             NombreChofer   = extraeValorJson(Data, "NombreChofer", 4, 3); 
             DirDest        = extraeValorJson(Data, "DirDest", 4, 3); 
             CmnaDest       = extraeValorJson(Data, "CmnaDest", 4, 3); 
             CiudadDest     = extraeValorJson(Data, "CiudadDest", 4, 3);
             CodModVenta    = Convert.ToInt32(extraeValorJson(Data,"CodModVenta", 3, 3));
             CodClauVenta   = Convert.ToInt32(extraeValorJson(Data, "CodClauVenta", 3, 3)); 
             TotClauVenta   = Convert.ToInt32(extraeValorJson(Data, "TotClauVenta", 3, 3));
             CodViaTransp   = Convert.ToInt32(extraeValorJson(Data, "CodViaTransp", 3, 3)); 
             NombreTransp   = extraeValorJson(Data, "NombreTransp", 4, 3);
             RUTCiaTransp   = extraeValorJson(Data, "RUTCiaTransp", 4, 3); 
             NomCiaTransp   = extraeValorJson(Data, "NomCiaTransp", 4, 3);  
             IdAdicTransp   = extraeValorJson(Data, "IdAdicTransp", 4, 3);  
             Booking        = extraeValorJson(Data, "Booking", 4, 3);
             Operador       = extraeValorJson(Data, "Operador", 4, 3); 
             CodPtoEmbarque = Convert.ToInt32(extraeValorJson(Data, "CodPtoEmbarque", 3, 3));
             IdAdicPtoEmb   = extraeValorJson(Data, "IdAdicPtoEmb", 4, 3);
             CodPtoDesemb   = extraeValorJson(Data, "RUTSolicita", 4, 3); 
             IdAdicPtoDesemb= extraeValorJson(Data, "CodPtoDesemb", 4, 3); 
             Tara           = Convert.ToInt32(extraeValorJson(Data, "Tara", 3, 3)); 
             CodUnidMedTara = Convert.ToInt32(extraeValorJson(Data, "CodUnidMedTara", 3, 3)); 
             PesoBruto      = Convert.ToInt32(extraeValorJson(Data, "PesoBruto", 3, 3)); 
             CodUnidPesoBruto = Convert.ToInt32(extraeValorJson(Data, "CodUnidPesoBruto", 3, 3)); 
             PesoNeto       = Convert.ToInt32(extraeValorJson(Data, "PesoNeto", 3, 3));
             CodUnidPesoNeto= Convert.ToInt32(extraeValorJson(Data, "CodUnidPesoNeto", 3, 3)); 
             TotItems       = Convert.ToInt32(extraeValorJson(Data, "TotItems", 3, 3));
             TotBultos      = Convert.ToInt32(extraeValorJson(Data, "TotBultos", 3, 3));

            // Area Totales 
             TpoMoneda      = extraeValorJson(Data, "TpoMoneda", 4, 3);
             MntNeto        = Convert.ToInt32(extraeValorJson(Data, "MntNeto", 3, 3));
             MntExe         = Convert.ToInt32(extraeValorJson(Data, "MntExe", 3, 3));
             MntBase        = Convert.ToInt32(extraeValorJson(Data, "MntBase", 3, 3));
             MntMargenCom   = Convert.ToInt32(extraeValorJson(Data, "MntMargenCom", 3, 3));
             TasaIVA        = Convert.ToInt32(extraeValorJson(Data, "TasaIVA", 3, 3));
             IVA            = Convert.ToInt32(extraeValorJson(Data, "IVA", 3, 3));
             IVAProp        = Convert.ToInt32(extraeValorJson(Data, "IVAProp", 3, 3));
             IVATerc        = Convert.ToInt32(extraeValorJson(Data, "IVATerc", 3, 3));      
             IVANoRet       = Convert.ToInt32(extraeValorJson(Data, "IVANoRet", 3, 3));
             CredEC         = Convert.ToInt32(extraeValorJson(Data, "CredEC", 3, 3));
             GrntDep        = Convert.ToInt32(extraeValorJson(Data, "GrntDep", 3, 3));
             ValComNeto     = Convert.ToInt32(extraeValorJson(Data, "ValComNeto", 3, 3));
             ValComExe      = Convert.ToInt32(extraeValorJson(Data, "ValComExe", 3, 3));
             ValComIVA      = Convert.ToInt32(extraeValorJson(Data, "ValComIVA", 3, 3));
             MntTotal       = Convert.ToInt32(extraeValorJson(Data, "MntTotal", 3, 3));
             MontoNF        = Convert.ToInt32(extraeValorJson(Data, "MontoNF", 3, 3));

          


             detalle      = extraeDetalle(Data);
             mntpagos     = extraeMntPagos(Data);
             imptoReten   = extraeImptoReten(Data);
             dscRcgGlobal = extraeDscRcgGlobal(Data);
             Referencia   = extraeReferenciaDoc(Data);
             comisiones   = extraeComisiones(Data);


            }

        public List<Comisiones> extraeComisiones(String data)
        {
            List<Comisiones> DscR = new List<Comisiones>();

            String Lin = String.Empty;

            String L = extraeLista(data, "ReferenciaDoc");

            if (L.Length == 0) return null;

            int n = occurs(data, '{');
            int i = 0;
            while (i < n)
            {
                Lin = sgteLineaLista(L, i);
                Comisiones clase = new Comisiones();
                clase.Glosa = extraeValorLista(Lin, "Glosa", 4, 0);
                clase.NroLinCom = Convert.ToInt32(extraeValorLista(Lin, "NroLinCom", 3, 0));
                clase.TipoMovim = extraeValorLista(Lin, "TipoMovim", 4, 0);
                clase.ValComExe = Convert.ToInt32(extraeValorLista(Lin, "ValComExe", 3, 0));
                clase.ValComIVA = Convert.ToInt32(extraeValorLista(Lin, "ValComIVA", 3, 0));
                clase.ValComNeto = Convert.ToInt32(extraeValorLista(Lin, "ValComNeto", 3, 1));
                

                DscR.Add(clase);
                i++;
            }

            return DscR;
        }

        public List<ReferenciaDoc> extraeReferenciaDoc(String data)
        {
            List<ReferenciaDoc> DscR = new List<ReferenciaDoc>();

            String Lin = String.Empty;

            String L = extraeLista(data, "ReferenciaDoc");

            if (L.Length == 0) return null;

            int n = occurs(data, '{');
            int i = 0;
            while (i < n)
            {
                Lin = sgteLineaLista(L, i);
                ReferenciaDoc clase = new ReferenciaDoc();
                clase.CodRef = Convert.ToInt32(extraeValorLista(Lin, "CodRef", 3, 0));
                clase.FchRef = extraeValorLista(Lin, "FchRef", 4, 0);
                clase.FolioRef = extraeValorLista(Lin, "FolioRef", 4, 0);
                clase.IdAdicOtr = extraeValorLista(Lin, "IdAdicOtr", 4, 0);
                clase.IndGlobal = Convert.ToInt32(extraeValorLista(Lin, "IndGlobal", 3, 0));
                clase.NroLinRef = Convert.ToInt32(extraeValorLista(Lin, "NroLinRef", 3, 0));
                clase.RazonRef = extraeValorLista(Lin, "RazonRef", 4, 0);
                clase.RUTOtr = extraeValorLista(Lin, "RUTOtr", 4, 0);
                clase.TpoDocRef = extraeValorLista(Lin, "TpoDocRef", 4, 1);
                
                DscR.Add(clase);
                i++;
            }

            return DscR;
        }


        public List<DscRcgGlobal> extraeDscRcgGlobal(String data)
        {
            List<DscRcgGlobal> DscR = new List<DscRcgGlobal>();

            String Lin = String.Empty;

            String L = extraeLista(data, "DscRcgGlobal");

            if (L.Length == 0) return null;

            int n = occurs(data, '{');
            int i = 0;
            while (i < n)
            {
                Lin = sgteLineaLista(L, i);
                DscRcgGlobal clase = new DscRcgGlobal();
                clase.GlosaDR  = extraeValorLista(Lin, "GlosaDR", 4, 0);
                clase.NroLinDR = Convert.ToInt32(extraeValorLista(Lin, "NroLinDR", 3, 0));
                clase.TpoMov   = extraeValorLista(Lin, "TpoMov", 4, 0);
                clase.TpoValor = extraeValorLista(Lin, "TpoValor", 4, 0);
                clase.ValorDR  = Convert.ToInt32(extraeValorLista(Lin, "ValorDR", 3, 0));
                clase.IndExeDR = Convert.ToInt32(extraeValorLista(Lin, "IndExeDR", 3, 1));
                DscR.Add(clase);
                i++;
            }

            return DscR;
        }

        public List<Detalle> extraeDetalle(String data)
        {

            List<Detalle> deta = new List<Detalle>();

            String Lin = String.Empty;

            String L = extraeLista(data, "detalle");

            if (L.Length == 0) return null;

            int n = occurs(L, '{');
            int i = 0;
            while (i < n)
            {
                Lin = sgteLineaLista(L, posOccurn(L,'{',i));
                Detalle det = new Detalle();


                det.NroLinDet = Convert.ToInt32(extraeValorLista(Lin, "NroLinDet", 3, 0));
                det.TpoCodigo = extraeValorLista(Lin, "TpoCodigo", 4, 0);
                det.VlrCodigo = extraeValorLista(Lin, "VlrCodigo", 4, 0);
                det.TpoDocLiq = extraeValorLista(Lin, "TpoDocLiq", 4, 0);
                det.IndExe = extraeValorLista(Lin, "IndExe", 4, 0);
                det.IndAgente = extraeValorLista(Lin, "IndAgente", 4, 0);
                det.MntBaseFaena = Convert.ToInt32(extraeValorLista(Lin, "MntBaseFaena", 3, 0));
                det.MntMargComer = Convert.ToInt32(extraeValorLista(Lin, "MntMargComer", 3, 0));
                det.PrcConsFinal = Convert.ToInt32(extraeValorLista(Lin, "PrcConsFinal", 3, 0));
                det.NmbItem = extraeValorLista(Lin, "NmbItem", 4, 0);
                det.DscItem = extraeValorLista(Lin, "DscItem", 4, 0);
                det.QtyRef = Convert.ToInt32(extraeValorLista(Lin, "QtyRef", 3, 0));
                det.UnmdRef = extraeValorLista(Lin, "UnmdRef", 4, 0);
                det.PrcRef = Convert.ToInt32(extraeValorLista(Lin, "PrcRef", 3, 0));
                det.QtyItem = Convert.ToInt32(extraeValorLista(Lin, "QtyItem", 3, 0));
                det.FchElabor = extraeValorLista(Lin, "FchElabor", 4, 0);
                det.FchVencim = extraeValorLista(Lin, "FchVencim", 4, 0);
                det.UnmdItem = extraeValorLista(Lin, "UnmdItem", 4, 0);
                det.PrcItem = Convert.ToInt32(extraeValorLista(Lin, "PrcItem", 3, 0));
                det.DescuentoPct = Convert.ToInt32(extraeValorLista(Lin, "DescuentoPct", 3, 0));
                det.DescuentoMonto = Convert.ToInt32(extraeValorLista(Lin, "DescuentoMonto", 3, 0));
                det.CodImpAdic = extraeValorLista(Lin, "CodImpAdic", 4, 0);
                det.MontoItem = Convert.ToInt32(extraeValorLista(Lin, "MontoItem", 3, 1));

                deta.Add(det);
                i++;
            }

            return deta;
        }


        public List<ImptoReten> extraeImptoReten(String data)
        {

            List<ImptoReten> impt = new List<ImptoReten>();

            String Lin = String.Empty;

            String L = extraeLista(data, "ImptoReten");

            if (L.Length == 0) return null;

            int n = occurs(data, '{');
            int i = 0;
            while (i < n)
            {
                Lin = sgteLineaLista(L, i);
                ImptoReten imp = new ImptoReten();

                imp.MontoImp = Convert.ToInt32(extraeValorLista(Lin, "MontoImp", 3, 0));
                imp.TasaImp = Convert.ToInt32(extraeValorLista(Lin, "TasaImp", 3, 0));
                imp.TipoImp = extraeValorLista(Lin, "TipoImp", 4, 1);
                
                impt.Add(imp);
                i++;
            }

            return impt;
        }




        public List<MntPagos> extraeMntPagos(String data)
        {
            
            List<MntPagos> mnts = new List<MntPagos>();
            
            String Lin = String.Empty;

            String L = extraeLista(data, "MntPagos");

            if (L.Length == 0) return null;

            int n = occurs(data, '{');
            int i = 0;
            while( i < n)
            {
                Lin = sgteLineaLista(L, i);
                MntPagos pago = new MntPagos();
                pago.MntPago = Convert.ToInt32(extraeValorLista(Lin,"MntPago",3,0));
                pago.FchPago = extraeValorLista(Lin, "FchPago", 4, 1);

                mnts.Add(pago);
                i++;
            }


            return mnts;
        }

        public String extraeLista(String data, String nombLista)
        {
            String lista = String.Empty;
            int pos = data.IndexOf(nombLista);

            lista = data.Substring(pos + nombLista.Length + 8, posSgteCharJson(data.Substring(pos + nombLista.Length, data.Length - (pos + nombLista.Length)), ']'));

            return lista;
        }

        public String sgteLineaLista(String lista, int pos)
        {
            String linea = String.Empty;
            linea = lista.Substring(pos, posSgteCharJson(lista.Substring(pos , lista.Length - pos), '}'));
            return linea;
        }

        public String extraeValorLista(String data, String Campo, int start, int ultimo)
        {
            String resultado = String.Empty;
            int pos = data.IndexOf(Campo);


            if (pos == -1)
            {
                if (start == 4)
                    return resultado;
                else
                    return "0";
            }

            if (start == 4)
            {
                resultado = data.Substring(pos + Campo.Length + start, posSgteCharJson(data.Substring(pos + Campo.Length + start, data.Length - (pos + Campo.Length + start)), ',') + ultimo);
                if(resultado.Length>0) resultado = resultado.Substring(0, resultado.Length - 1);
            }
            else
            {
                resultado = data.Substring(pos + Campo.Length + start, posSgteCharJson(data.Substring(pos + Campo.Length + start, data.Length - (pos + Campo.Length + start)), ',') + ultimo);
                if (resultado == "") resultado = "0" ;
            }

            if (resultado.StartsWith("\"")) resultado = String.Empty;
            return resultado;
        }

        public String extraeValorJson(String data, String Campo, int start, int largo)
        {
            String resultado = String.Empty;
            int pos = data.IndexOf(Campo);

            if (pos == -1)
            {
                if (start == 4)
                    return resultado;
                else
                    return "0";
            }

            if (start == 4)
                resultado = data.Substring(pos + Campo.Length + start, posSgteCharJson(data.Substring(pos + Campo.Length + start, data.Length - (pos + Campo.Length + start)), '\r') - 2);
            else
            {
                resultado = data.Substring(pos + Campo.Length + start, posSgteCharJson(data.Substring(pos + Campo.Length + start, data.Length - (pos + Campo.Length + start)), '\r')-1);
            }

            if (resultado.StartsWith("\"")) resultado = String.Empty;
            return resultado;
        }

        public int posSgteCharJson(String Data, char busca)
        {
            if (Data.Length == 0) return 0; 

            int i = 0;
            while ((Data[i] != busca) && (i < Data.Length-1))
            {
                i++;
                
            }
            return i;
        }

        public int occurs(String Data, char busca)
        {
            int c = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                char n = Data[i];
                if (n == busca) c++;
            }

            return c;
        }

        public int posOccurn(String Data, char busca, int occu)
        {
            int c = 0;
            int i = 0;
            while(c <= occu)
            {
                if (Data[i] == busca) c++;
                i++;
            }

            return i;
        }

    }

//############################## Area Clases #################################################################################


    [DataContract]
    class Detalle
    {
        [DataMember]
        public int NroLinDet { get; set; } //Número del ítem. Desde 1 a 60
        [DataMember]
        public string TpoCodigo { get; set; } //Tipo de codificación utilizada para el ítem Standard: EAN, PLU, DUN o Interna (Hasta 5 tipos de códigos).... este puede ser una clase...
        [DataMember]       
        public string VlrCodigo { get; set; } // Código del producto de acuerdo a tipo de codificación indicada en campo anterior (Hasta 5 códigos)
        [DataMember] 
        public string TpoDocLiq { get; set; } // Para liquidaciones se debe registrar el código del docto. que se liquida. (Ej: :30, 33, 35, 39, 56,etc.) 
        [DataMember]
        public string IndExe { get; set; } // Indica si el producto o servicio es exento o no afecto a impuesto o si ya ha sido facturado. 
                       //(También se utiliza para indicar garantía de depósito por envases. Art.28,Inc3 Reglamento DL 825) 
                       //(Cervezas, Jugos, Aguas Minerales, Bebidas Analcohólicas u otros autorizados por Resolución especial) 
                       //Si todos los ítems de una factura tienen valor 1 en este indicador la factura no puede ser factura electrónica (código 33),
                       //debería serfactura exenta (código 34) . Sólo en caso de Liquidación-Factura
                       // que tenga ítems no facturables negativos, se debe señalar el indicador 2, e informar el valor con signo negativo
        [DataMember] 
        public string IndAgente { get; set; } //Obligatorio para agentes retenedores, indica para cada transacción si es agente retenedor del producto que está vendiendo
        [DataMember] 
        public int MntBaseFaena { get; set; } //Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 17
        [DataMember] 
        public int MntMargComer { get; set; } // Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 14 y 50
        [DataMember] 
        public int PrcConsFinal { get; set; } // Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 14, 17 y 50
        [DataMember]
        public string NmbItem { get; set; } //Nombre del producto o servicio
        [DataMember] 
        public string DscItem { get; set; } // Descripción Adicional del producto o servicio. Se utiliza para pack, servicios con detalle
        [DataMember] 
        public int QtyRef { get; set; } // Cantidad para la unidad de medida de referencia (no se usa para el cálculo del valor neto) en 12 enteros y 6 decimales.
                    // Obligatorio para facturas de venta o compra que indican emisor opera como Agente Retenedor
        [DataMember] 
        public string UnmdRef { get; set; } //Glosa con unidad de medida de referencia Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor
        [DataMember] 
        public float PrcRef { get; set; } // Precio unitario para la unidad de medida de referencia (no se usa para el cálculo del valor neto) 12 enteros, 6 decimales. Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor 
        [DataMember]
        public float QtyItem { get; set; } // Cantidad del ítem en 12 enteros y 6 decimales Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor
        [DataMember] 
        public string FchElabor { get; set; } // del item
        [DataMember] 
        public string FchVencim { get; set; } // del item
        [DataMember]
        public string UnmdItem { get; set; } // unidad de medidas
        [DataMember]
        public float PrcItem { get; set; } // Precio
        [DataMember] 
        public float DescuentoPct { get; set; } // Descuento (%) en 3 enteros y 2 decimales
        [DataMember] 
        public int DescuentoMonto { get; set; } //Correspondiente al anterior. Totaliza todos los descuentos otorgados al ítem
        [DataMember]
        public string CodImpAdic { get; set; } //Indica el código según tabla de códigos (Ver en Índice 4.- Codificación Tipos de Impuesto).
        [DataMember]
        public int MontoItem { get; set; } //(Precio Unitario * Cantidad ) – Monto Descuento + Monto Recargo

    }

//######################################## Sub Totales Informativos ###########################################################################
     [DataContract]
    class MntPagos
    {
        [DataMember]
        public string FchPago { get; set; } //
        [DataMember]
        public int MntPago { get; set; } //    


    }


     [DataContract]
     class ImptoReten
    {
        [DataMember]
        public string TipoImp { get; set; } //Código del impuesto o retención de acuerdo a la codificación detallada en tabla de códigos (ver Punto 4 del índice). Incluye Retención de Cambio sujeto de Construcción
        [DataMember]
        public int TasaImp { get; set; } //Se debe indicar la tasa de Impuesto adicional o retención. En el caso de impuesto específicos se puede omitir
        [DataMember]
        public int MontoImp { get; set; } // Valor del impuesto o retención asociado al código indicado anteriormente
    }

     [DataContract]
     class DscRcgGlobal
     {
         [DataMember]
         public int NroLinDR { get; set; }
         [DataMember]
         public string TpoMov { get; set; }
         [DataMember]
         public string GlosaDR { get; set; }
         [DataMember]
         public string TpoValor { get; set; }
         [DataMember]
         public int ValorDR { get; set; }
         [DataMember]
         public int IndExeDR { get; set; }

     }


     [DataContract]
     class ReferenciaDoc
     {
         [DataMember]
         public int NroLinRef { get; set; }
         [DataMember]
         public string TpoDocRef { get; set; }
         [DataMember]
         public int IndGlobal { get; set; }
         [DataMember]
         public string FolioRef { get; set; }
         [DataMember]
         public string RUTOtr { get; set; }
         [DataMember]
         public string IdAdicOtr { get; set; }
         [DataMember]
         public string FchRef { get; set; }
         [DataMember]
         public int CodRef { get; set; }
         [DataMember]
         public string RazonRef { get; set; }
     }


     [DataContract]
     class Comisiones
     {
         [DataMember]
         public int NroLinCom { get; set; }
         [DataMember]
         public string TipoMovim { get; set; }
         [DataMember]
         public string Glosa { get; set; }
         [DataMember]
         public int ValComNeto { get; set; }
         [DataMember]
         public int ValComExe { get; set; }
         [DataMember]
         public int ValComIVA { get; set; }

     }




}
