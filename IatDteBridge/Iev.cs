using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IatDteBridge
{
    [DataContract]
    class Iev
    {
        [DataMember]
        public Caratula caratula;
        [DataMember]
        public TotalPeriodo totalesperiodo;
        [DataMember]
        public DetalleIev detalle;

    }
    [DataContract]
    class Caratula
    {
        [DataMember]
        public string RutEmisorLibro { get; set; }
        [DataMember]
        public string RutEnvia { get; set; }
        [DataMember]
        public string PeriodoTributario { get; set; }
        [DataMember]
        public string FchResol { get; set; }
        [DataMember]
        public int NroResol { get; set; }
        [DataMember]
        public string TipoOperacion { get; set; }
        [DataMember]
        public string TipoLibro { get; set; }
        [DataMember]
        public string TipoEnvio { get; set; }
        [DataMember]
        public int NroSegmento { get; set; }
        [DataMember]
        public string FolioNotificacion { get; set; }
        [DataMember]
        public string CodAutRec { get; set; }

    }

    [DataContract]
    class TotalPeriodo
    {
        [DataMember]
        public int TpoDoc { get; set; }
        [DataMember]
        public int TotDoc { get; set; }
        [DataMember]
        public int TotAnulado { get; set; }
        [DataMember]
        public int TotOpExe { get; set; }
        [DataMember]
        public int TotMntExe { get; set; }
        [DataMember]
        public int TotMntNeto { get; set; }
        [DataMember]
        public int TotMntIVA { get; set; }
        [DataMember]
        public int TotIVAFueraPlazo { get; set; }
        [DataMember]
        public int TotIVAPropio { get; set; }
        [DataMember]
        public int TotIVATerceros { get; set; }
        [DataMember]
        public int TotLey18211 { get; set; }
        [DataMember]
        public List<TotOtroImp> totOtroImp = new List<TotOtroImp>();
        [DataMember]
        public int TotOpIVARetTotal { get; set; }
        [DataMember]
        public int TotIVARetTotal { get; set; }
        [DataMember]
        public int TotOpIVARetParcial { get; set; }
        [DataMember]
        public int TotIVARetParcial { get; set; }
        [DataMember]
        public int TotCredEC { get; set; }
        [DataMember]
        public int TotDepEnvase { get; set; }
        [DataMember]
        public List<TotLiquidaciones> totLiquidaciones = new List<TotLiquidaciones>();
        [DataMember]
        public int TotMntTotal { get; set; }
        [DataMember]
        public int TotOpIVANoRetenido { get; set; }
        [DataMember]
        public int TotIVANoRetenido { get; set; }
        [DataMember]
        public int TotMntNoFact { get; set; }
        [DataMember]
        public int TotMntPeriodo { get; set; }
        [DataMember]
        public int TotPsjNac { get; set; }
        [DataMember]
        public int TotPsjInt { get; set; }
    }
    [DataContract]
    class DetalleIev
    {
        [DataMember]
        public int TpoDoc { get; set; }
        [DataMember]
        public int Emisor { get; set; }
        [DataMember]
        public int NroDoc { get; set; }
        [DataMember]
        public string Anulado { get; set; }
        [DataMember]
        public int Operacion { get; set; }
        [DataMember]
        public int TasaImp { get; set; }
        [DataMember]
        public int NumInt { get; set; }
        [DataMember]
        public int IndServicio { get; set; }
        [DataMember]
        public int IndSinCosto { get; set; }
        [DataMember]
        public string FchDoc { get; set; }
        [DataMember]
        public int CdgSIISucur { get; set; }
        [DataMember]
        public string RUTDoc { get; set; }
        [DataMember]
        public string RznSoc { get; set; }
        [DataMember]
        public int NumId { get; set; }
        [DataMember]
        public int Nacionalidad { get; set; }
        [DataMember]
        public int TpoDocRef { get; set; }
        [DataMember]
        public int FolioDocRef { get; set; }
        [DataMember]
        public int MntExe { get; set; }
        [DataMember]
        public int MntNeto { get; set; }
        [DataMember]
        public int MntIVA { get; set; }
        [DataMember]
        public int IVAFueraPlazo { get; set; }
        [DataMember]
        public int IVAPropio { get; set; }
        [DataMember]
        public int IVATerceros { get; set; }
        [DataMember]
        public int Ley18211 { get; set; }
        [DataMember]
        public List<OtrosImp> otrosImp = new List<OtrosImp>();
        [DataMember]
        public int IVARetTotal { get; set; }
        [DataMember]
        public int IVARetParcial { get; set; }
        [DataMember]
        public int CredEC { get; set; }
        [DataMember]
        public int DepEnvase { get; set; }
        [DataMember]
        public List<Liquidaciones> liquidaciones = new List<Liquidaciones>();
        [DataMember]
        public int MntTotal { get; set; }
        [DataMember]
        public int IVANoRetenido { get; set; }
        [DataMember]
        public int MntNoFact { get; set; }
        [DataMember]
        public int MntPeriodo { get; set; }
        [DataMember]
        public int PsjNac { get; set; }
        [DataMember]
        public int PsjInt { get; set; }
    }
    [DataContract]
    class TotOtroImp
    {
        [DataMember]
        public int CodImp { get; set; }
        [DataMember]
        public int TotMntImp { get; set; }
    }
    [DataContract]
    class TotLiquidaciones
    {
        [DataMember]
        public int TotValComNeto { get; set; }
        [DataMember]
        public int TotValComExe { get; set; }
        [DataMember]
        public int TotValComIVA { get; set; }        
    }
    [DataContract]
    class OtrosImp
    {
        [DataMember]
        public int CodImp { get; set; }
        [DataMember]
        public int TasaImp { get; set; }
        [DataMember]
        public int MntImp { get; set; }
    
    }
   [DataContract]
    class Liquidaciones
    {
        [DataMember]
        public int RutEmisor { get; set; }
        [DataMember]
        public int ValComNeto { get; set; }
        [DataMember]
        public int ValComExe { get; set; }
        [DataMember]
        public int ValComIVA { get; set; }    
    }
}
