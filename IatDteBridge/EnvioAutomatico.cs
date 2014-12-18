using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Net.Http;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Threading;

namespace IatDteBridge
{
    /// <summary>
    /// Esta clase queda para uso futuro
    /// Osvaldo.
    /// </summary>
    class EnvioAutomatico
    {
        //  public static string server = "http://104.130.1.179";  // Staging
        public static string server = "http://192.168.1.33:3000";   // Localhost
        // public static string server = "http://200.72.145.75"; // prosuccion
        //public static string server = "http://192.168.1.33:3000";   // Localhost
        // public static string server = "http://200.72.145.75"; // prosuccion

        public static string version = "/api/v1";
        public static string auth_token = "tokenprueba";


        public string getTokenCore()
        {
            try
            {
                string getUrl = string.Format("{0}{1}/gettoken.json?auth_token={2}",
                                    server,
                                    version,
                                    auth_token);

                Console.WriteLine("Url = {0}.", getUrl);
                string response;
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(getUrl);
                }

                Console.WriteLine("TOKEN = {0}.", response);
                return response;

            }
            catch (Exception err)
            {
                return err.Message;
            }

        }



        public void sendToSii(String fileName)
        {

            String rutSender = "05682509";
            String dvSender = "6";
            String rutCompany = fileName.Substring(10, 8);
            String dvCompany = fileName.Substring(19, 1); ;

            string url = "https://palena.sii.cl/cgi_dte/UPL/DTEUpload";

            
            string boundary = "9022632e1130lc4";
 

            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials = System.Net.CredentialCache.DefaultCredentials;


            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");


            // ************** Body ********
            string formdataTemplate = "--" + boundary +"\r\n"+
            "Content-Disposition: form-data; name=\"{0}\"\r\n"+
            "Content-Type: text/plain; charset=US-ASCII\r\n"+
            "Content-Transfer-Encoding: 8Bit\r\n"+
            "\r\n"+
            "{1}\r\n";

            string formitem = string.Format(formdataTemplate, "rutSender", rutSender);
           
            formitem += string.Format(formdataTemplate, "dvSender", dvSender);
           
            formitem += string.Format(formdataTemplate, "ruCompany", rutCompany);
           
            formitem += string.Format(formdataTemplate, "dvCompany", dvCompany);

            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            memStream.Write(formitembytes, 0, formitembytes.Length);
            memStream.Write(boundarybytes, 0, boundarybytes.Length);


            // ****************** FILE HEAD***************
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: /xml\r\n\r\n";

            string header = string.Format(headerTemplate, "archivo", fileName);

            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            memStream.Write(headerbytes, 0, headerbytes.Length);


            // ******************* FILE BODY **************

           
            FileStream fileStream = new FileStream(@"C:/IatFiles/file/xml/envio/" + fileName, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[1024];

            int bytesRead = 0;

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }

            boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            fileStream.Close();



            // ******************** REQUEST *****************

            memStream.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(memStream);
            var myStr = sr.ReadToEnd();
            Console.WriteLine("Stream Output: " + myStr);
            
            httpWebRequest2.Accept = @"image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-powerpoint, application/ms-excel, application/msword, */*";
            httpWebRequest2.Referer = "http://www.lubba.cl";
            httpWebRequest2.Headers.Add("Accept-Language", "es-cl");
            httpWebRequest2.ContentType = "multipart/form-data: boundary="+boundary;
            httpWebRequest2.Headers.Add("Accept-Encoding", "gzip, deflate");
            httpWebRequest2.UserAgent = "Mozilla/4.0 (compatible; PROG 1.0; Windows NT 5.0; YComp 5.0.2.4)";
            httpWebRequest2.ContentLength = memStream.Length;
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Headers.Add("Cache-Control", "no-cache");

            String tokenOk = getTokenCore();
            httpWebRequest2.Headers.Add("Cookie" , "TOKEN="+tokenOk);


            
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


            MessageBox.Show(reader2.ReadToEnd());

            webResponse2.Close();
            httpWebRequest2 = null;
            webResponse2 = null;



        }


        public void getToken()
        {
            var _url = "https://palena.sii.cl/DTEWS/CrSeed.jws?WSDL";
            var _action = "get_seed";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelopeSeed();

            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult = string.Empty;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);

                
            }


            // ************** Teniendo Semilla, paso a obtener Token ******************
            if (soapResult != string.Empty)
            {

                // ************* lo primero es crear el xml para el token y firmarlo
                int princip = soapResult.IndexOf("SEMILLA") +11;
                String seed = soapResult.Substring( princip, soapResult.IndexOf("/SEMILLA") - (princip+4) );

                String tosign_xml = "<gettoken><item><Semilla>" + seed + "</Semilla></item></gettoken>";

                Console.WriteLine(tosign_xml);

                // ********** FIRMAR
                X509Certificate2 cert = FuncionesComunes.obtenerCertificado("LUIS BARAHONA MENDOZA");

                XmlDocument sign_xml = firmarSemilla(tosign_xml, cert);

                // ****************
                Console.WriteLine("ESPERO · SEGUNDOS A LA FIRMA {0}", sign_xml);
                Thread.Sleep(3000);

                var url = "https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws?WSDL";
                var action = "get_token";

                string soapResultToken = string.Empty;

                while (soapResultToken == string.Empty)
                {
                    XmlDocument soapEnvelopeXmlToken = CreateSoapEnvelopeToken(sign_xml);

                    webRequest = CreateWebRequest(url, action);
                    InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXmlToken, webRequest);
                

                    // begin async call to web request.
                    asyncResult = webRequest.BeginGetResponse(null, null);

                    // suspend this thread until call is complete. You might want to
                    // do something usefull here like update your UI.
                    asyncResult.AsyncWaitHandle.WaitOne();

                    // get the response from the completed web request.

                    try
                    {

                        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                        {
                            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                            {
                                soapResultToken = rd.ReadToEnd();
                            }
                            Console.Write(soapResultToken);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }
                    Thread.Sleep(1000);
                }

            }

        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelopeSeed()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:def=""http://DefaultNamespace""><soapenv:Header/><soapenv:Body><def:getSeed soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""/></soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        private static XmlDocument CreateSoapEnvelopeToken(XmlDocument xmlFirmado)
        {
            XmlDocument soapEnvelop = new XmlDocument();


            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:def=""http://DefaultNamespace""><soapenv:Header/><soapenv:Body><def:getToken soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><pszXml xsi:type=""xsd:string"">" + xmlFirmado.InnerXml + @"</pszXml></def:getToken></soapenv:Body></soapenv:Envelope>");

            Console.WriteLine(soapEnvelop.InnerXml);

            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }


        public XmlDocument firmarSemilla(string documento, X509Certificate2 certificado)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(documento);

            SignedXml signedXml = new SignedXml(doc);

            signedXml.SigningKey = certificado.PrivateKey;

            Signature XMLSignature = signedXml.Signature;

            Reference reference = new Reference("");

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            XMLSignature.SignedInfo.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            XMLSignature.KeyInfo = keyInfo;

            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();

            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }

            return doc;


        }
    }
}
