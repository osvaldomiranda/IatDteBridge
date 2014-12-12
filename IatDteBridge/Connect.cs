using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections;
using System.Windows.Forms;





namespace IatDteBridge
{
    class Connect
    {
      
        //  public static string server = "http://104.130.1.179";  // Staging
      // public static string server = "http://192.168.1.34:3000";   // Localhost
        public static string server = "http://200.72.145.75"; // prosuccion
        public static string version = "/api/v1";
        public static string auth_token = "tokenprueba";

        //TO DO: falta definir el mecanismo de Token

        public Connect()
        {

        }

        //TO DO : falta definir el ID del iat, debe estar definido en una variable global (ver parameters en el método ping)
        public string ping()
        {
            try
            {
                string getUrl = string.Format("{0}{1}/iat_ping/1.json?auth_token={2}",
                                    server,
                                    version,
                                    auth_token);

                Console.WriteLine("Url = {0}.", getUrl);
                string response;
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(getUrl);
                }

                return response;

            }
            catch (Exception err)
            {
                return err.Message;
            }

        }

        //TO DO : falta definir el ID del iat, debe estar definido en una variable global (ver parameters en el método sendXml)
        public string sendXml(String xml, String filename)
        {
            try
            {
                string HtmlResult = String.Empty;
                string postUri = string.Format("{0}{1}/iat_doc.json",
                                    server,
                                    version);

                String parameters = string.Format("id={0}&docxml={1}&filename={2}&auth_token={3}", "1", xml, filename, auth_token);

                Console.WriteLine("Url = {0}.", postUri);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    HtmlResult = wc.UploadString(postUri, parameters);
                }

                Console.WriteLine("Resultado = {0}.", HtmlResult);
                return HtmlResult;

            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        public string sendInvoice(Documento doc, String xml, String filename)
        {

               try
                {
            string HtmlResult = String.Empty;
            string postUri = string.Format("{0}{1}/invoice.json",
                                server,
                                version);

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Documento));
            ds.WriteObject(stream, doc);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();

            String json = jsonString.Replace("null", "\"\"");
            json = json.Replace("\":", ":");
            json = json.Replace(",\"", ",");
            json = json.Replace("{\"", "{");
            json = json.Replace("detalle", "detalles_attributes");
            json = json.Replace("Referencia", "ref_detalles_attributes");
            json = json.Replace("comisiones", "comisions_attributes");
            json = json.Replace("dscRcgGlobal", "dsc_rcg_globals_attributes");
            json = json.Replace("imptoReten", "impuesto_retens_attributes");
            json = json.Replace("mntpagos", "monto_pagos_attributes");




            if (json.IndexOf("detalles_attributes:\"\"") != -1)
            {
                json = json.Replace("detalles_attributes:\"\"", "detalles_attributes:[]");
            }
            if (json.IndexOf("ref_detalles_attributes:\"\"") != -1)
            {
                json = json.Replace("ref_detalles_attributes:\"\"", "ref_detalles_attributes:[]");
            }
            if (json.IndexOf("comisions_attributes:\"\"") != -1)
            {
                json = json.Replace("comisions_attributes:\"\"", "comisions_attributes:[{}]");
            }
            if (json.IndexOf("dsc_rcg_globals_attributes:\"\"") != -1)
            {
                json = json.Replace("dsc_rcg_globals_attributes:\"\"", "dsc_rcg_globals_attributes:[]");
            }
            if (json.IndexOf("impuesto_retens_attributes:\"\"") != -1)
            {
                json = json.Replace("impuesto_retens_attributes:\"\"", "impuesto_retens_attributes:[]");
            }
            if (json.IndexOf("monto_pagos_attributes:\"\"") != -1)
            {
                json = json.Replace("monto_pagos_attributes:\"\"", "monto_pagos_attributes:[]");
            }



            String parameters = string.Format("doc={0}&auth_token={1}&xml={2}&filename={3}", "{documento:" + json + "}", auth_token, xml, filename);

            Console.WriteLine("Url = {0}.", postUri);

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                HtmlResult = wc.UploadString(postUri, parameters);
            }

            Console.WriteLine("Resultado = {0}.", HtmlResult);

            Log log = new Log();
            log.addLog("DTE enviado al Core TipoDTE :" + doc.TipoDTE + " Folio :" + doc.Folio, "OK");

            return HtmlResult;

            }
            catch (Exception err)
            {
                Log log = new Log();
                log.addLog("ERROR envio al Core TipoDTE :" + doc.TipoDTE + " Folio :" + doc.Folio, "ERROR");

                  Console.WriteLine(err);
                  return err.Message;
            }
            
        }

        public void sendDocPdf(String fileName)
        {
            string postUri = string.Format("{0}{1}/iat_doc.json",
                    server,
                    version);

  
        }
    }
}
