using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IatDteBridge
{

    [DataContract]
    class LibroVenta
    {
        [DataMember]
        public String RutEmisorLibro{get;set;}
        [DataMember]
	    public String RutEnvia {get;set;}
        [DataMember]
	    public String PeriodoTributario{get;set;}
        [DataMember]
	    public String FchResol{get;set;}
        [DataMember]
	    public int    NroResol{get;set;}
        [DataMember]
	    public String TipoOperacion{get;set;}
        [DataMember]
	    public String TipoLibro {get;set;}
        [DataMember]
	    public String TipoEnvio {get;set;}
        [DataMember]
	    public int    NroSegmento{get;set;}
        [DataMember]
	    public int    FolioNotificacion{get;set;}
        [DataMember]
	    public String CodAutRec{get;set;}
             
        [DataMember]
        public List<resumenSeg> ResumenSegmento = new List<resumenSeg>();

        [DataMember]
        public List<totalesPer> TotalesPeriodo = new List<totalesPer>();
	 	     
        [DataMember]
        public List<detalle> Detalle = new List<detalle>();
	 	       
        [DataMember]
        public float MntTotal{get;set;}
        [DataMember]
	    public float IVANoRetenido {get;set;}
        [DataMember]
	    public float MntNoFact {get;set;}
        [DataMember]
	    public float MntPeriodo{get;set;}
        [DataMember]
	    public float PsjNac {get;set;}
        [DataMember]
	    public float PsjInt{get;set;}
        
    }

    [DataContract]
    class resumenSeg
    {
        [DataMember]
        public int TpoDoc{get;set;}
        [DataMember]
        public int TotDoc{get;set;}
        [DataMember] 
        public float TotAnulado{get;set;}
        [DataMember]
        public float TotMntExe{get;set;}
        [DataMember]
        public float TotMntNeto{get;set;}
        [DataMember]
        public float TotMntIVA{get;set;}
        [DataMember]
        public float TotIVAFueraPlazo{get;set;}
        [DataMember]
        public float TotIVAPropio{get;set;}
        [DataMember]
        public float TotIVATerceros{get;set;}
        [DataMember]
        public float TotLey18211{get;set;}
        

        [DataMember]
        public List<totOtrosImp> TotOtrosImp = new List<totOtrosImp>();
        
        [DataMember]
        public float TotOpIVARetTotal{get;set;}
        [DataMember]
        public float TotIVARetTotal{get;set;}
        [DataMember]
        public float TotOpIVARetParcial{get;set;}
        [DataMember]
        public float TotIVARetParcial{get;set;}
        [DataMember]
        public float TotCredEC{get;set;}
        [DataMember]
        public float TotDepEnvase{get;set;}
       
        [DataMember]
        public List<totLiqui> TotLiquidaciones = new List<totLiqui>();
        
        [DataMember]
        public float TotMntTotal{get;set;}
        [DataMember]
        public float TotOpIVANoRetenido{get;set;}
        [DataMember]
        public float TotIVANoRetenido{get;set;}
        [DataMember]
        public float TotMntNoFact{get;set;}
        [DataMember]
        public float TotMntPeriodo{get;set;}
        [DataMember]
        public float TotPsjNac{get;set;}
        [DataMember]
        public float TotPsjInt{get;set;}
        
            
    }

    [DataContract]
    class totOtrosImp
    {
        [DataMember]
        public int   CodImp{get;set;}
        [DataMember]
        public float TotMntImp{get;set;}
        
    }

    [DataContract]
    class totLiqui
    {
        [DataMember]
        public float TotValComNeto{get;set;}
        [DataMember] 
        public float TotValComExe{get;set;}
        [DataMember] 
        public float TotValComIVA{get;set;}
        
    }

    [DataContract]
    class totalesPer
    {
        [DataMember] 
        public int TpoDoc{get;set;}
        [DataMember] 
        public int TotDoc{get;set;}
        [DataMember] 
        public float TotAnulado{get;set;}
        [DataMember] 
        public float TotOpExe{get;set;}
        [DataMember] 
        public float TotMntExe{get;set;}
        [DataMember] 
        public float TotMntNeto{get;set;}
        [DataMember]  
        public float TotMntIVA{get;set;}
        [DataMember] 
        public float TotIVAFueraPlazo{get;set;}
        [DataMember] 
        public float TotIVAPropio{get;set;}
        [DataMember] 
        public float TotIVATerceros{get;set;}
        [DataMember] 
        public float TotLey18211{get;set;}
        
        [DataMember]
        public List<totOtrosImp> TotOtrosImp = new List<totOtrosImp>();
     

        [DataMember] 
        public float TotOpIVARetTotal{get;set;}
        [DataMember] 
        public float TotIVARetTotal{get;set;}
        [DataMember] 
        public float TotOpIVARetParcial{get;set;}
        [DataMember] 
        public float TotIVARetParcial{get;set;}
        [DataMember] 
        public float TotCredEC{get;set;}
        [DataMember] 
        public float TotDepEnvase {get;set;}
    
        [DataMember]
        public List<totLiqui> TotLiquidaciones = new List<totLiqui>();
    
        [DataMember] 
        public float TotMntTotal{get;set;}
        [DataMember] 
        public float TotOpIVANoRetenido{get;set;}
        [DataMember] 
        public float TotIVANoRetenido{get;set;}
        [DataMember] 
        public float TotMntNoFact{get;set;}
        [DataMember] 
        public float TotMntPeriodo{get;set;}
        [DataMember] 
        public float TotPsjNac{get;set;}
        [DataMember] 
        public float TotPsjInt {get;set;}
        
	        
    }

    [DataContract]
    class detalle 
    {
        public int   TpoDoc{get;set;}
        [DataMember] 
        public int   Emisor{get;set;}
        [DataMember] 
        public int   NroDoc {get;set;}
        [DataMember] 
        public int   Anulado{get;set;}
        [DataMember] 
        public int   Operacion {get;set;}
        [DataMember] 
        public float TasaImp{get;set;}
        [DataMember] 
        public int   NumInt{get;set;}
        [DataMember] 
        public int   IndServicio{get;set;}
        [DataMember] 
        public int   IndSinCosto{get;set;}
        [DataMember] 
        public String FchDoc{get;set;}
        [DataMember] 
        public String CdgSIISucur{get;set;}
        [DataMember] 
        public String RUTDoc{get;set;}
        [DataMember] 
        public String RznSoc{get;set;}
        [DataMember] 
        public int    NumId{get;set;}
        [DataMember] 
        public String Nacionalidad{get;set;}
        [DataMember] 
        public int    TpoDocRef{get;set;}
        [DataMember] 
        public int    FolioDocRef{get;set;}
        [DataMember] 
        public float MntExe{get;set;}
        [DataMember] 
        public float MntNeto{get;set;}
        [DataMember] 
        public float MntIVA{get;set;}
        [DataMember] 
        public float IVAFueraPlazo {get;set;}
        [DataMember] 
        public float IVAPropio{get;set;}
        [DataMember] 
        public float IVATerceros{get;set;}
        [DataMember] 
        public float Ley18211{get;set;}
        
        [DataMember]
        public List<detOtrosImp> OtrosImp = new List<detOtrosImp>();
        
        public float IVARetTotal{get;set;}
        [DataMember] 
        public float IVARetParcial{get;set;}
        [DataMember] 
        public float CredEC{get;set;}
        [DataMember] 
        public float DepEnvase{get;set;}
         
        [DataMember]
        public List<detLiqui> Liquidaciones = new List<detLiqui>();
	             
    }

    [DataContract]
    class detOtrosImp
    {
        [DataMember] 
        public int CodImp {get;set;}
        [DataMember] 
        public float TasaImp{get;set;}
        [DataMember] 
        public float MntImp{get;set;}
        
    }

    [DataContract]
    class detLiqui
    {
        [DataMember] 
        public String RutEmisor{get;set;}
        [DataMember]  
        public float ValComNeto{get;set;}
        [DataMember]  
        public float ValComExe{get;set;}
        [DataMember]  
        public float ValComIVA{get;set;}
         
    }
}
