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
       // public static string server = "http://www.cranberrychic.com";
      //  public static string server = "http://104.130.1.179";
        public static string server = "http://192.168.1.34:3000";
        public static string version = "/api/v1";
        public static string auth_token = "tokenprueba";

        //TO DO: falta definir el mecanismo de Token

        public Connect() {
        }

        //TO DO : falta definir el ID del iat, debe estar definido en una variable global (ver parameters en el método ping)
        public string ping ()
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

               String parameters = string.Format("id={0}&docxml={1}&filename={2}&auth_token={3}","1",xml,filename,auth_token);

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

        public string sendInvoice(Documento doc)
        {

        //    try
        //    {
                string HtmlResult = String.Empty;
                string postUri = string.Format("{0}{1}/invoice.json",
                                    server,
                                    version);



                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Documento));
             //   DataContractJsonSerializerSettings js = new DataContractJsonSerializerSettings();
                ds.WriteObject(stream, doc);
                string jsonString = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();



              //  String json = js.ToString();
                String json = jsonString.Replace("null", "\"\"");
                json = json.Replace("\":", ":");
                json = json.Replace(",\"", ",");
                json = json.Replace("{\"", "{");
                json = json.Replace("detalle", "detalles_attributes");
                json = json.Replace("Referencia", "referencium_attributes");
                json = json.Replace("detalle", "detalles_attributes");
                json = json.Replace("detalle", "detalles_attributes");
                json = json.Replace("detalle", "detalles_attributes");
                json = json.Replace("detalle", "detalles_attributes");

                String parameters = string.Format("documento={0}&auth_token={1}", json, auth_token);

                Console.WriteLine("Url = {0}.", postUri);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    HtmlResult = wc.UploadString(postUri, parameters);
                }

                Console.WriteLine("Resultado = {0}.", HtmlResult);
                return HtmlResult;

          /*  }
            catch (Exception err)
            {
                return err.Message;
            }
           */ 
        }
        

    }
}
