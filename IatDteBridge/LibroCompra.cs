using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IatDteBridge
{
    [DataContract]
    class LibroCompra
    {
        //Cabecera
	    [DataMember]
        public String RutEmisorLibro {get;set;}
        [DataMember]
	    public String RutEnvia{get;set;}
        [DataMember]
	    public String PeriodoTributario {get;set;}
        [DataMember]
	    public String FchResol {get;set;}
        [DataMember]
	    public int    NroResol {get;set;}
        [DataMember]
	    public String TipoOperacion {get;set;}
        [DataMember]
	    public String TipoLibro {get;set;}
        [DataMember]
	    public String TipoEnvio {get;set;}


        // Lista de Totales
        [DataMember]
        public List<Totales_periodo> TotalesPeriodo = new List<Totales_periodo>();


        // Lista de Detalles
        [DataMember]
        public List<Detalle_libro> Detalle = new List<Detalle_libro>();
    }

    [DataContract]
    class Totales_periodo {
        [DataMember]
        public int TpoDoc {get;set;}
        [DataMember]
        public int TotDoc { get; set; }
        [DataMember]
        public float TotMntExe { get; set; }
        [DataMember]
        public float TotMntNeto { get; set; }
        [DataMember]
		public float TotMntIVA {get;set;}
        [DataMember]
		public float TotOpIVAUsoComun {get;set;}
        [DataMember]
		public float TotIVAUsoComun {get;set;}
        [DataMember]
		public float FctProp {get;set;}
        [DataMember]
		public float TotCredIVAUsoComun {get;set;}
        [DataMember]
		public float TotIVAFueraPlazo {get;set;}
        [DataMember]
		public float TotMntTotal {get;set;}

        // Lista TotIVANoRec
        [DataMember]
        public List<TotIVANo_Rec> TotIVANoRec = new List<TotIVANo_Rec>();

        // Lista TotOtrosImp
        [DataMember]
        public List<TotOtros_Imp> TotOtrosImp = new List<TotOtros_Imp>();
    }

    [DataContract]
    class TotIVANo_Rec {
        [DataMember]
        public String CodIVANoRec {get;set;}
        [DataMember]
	    public float TotOpIVANoRec {get;set;}
        [DataMember]
        public float TotMntIVANoRec { get; set; }
	}

    class TotOtros_Imp
	{
        [DataMember]
        public int CodImp {get;set;}
        [DataMember]
		public float TotMntImp {get;set;}
    }
	


    [DataContract]
    class Detalle_libro
    {
        [DataMember]
        public int TpoDoc {get;set;}
        [DataMember]
        public int NroDoc {get;set;}
        [DataMember]
		public int TpoImp {get;set;}
        [DataMember]
		public float TasaImp {get;set;}
        [DataMember]
		public String FchDoc {get;set;}
        [DataMember]
		public String RUTDoc {get;set;}
        [DataMember]
		public String RznSoc {get;set;}
        [DataMember]
		public float MntExe {get;set;}
        [DataMember]
		public float MntNeto {get;set;}
        [DataMember]
	    public float IVAUsoComun {get;set;}
        [DataMember]
		public float MntIVA {get;set;}
        [DataMember]
		public float MntTotal {get;set;}

        // Lista OtrosImp
        [DataMember]
        public List<Otros_Imp> OtrosImp = new List<Otros_Imp>();
        
        // lista IVANoRec
        [DataMember]
        public List<IVANo_Rec> IVANoRec = new List<IVANo_Rec>();
    }

    [DataContract]
    class Otros_Imp
    {   
        [DataMember]
        public int CodImp {get;set;}
        [DataMember]
		public float TasaImp {get;set;}
        [DataMember]
		public float MntImp {get;set;}
        
    }

    [DataContract]
    class  IVANo_Rec	
    {
        [DataMember]
        public int CodIVANoRec {get;set;}
        [DataMember]
        public float MntIVANoRec { get; set; }
       
    }
    
}
