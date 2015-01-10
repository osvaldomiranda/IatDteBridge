using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IatDteBridge
{
    class ProcesoContingencia
    {

        public void procesoContingencia()
        {
            ReenvioSql reenv = new ReenvioSql();
            // saco el nombres de los archivos  json, enviomasivo.xml pdfT y pdfC

            List<String> listaReenvio = new List<string>();

            listaReenvio = reenv.sgteReenvio();
            // cargo clase Documento
            Documento doc = new Documento();
            TxtReader json = new TxtReader();

            String envunit = String.Empty;
            String pdft = String.Empty;
            String pdfc = String.Empty;
            String jsonName = String.Empty;

            int i = 0;
            foreach (var reenvio in listaReenvio)
            {
                i++;
                switch (i)
                {
                    case 1:
                        {
                            doc = json.lectura(@"c:\IatFiles\fileprocess\" + reenvio, false, @"c:\IatFiles\fileprocess\");
                            jsonName = reenvio;
                        }
                        break;
                    case 2: envunit = reenvio; 
                        break;
                    case 3: pdft = reenvio;
                        break;
                    case 4: pdfc = reenvio;
                        break;
                }
            }
            // llamo clase connect para reenviar
            if (listaReenvio.Count() > 0)
            {
                Connect conn = new Connect();
                conn.sendInvoice(doc, pdft, pdfc, envunit, "S");

                // Cambio estado del registro de reenvio 
                reenv.cambioEstadoReenvio("PROCESADO", jsonName);
            }

        }
    }
}
