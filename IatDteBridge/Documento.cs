using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class Documento
    {
        public int TipoDte {get;set;}
        int Folio;
        DateTime FchEmis;
        int IndNoRebaja;
        int TipoDespacho;
        int IndTraslado;
        String TpoImpresion;
        int IndServicio;
        int MntBruto;
        int FmaPago;
        int FmaPagoExp;
        DateTime FchCancel;
        int MntCancel;
        int SaldoInsol;
        DateTime FchPago;
        int MntPago;
        String GlosaPago;
        DateTime PeriodoDesde;
        DateTime PeriodoHasta;
        String MedioPago;
        String TipoCtaPago;
        String NumCtaPago;
        String BcoPago;

        det_documento[] detalle;

        
    }

    class det_documento
    {
        int item;
        String codigo;

    }
}
