using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Demo_GitHub.Models
{
    public class JsonViewModel
    {
        public long IdCliente { get; set; }

        public long IdComprobante { get; set; }

        public string Json { get; set; }

        public string Token { get; set; }

        public JsonViewModel(long idCliente, long idComprobante)
        {
            IdCliente = idCliente;
            IdComprobante = idComprobante;
            Json = GetJson();
        }

        public string GetJson()
        {
            string completeUrl = GetApiUrl("http://localhost:40696/api/comprobantes/getjson/");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("kAwfZMgk5cMSU+7oPU3TCKtJLgjiE6DEXVYGO75eP6iqp5B5cODoMBehebXfs3Mq8fKW9Rg1MpyXOmafhiZ9ocPTa7jIewMsmxMLvfz47fLAhuJR890Nwip80YbfvG7Z");

                HttpWebRequest request = WebRequest.Create(completeUrl) as HttpWebRequest;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var encoding = Encoding.ASCII;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        protected string GetApiUrl(string url)
        {
            //gcozzi@axoft.com: de ser posible, hallar una forma menos rudimentaria de construir la URL
            var absoluteUri = url.EndsWith("/") ? url : new StringBuilder(url).Append("/").ToString();
            return new Uri(new Uri(absoluteUri), $"{IdCliente}/{IdComprobante}").ToString();
        }
    }
}