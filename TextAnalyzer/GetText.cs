using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextAnalyzer
{
    // Here the GET request to a server happens.
    public class GetText
    {
        public static string url = "https://tmgwebtest.azurewebsites.net/api/textstrings/";

        public static string key = "TMG-Api-Key";

        public static string value = "0J/RgNC40LLQtdGC0LjQutC4IQ==";
        public T downloadContent<T>(string id)
        {
            id = id.Replace(",", "");
                
            Uri myUri = new Uri(url + id);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);

            request.AllowAutoRedirect = true;

            request.MaximumAutomaticRedirections = 1;
                
            request.Headers.Add(key, value);

            request.Method = "GET";

            request.Accept = "application/json";

            request.Timeout = 30000;

            WebResponse webResponse = request.GetResponse();

            Stream responseStream = webResponse.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

            string json = streamReader.ReadToEnd();

            var content = JsonConvert.DeserializeObject<T>(json);

            responseStream.Close();

            webResponse.Close();

            return content;
        }
    }

}
