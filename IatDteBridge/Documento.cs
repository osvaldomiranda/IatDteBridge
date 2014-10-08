using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class Documento
    {
        public int TipoDte { get; set; } // 
        public int Folio{get;set;}
        public String FchEmis { get; set; }
        public int IndNoRebaja { get; set; }
        public int TipoDespacho { get; set; }
        public int IndTraslado { get; set; }
        public String TpoImpresion { get; set; }
        public int IndServicio { get; set; }
        public int MntBruto { get; set; }
        public int FmaPago { get; set; }
        public int FmaPagoExp { get; set; }
        public DateTime FchCancel { get; set; }
        public int MntCancel { get; set; }
        public int SaldoInsol { get; set; }
        public DateTime FchPago { get; set; }
        public int MntPago { get; set; }
        public String GlosaPago { get; set; }
        public DateTime PeriodoDesde { get; set; }
        public DateTime PeriodoHasta { get; set; }
        public String MedioPago { get; set; }
        public String TipoCtaPago { get; set; }
        public String NumCtaPago { get; set; }
        public String BcoPago { get; set; }
        public String TermPagoCdg { get; set; }
        public String TermPagoGlosa { get; set; }
        public int TermPagoDias { get; set; }
        public string FchVenc { get; set; }
//#################################### Area Emisor ####################################################################
        public string RUTEmisor { get; set; }
        public string RznSoc { get; set; }
        public string GiroEmis { get; set; }
        public string Telefono { get; set; }
        public string CorreoEmisor { get; set; }
        public int Acteco { get; set; } // Actividad Economica
        public int CdgTraslado { get; set; } //solo para guia de despacho
        public int FolioAut { get; set; } //Solo para guia de despacho.
        public string FchAut { get; set; } //
        public string Sucursal { get; set; } //Nombre de la sucursal que emite el documento
        public int CdgSIISucur { get; set; } // Codigo de sucursal que emite el documento
        public string CodAdicSucur { get; set; } //Codigo para uso libre
        public string DirOrigen { get; set; } // Direccion desde donde se despachan
        public string CmnaOrigen { get; set; } // Analogo a direccion de origen
        public string CiudadOrigen { get; set; } // Analogo a direccion de origen
        public int CdgVendedor { get; set; } // Identificador del vendedor
        public string IdAdicEmisor { get; set; } // adicional para uso libre
        public string RUTMandante { get; set; }
//################################### Area Receptor ############################################################################
        public string RUTRecep { get; set; } // rut del cliente en la factura de compra se referencia al vendedor
        public string CdgIntRecep { get; set;  } // para identificacion interna de receptor
        public string RznSocRecep { get; set; } // Razon Social Receptor
        public string NumId { get; set; } // Numero o codigo de identificacion personal del receptor extrangero otorgado por la adm. tributaria
        public string Nacionalidad { get; set; } // Nacionalidad del extrangero
        public string IdAdicRecep { get; set; } // solo para exportacion uso libre
        public string GiroRecep { get; set;  } // glosa giro del receptor
        public string Contacto { get; set;  } // Glosa con nombre o telefono del contacto de la empresa receptor "Atencion a:"
        public string CorreoRecep { get; set; } // e-mail de contacto en empresa del receptor (para registrar el “Atención A:”)
        public string DirRecep { get; set;  } // Dirección Legal del Receptor (registrada en el SII) En caso de documentos de exportación, corresponde a la dirección en el extranjero del Receptor
        public string CmnaRecep { get; set; } // Análogo a Dirección Receptor
        public string CiudadRecep { get; set; } //Análogo a Dirección Receptor
        public string DirPostal { get; set; } // Análogo a Dirección Recepto
        public string CmnaPostal { get; set; } // Análogo a Dirección Receptor
        public string CiudadPostal { get; set; } // Análogo a Dirección Receptor
        public string RUTSolicita { get; set; } // En casos de venta a público. Es obligatorio si es distinto de Rut receptor o Rut Receptor es persona jurídica. Con guión y dígito verificador
//################################# Area Transporte #############################################################################       
        public string Patente { get; set; } // 
        public string RUTTrans { get; set; } // Con guión y dígito verificador Indicador Tipo de Despacho es 2 o 3
        public string RUTChofer { get; set; } // 
        public string NombreChofer { get; set; } // 
        public string DirDest { get; set; } // Datos correspondientes a Dirección destino en documento que acompaña productos o a la Dirección en que se otorga el servicio en caso de Servicios periódicos.
        public string CmnaDest { get; set; } // Análogo Dirección Destino
        public string CiudadDest { get; set; } // Análogo Dirección Destino
        public int CodModVenta { get; set; } // Para doctos. utilizados en exportación. Se refiere a si la exportación se realiza bajo venta, En consignación, a firme, en Consignación con mínimo afirme, etc.)
        public int CodClauVenta { get; set; } // Se refiere a la cláusula de venta indicada en el DUS ( FOB, CIF, etc.)
        public int TotClauVenta { get; set; } // Corresponde al valor total de la exportación a pagar por el importador según la cláusula de venta acordada entre las partes y que se indica en el DUS. (No incluye comisiones ni otros gastos deducibles en el exterior)
        public int CodViaTransp { get; set; } // Corresponde a la vía de transporte por donde se envía la mercadería (aéreo, terrestre, marítimo, etc) al Extranjero
        public string NombreTransp { get; set; } // Corresponde al nombre o glosa de la nave transportista.
        public string RUTCiaTransp { get; set; } // Para doctos. utilizados en exportación. Señale el Rol Unico Tributario (RUT) de la compañía transportista indicada en el DUS. Si ésta es extranjera, señale el RUT de la Agencia que la representa en Chile.
        public string NomCiaTransp { get; set; } // Nombre de la Cía. transportadora declarada en el DUS.
        public string IdAdicTransp { get; set; } // Identificación adicional para uso libre
        public string Booking { get; set; } // Número de Booking o Reserva del operador
        public string Operador { get; set; } // Código de Operador
        public int CodPtoEmbarque { get; set; } // Puerto de embarque de mercancías
        public string IdAdicPtoEmb { get; set; } // Identificación adicional para uso libre
        public string CodPtoDesemb { get; set; } // 
        public string IdAdicPtoDesemb { get; set; } // Identificación adicional para uso libre
        public int Tara { get; set; }//
        public int CodUnidMedTara { get; set; } // Indique la unidad de medida en la que se encuentra expresado la Tara
        public int PesoBruto { get; set; } // Señale con dos decimales, la sumatoria de los pesos brutos de todos los ítems del documento. 
        public int CodUnidPesoBruto { get; set; } // Indique la unidad de medida en la que se encuentra el peso bruto de la mercadería
        public int PesoNeto { get; set; } // Señale con dos decimales, la sumatoria del peso neto de todos los ítems del documento.
        public int CodUnidPesoNeto { get; set; } // Indique la unidad de medida en la que se encuentra el peso neto de la mercadería
        public int TotItems { get; set; } // Indique el total de ítems del documento
        public int TotBultos { get; set; } // Señale la cantidad total de bultos que ampara el documento.

// ############################# Area Totales ####################################################################################
       
        public string TpoMoneda { get; set;  } // Moneda en que se registra la transacción de exportación.
        public int MntNeto { get; set; } // Suma de valores total de ítems afectos -descuentos globales + recargos globales (Asignados a ítems afectos). Si está encendido el Indicador de Montos Brutos (=1) entonces el resultado anterior se debe dividir por (1 + tasa de IVA)
        public int MntExe { get; set; } // Suma de valores total de ítems no afectos o exentos -descuentos globales + recargos globales (Asignados a ítems exentos o no afectos)
        public int MntBase { get; set; } // Monto informado
        public int MntMargenCom { get; set; } // Monto informado
        public float TasaIVA { get; set; } // 
        public int IVA { get; set; } // 
        public int IVAProp { get; set; } //Las empresas que venden por cuenta de un mandatario, pueden opcional separar el IVA en propio y de terceros. En todos estos casos el campo “IVA” debe contener el IVA total de la Factura
        public int IVATerc { get; set; } // Ídem al anterior
        // impuestos adicionales puede ser mas de uno por ese motivo se crea una clase
        imp_adicional[] impuestos_adicionales;
        public int IVANoRet { get; set; } // Sólo en facturas de Compra en que hay retención de IVA por el emisor y Notas de Crédito o débito que referencian facturas de compra. No se registra si es igual a 0.
        public int CredEC { get; set; } // Artículo 21 del decreto ley N° 910/75. Este Es el único código que opera en forma opuesta al resto, ya que se resta al IVA general
        public int GrntDep { get; set; } // Sólo para empresas que usen envases en forma habitual, por su giro principal. Art.28,Inc3 Reglamento DL 825) (Cervezas, Jugos, Aguas Minerales, Bebidas Analcohólicas u otros autorizados por Resolución especial). Corresponde a la Sumatoria de las líneas de detalle que indican Indicador de facturación/ exención = 3        
        public int ValComNeto { get; set; }  // Suma de detalle de Valores de Comisiones y Otros Cargos
        public int ValComExe { get; set; } // Suma de detalles de valores de comisiones y otros cargos no afectos o exentos
        public int ValComIVA { get; set; } // Suma de detalle de IVA de Valor de Comisiones y Otros Cargos
        public int MntTotal { get; set; } // Monto neto + Monto no afecto o  exento + IVA + Impuestos Adicionales + Impuestos Específicos + Iva Margen Comercialización +IVA Anticipado + Garantía por depósito de envases o embalajes - Crédito empresas constructoras- IVA Retenido productos (en caso de facturas de compra) -  Valor Neto Comisiones y Otros Cargos- IVA Comisiones y Otros Cargos - Valor Comisiones y Otros Cargos No Afectos o Exentos. (Los Impuestos Adicionales y el IVA Anticipado están detallados en la TABLA de Impuestos Adicionales y Retenciones)
        public int MontoNF { get; set; } //Suma de montos de bienes o servicios con Indicador de facturación/ exención = 2 menos Suma de montos de bienes o servicios con Indicador de facturación/ exención = 6
        public int MontoPeriodo { get; set; } // Monto Total + Monto no Facturable
        public int SaldoAnterior { get; set; } // Saldo Anterior. Se incluye sólo con fines de ilustrar con claridad el cobro.
        public int VlrPagar { get; set; } // valor cobrado

// ################################# Area Otra Moneda ##########################################################################

        public string TpoMonedas { get; set; } // Moneda alternativa en que se expresan los Montos Totales.
        public int TpoCambio { get; set; } // 1. Factor de conversión utilizado de pesos a Otra moneda 2. 6 enteros y 4 decimales 3. En documentos de Exportación corresponde al tipo de cambio de la fecha de emisión del documento, publicado por
        // Faltan campos pero en esta etapa no es importante


 //############################## Area Detalle #################################################################################
     
        public List<det_documento> detalles = new List<det_documento>();
        
    }



    class det_documento
    {

        public int NroLinDet { get; set; } //Número del ítem. Desde 1 a 60
        public string TpoCodigo { get; set; } //Tipo de codificación utilizada para el ítem Standard: EAN, PLU, DUN o Interna (Hasta 5 tipos de códigos).... este puede ser una clase...
        public string VlrCodigo { get; set; } // Código del producto de acuerdo a tipo de codificación indicada en campo anterior (Hasta 5 códigos)
        public string TpoDocLiq { get; set; } // Para liquidaciones se debe registrar el código del docto. que se liquida. (Ej: :30, 33, 35, 39, 56,etc.) 
        public string IndExe { get; set; } // Indica si el producto o servicio es exento o no afecto a impuesto o si ya ha sido facturado. 
                       //(También se utiliza para indicar garantía de depósito por envases. Art.28,Inc3 Reglamento DL 825) 
                       //(Cervezas, Jugos, Aguas Minerales, Bebidas Analcohólicas u otros autorizados por Resolución especial) 
                       //Si todos los ítems de una factura tienen valor 1 en este indicador la factura no puede ser factura electrónica (código 33),
                       //debería serfactura exenta (código 34) . Sólo en caso de Liquidación-Factura
                       // que tenga ítems no facturables negativos, se debe señalar el indicador 2, e informar el valor con signo negativo
        public string IndAgente { get; set; } //Obligatorio para agentes retenedores, indica para cada transacción si es agente retenedor del producto que está vendiendo
        public int MntBaseFaena { get; set; } //Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 17
        public int MntMargComer { get; set; } // Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 14 y 50
        public int PrcConsFinal { get; set; } // Sólo para transacciones realizadas por Agentes Retenedores, según códigos de retención 14, 17 y 50
        public string NmbItem { get; set; } //Nombre del producto o servicio
        public string DscItem { get; set; } // Descripción Adicional del producto o servicio. Se utiliza para pack, servicios con detalle
        public int QtyRef { get; set; } // Cantidad para la unidad de medida de referencia (no se usa para el cálculo del valor neto) en 12 enteros y 6 decimales.
                    // Obligatorio para facturas de venta o compra que indican emisor opera como Agente Retenedor
        public string UnmdRef { get; set; } //Glosa con unidad de medida de referencia Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor
        public float PrcRef { get; set; } // Precio unitario para la unidad de medida de referencia (no se usa para el cálculo del valor neto) 12 enteros, 6 decimales. Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor 
        public float QtyItem { get; set; } // Cantidad del ítem en 12 enteros y 6 decimales Obligatorio para facturas de venta, compra o notas que indican emisor opera como Agente Retenedor
        public string FchElabor { get; set; } // del item
        public string FchVencim { get; set; } // del item
        public string UnmdItem { get; set; } // unidad de medidas
        public float PrcItem { get; set; } // Precio
        public float DescuentoPct { get; set; } // Descuento (%) en 3 enteros y 2 decimales
        public int DescuentoMonto { get; set; } //Correspondiente al anterior. Totaliza todos los descuentos otorgados al ítem
        public string CodImpAdic { get; set; } //Indica el código según tabla de códigos (Ver en Índice 4.- Codificación Tipos de Impuesto).
        public int MontoItem { get; set; } //(Precio Unitario * Cantidad ) – Monto Descuento + Monto Recargo

        public List<imp_adicional> impuestos = new List<imp_adicional>();
        

        public det_documento(string det)
        {

            List<string> d = det.Split(';').ToList();

            int i=0;
            foreach (var item in d)
            {
                switch (i)
                {
                    case 1:
                        this.NroLinDet = Convert.ToInt32(item);
                        break;
                    case 2:
                        this.TpoCodigo = item;
                        break;
                    case 10:
                        //this.VlrCodigo = item;
                        break;
                    case 14:
                       // this.TpoDocLiq = item;
                        break;
                    case 19:
                        this.IndExe = item;
                        break;
                    case 6:
                        //this.IndAgente = item;
                        break;
                    case 7:
                        //this.MntBaseFaena = Convert.ToInt32(item);
                        break;
                    case 8:
                        //this.MntMargComer = Convert.ToInt32(item);
                        break;
                    case 9:
                        //this.PrcConsFinal = Convert.ToInt32(item);
                        break;
                    case 3:
                        this.NmbItem = item;
                        break;
                    case 23:
                        this.DscItem = item;
                        break;
                    case 12:
                        this.QtyRef = Convert.ToInt32(item);
                        break;
                    case 18:
                        this.PrcRef = Convert.ToInt32(item);
                        break;
                    case 4:
                        this.QtyItem = Convert.ToInt32(item);
                        break;
                    case 15:
                        this.UnmdRef = item;
                        break;
                    case 16:
                        this.FchElabor = item;
                        break;
                    case 17:
                        this.FchVencim = item;
                        break;
                    case 13:
                        this.UnmdItem = item;
                        break;
                    case 5:
                        this.PrcItem = Convert.ToInt32(item);
                        break;
                    case 20:
                        this.DescuentoPct = Convert.ToInt32(item);
                        break;
                    case 21:
                        this.DescuentoMonto = Convert.ToInt32(item);
                        break;
                    case 22:
                        this.CodImpAdic = item;
                        break;
                    case 11:
                        this.MontoItem = Convert.ToInt32(item);
                        break; 
     
                }
            }
        }

    }

//######################################## Sub Totales Informativos ###########################################################################
    class imp_adicional
    {
        public string TipoImp { get; set; } //Código del impuesto o retención de acuerdo a la codificación detallada en tabla de códigos (ver Punto 4 del índice). Incluye Retención de Cambio sujeto de Construcción
        public int TasaImp { get; set; } //Se debe indicar la tasa de Impuesto adicional o retención. En el caso de impuesto específicos se puede omitir
        public int MontoImp { get; set; } // Valor del impuesto o retención asociado al código indicado anteriormente

        public imp_adicional(String TipoImp, int TasaImp, int MontoImp)
        {
            this.TipoImp  = TipoImp;
            this.TasaImp  = TasaImp;
            this.MontoImp = MontoImp;

        }
    }


}
