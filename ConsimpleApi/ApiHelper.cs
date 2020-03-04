using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace ConsimpleApi
{
    using Model;
    
    static class ApiHelper
    {
        static CookieContainer _authCookie = new CookieContainer();

        public static ResponseData GetData(string path)
        {
            var authRequest = GetHttpWebRequest(path, "GET");

            ResponseData responseData;
            using (var response = (HttpWebResponse)authRequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    responseData = JsonConvert.DeserializeObject<ResponseData>(responseText);
                }
            }

            return responseData;
        }

        /*
         * uri - строка доступа к ресурсу Api
         * method - GET, POST, etc
         * headers - набор параметров в "шапке"
         * contentType - тип передаваемых "данных"
         */
        public static HttpWebRequest GetHttpWebRequest(string uri, string method, Dictionary<string, string> headers = null, string contentType = "application/json")
        {
            var httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.CookieContainer = _authCookie;

            headers = (headers != null) ? headers : new Dictionary<string, string>();
            foreach (var header in headers)
            {
                httpWebRequest.Headers.Add(header.Key, header.Value);
            }

            return httpWebRequest;
        }
    }
}
