using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace IatDteBridge
{
    class Connect
    {
        public static string server = "http://www.cranberrychic.com";
        public static string version = "/api/v2";
        public static string auth_token = "aksub82763";

        public Connect() {
        }
        
        public string ping ()
        {
            try
            {
                string getUrl = string.Format("{0}{1}/iphone/current_version?auth_token={2}",
                                    Uri.EscapeDataString(server),
                                    Uri.EscapeDataString(version),
                                    Uri.EscapeDataString(auth_token));
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
