using System;
using System.IO;
using System.Net;
using System.Text;

namespace Demo_GitHub.Helpers
{
    public static class ApiCompJsonHelper
    {
        /// <summary>
        /// Obtiene el comprobante JSON de Tango Clientes (API)
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="idComprobante">Identificador del comprobante</param>
        /// <param name="token">Token de seguridad</param>
        /// <returns>Objeto JSON del comprobante</returns>
        public static string GetJson(long idCliente, long idComprobante, string token)
        {
            string url = GetApiUrl(idCliente, idComprobante);

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Headers.Add("token", token);

                var response = request.GetResponse();

                var encoding = Encoding.ASCII;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Construye la URL correcta para la API del servicio de Tango Clientes
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="idComprobante">Identificador del comprobante</param>
        /// <returns>URL de la API de Tango Clientes</returns>
        private static string GetApiUrl(long idCliente, long idComprobante)
        {
            var url = AppSettingsHelper.UrlApi;
            var absoluteUri = url.EndsWith("/") ? url : new StringBuilder(url).Append("/").ToString();
            return new Uri(new Uri(absoluteUri), $"{idCliente}/{idComprobante}").ToString();
        }
    }
}