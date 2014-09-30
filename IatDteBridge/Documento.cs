using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class Documento
    {
        public int TipoDte {get;set;}
        public int Folio;
        public String FchEmis;
        public int IndNoRebaja;
        public int TipoDespacho;
        public int IndTraslado;
        public String TpoImpresion;
        public int IndServicio;
        public int MntBruto;
        public int FmaPago;
        public int FmaPagoExp;
        public DateTime FchCancel;
        public int MntCancel;
        public int SaldoInsol;
        public DateTime FchPago;
        public int MntPago;
        public String GlosaPago;
        public DateTime PeriodoDesde;
        public DateTime PeriodoHasta;
        public String MedioPago;
        public String TipoCtaPago;
        public String NumCtaPago;
        public String BcoPago;
        public String TermPagoCdg;
        public String TermPagoGlosa;

        det_documento[] detalle;

        
    }

    class det_documento
    {
        int item;
        String codigo;

    }
}
