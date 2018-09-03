using Demo_GitHub.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Hosting;

namespace Demo_GitHub.Models
{
    public class HomeViewModel
    {
        private static readonly string ACCESS = @"~/App_Data/Acceso.txt";
        private static readonly string COMPROBANTES = @"~/App_Data/Comprobantes.txt";

        public HomeViewModel()
        {
            IdCliente = Convert.ToInt64(FileHelper.GetFieldFromFile(HostingEnvironment.MapPath(ACCESS), 0, 0));
            Token = FileHelper.GetFieldFromFile(HostingEnvironment.MapPath(ACCESS), 0, 1);

            JsonList = new List<ComprobanteJson>();
            AddComprobanteToList();
        }

        #region Attributes
        public IList<ComprobanteJson> JsonList { get; set; }

        public long IdCliente { get; set; }

        public string Token { get; set; } 
        #endregion

        private void AddComprobanteToList()
        {
            string[] lines = System.IO.File.ReadAllLines(HostingEnvironment.MapPath(COMPROBANTES));

            foreach (string line in lines)
            {
                string[] comprobante = line.Split(';');

                if (Convert.ToInt64(comprobante[0]) == this.IdCliente)
                    JsonList.Add(new ComprobanteJson() { IdCliente = this.IdCliente, IdComprobante = Convert.ToInt64(comprobante[1]) });                
            }
        }
    }
}