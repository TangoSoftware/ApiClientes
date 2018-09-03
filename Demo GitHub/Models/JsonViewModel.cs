using Demo_GitHub.Helpers;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Demo_GitHub.Models
{
    public class JsonViewModel
    {
        private static readonly string COMPROBANTES = @"~/App_Data/Comprobantes.txt";

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
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={FileHelper.GetFieldFromFile(@"C:\inetpub\wwwroot\Demo GitHub\App_Data\Acceso.txt", 0, 1)}");

                try
                {
                    HttpWebRequest request = WebRequest.Create(completeUrl) as HttpWebRequest;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    FileHelper.DeleteLine(COMPROBANTES, $"{IdCliente};{IdComprobante}");

                    var encoding = Encoding.ASCII;
                    using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    throw new WebException(e.Message);
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