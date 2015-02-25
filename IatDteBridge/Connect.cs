using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Xml;



namespace IatDteBridge
{
    class Connect
    {

        //  public static string server = "http://104.130.1.179";  // Staging
        public static string server = "http://192.168.43.164:3000";   // Localhost
       //public static string server = "http://200.72.145.75"; // produccion



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

        public string sendInvoice(Documento doc, String pdfTfileName, String pdfCfileName, String filename, String conEnv)
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


            return sendToServer(doc.Folio, filename,json,pdfTfileName,pdfCfileName, conEnv);

            


            }
            catch (Exception err)
            {
                Log log = new Log();
                ReenvioSql renv = new ReenvioSql();
                log.addLog("ERROR envio al Core TipoDTE :" + doc.TipoDTE + " Folio :" + doc.Folio, "ERROR");
                renv.addReenvio(doc.fileName,filename,pdfTfileName,pdfCfileName);

                  Console.WriteLine(err);
                  return err.Message;
            }
            
        }

        // TO DO: agregar envio del archivo pdf cedible, tributable y xml
        public String sendToServer(int folio, String fileName, String json, String pdfTfileName, String pdfCfileName, String conEnvio)
        {
            string url = string.Format("{0}{1}/invoice.json",
                    server,
                    version);

            Console.WriteLine("Url = {0}.  {1}", url, fileName);

          //  long length = 0;
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");

            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +boundary + "\r\n");


            // ************** HEADER ********
            string formdataTemplate = "\r\n--" + boundary +
            "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            string formitem = string.Format(formdataTemplate, "doc", "{documento:" + json + "}");

            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem); 
            memStream.Write(formitembytes, 0, formitembytes.Length);
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            // ************************************************

            
            formitem = string.Format(formdataTemplate, "conEnvio", conEnvio);

            formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem); 
            memStream.Write(formitembytes, 0, formitembytes.Length);
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            
            // ****************** XML HEAD***************
          string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

            string header = string.Format(headerTemplate, "xmlFile", fileName);

            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            memStream.Write(headerbytes, 0, headerbytes.Length);
            // ******************* XML BODY **************
            Console.WriteLine("FILE BODY {0}", @"C:/IatFiles/file/xml/enviounitario/" + fileName);


            FileStream fileStream = new FileStream(@"C:/IatFiles/file/xml/envioUnitario/"+fileName, FileMode.Open, FileAccess.Read);
           // FileStream fileStream = new FileStream(@"C:/IatFiles/envioUnitario/" + fileName, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                 memStream.Write(buffer, 0, bytesRead);
            }

            boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            memStream.Write(boundarybytes, 0, boundarybytes.Length);


            if (pdfCfileName != "")
            {
                // ****************** PDFC HEAD***************
                header = string.Format(headerTemplate, "pdfCed", pdfCfileName);

                headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);
                // ******************* pdfC BODY **************
                Console.WriteLine("FILE BODY {0}", @"C:/IatFiles/file/pdf/" + pdfCfileName);

                FileStream fileStreampdfC = new FileStream(@"C:/IatFiles/file/pdf/" + pdfCfileName, FileMode.Open, FileAccess.Read);


                byte[] bufferpdfC = new byte[1024];
                int bytesReadpdfC = 0;
                while ((bytesReadpdfC = fileStreampdfC.Read(bufferpdfC, 0, bufferpdfC.Length)) != 0)
                {
                    memStream.Write(bufferpdfC, 0, bytesReadpdfC);
                }

                boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                fileStreampdfC.Close();
            }
            

            // ****************** PdfT HEAD***************
            header = string.Format(headerTemplate, "pdfTrib", pdfTfileName);

            headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            memStream.Write(headerbytes, 0, headerbytes.Length);


            // ******************* pdfT BODY **************
            Console.WriteLine("FILE BODY {0}", @"C:/IatFiles/file/pdf/" + pdfTfileName);


            FileStream fileStreampdfT = new FileStream(@"C:/IatFiles/file/pdf/" + pdfTfileName, FileMode.Open, FileAccess.Read);


          //  FileStream fileStreampdfT = new FileStream(@"C:/IatFiles/pdf/" + pdfTfileName, FileMode.Open, FileAccess.Read);

            byte[] bufferpdfT = new byte[1024];
            int bytesReadpdfT = 0;
            while ((bytesReadpdfT = fileStreampdfT.Read(bufferpdfT, 0, bufferpdfT.Length)) != 0)
            {
                memStream.Write(bufferpdfT, 0, bytesReadpdfT);
            }

            fileStreampdfT.Close();
            // ***************** Cierro el envío

            boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            fileStream.Close();
            
            
            
            // ******************** REQUEST *****************

            httpWebRequest2.ContentLength = memStream.Length;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();


            WebResponse webResponse2 = httpWebRequest2.GetResponse();

            Stream stream2 = webResponse2.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);


            webResponse2.Close();
            httpWebRequest2 = null;
            webResponse2 = null;

            return "Ok";
  
        }


    }


}
