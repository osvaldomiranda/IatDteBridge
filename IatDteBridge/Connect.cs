using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace IatDteBridge
{
    class Connect
    {
       // public static string server = "http://www.cranberrychic.com";
        public static string server = "http://104.130.1.179";
        public static string version = "/api/v1";
        public static string auth_token = "tokenprueba";

        public Connect() {
        }
        
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
        
        

    }
}
