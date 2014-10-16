using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace IatDteBridge
{
    [DataContract]
    class Iec
    {
        [DataMember]
        public CaratulaIec caratulaIec;
        [DataMember]
        public TotalPeriodoIec totalPeriodoIec;
        [DataMember]
        public DetalleIec detalleIec;
    }
    [DataContract]
    class CaratulaIec
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
        public int FolioNotificacion { get; set; }
        [DataMember]
        public string CodAutRec { get; set; }
    }
    [DataContract]
    class TotalPeriodoIec
    {
        [DataMember]
        public int TpoDoc { get; set; }
        [DataMember]
        public int TpoImp { get; set; }
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
        public int TotOpIVARec { get; set; }
        [DataMember]
        public int TotMntIVA { get; set; }
        [DataMember]
        public int TotOpActivoFijo { get; set; }
        [DataMember]
        public int TotMntActivoFijo { get; set; }
        [DataMember]
        public int TotMntIVAActivoFijo { get; set; }
        //TotIVANoRec
        [DataMember]
        public List<TotIVANoRec> totIVANoRec = new List<TotIVANoRec>();
        [DataMember]
        public int TotOpIVAUsoComun { get; set; }
        [DataMember]
        public int TotIVAUsoComun { get; set; }
        [DataMember]
        public int FctProp { get; set; }
        [DataMember]
        public int TotCredIVAUsoComun { get; set; }
        //TotOtrosImp
        [DataMember]
        public List<TotOtroImpIec> totOtroImp = new List<TotOtroImpIec>(); 
        [DataMember]
        public int TotImpSinCredito { get; set; }
        [DataMember]
        public int TotMntTotal { get; set; }
        [DataMember]
        public int TotIVANoRetenido { get; set; }
        [DataMember]
        public int TotTabPuros { get; set; }
        [DataMember]
        public int TotTabCigarrillos { get; set; }
        [DataMember]
        public int TotTabElaborado { get; set; }
        [DataMember]
        public int TotImpVehiculo { get; set; }
    }
    [DataContract]
    class DetalleIec
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
        public int TpoImp { get; set; }
        [DataMember]
        public int TasaImp { get; set; }
        [DataMember]
        public int NumInt { get; set; }
        [DataMember]
        public int FchDoc { get; set; }
        [DataMember]
        public int CdgSIISucur { get; set; }
        [DataMember]
        public int RUTDoc { get; set; }
        [DataMember]
        public string RznSoc { get; set; }
        [DataMember]
        public int MntExe { get; set; }
        [DataMember]
        public int MntNeto { get; set; }
        [DataMember]
        public int MntIVA { get; set; }
        [DataMember]
        public int MntActivoFijo { get; set; }
        [DataMember]
        public int MntIVAActivoFijo { get; set; }
        //IVANoRec
        [DataMember]
        public List<IVANoRec> ivaNoRec = new List<IVANoRec>();
        [DataMember]
        public int IVAUsoComun { get; set; }
        //OtrosImp
        [DataMember]
        public List<OtrosImpIec> otrosImp = new List<OtrosImpIec>();
        [DataMember]
        public int MntSinCred { get; set; }
        [DataMember]
        public int MntTotal { get; set; }
        [DataMember]
        public int IVANoRetenido { get; set; }
        [DataMember]
        public int TabPuros { get; set; }
        [DataMember]
        public int TabCigarrillos { get; set; }
        [DataMember]
        public int TabElaborado { get; set; }
        [DataMember]
        public int ImpVehiculo { get; set; }

    }
    [DataContract]
    class TotIVANoRec
    {
        [DataMember]
        public int CodIVANoRec { get; set; }
        [DataMember]
        public int TotOpIVANoRec { get; set; }
        [DataMember]
        public int TotMntIVANoRec { get; set; }
    }
    [DataContract]
    class TotOtroImpIec
    {
        [DataMember]
        public int CodImp { get; set; }
        [DataMember]
        public int TotMntImp { get; set; }
        [DataMember]
        public int FctImpAdic { get; set; }
        [DataMember]
        public int TotCredImp { get; set; }
    }
    [DataContract]
    class IVANoRec
    {
        [DataMember]
        public int CodIVANoRec { get; set; }
        [DataMember]
        public int MntIVANoRec { get; set; }
    }
    [DataContract]
    class OtrosImpIec
    {
        [DataMember]
        public int CodImp { get; set; }
        [DataMember]
        public int TasaImp { get; set; }
        [DataMember]
        public int MntImp { get; set; }
    }

     
}
