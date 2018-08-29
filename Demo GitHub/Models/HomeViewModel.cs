using System;
using System.Collections.Generic;
using System.Configuration;

namespace Demo_GitHub.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            IdCliente = Convert.ToInt64(GetAccessData(0));
            Token = GetAccessData(1);

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
            string[] lines = System.IO.File.ReadAllLines(@"C:\inetpub\wwwroot\Demo GitHub\IO\Comprobantes.txt");

            foreach (string line in lines)
            {
                string[] comprobante = line.Split(';');

                if (Convert.ToInt64(comprobante[0]) == this.IdCliente)
                    JsonList.Add(new ComprobanteJson() { IdCliente = this.IdCliente, IdComprobante = Convert.ToInt64(comprobante[1]) });                
            }
        }

        private string GetAccessData(int posicion)
        {
            System.IO.StreamReader file =  new System.IO.StreamReader(@"C:\inetpub\wwwroot\Demo GitHub\IO\Acceso.txt");
            string line = file.ReadLine();
            file.Close();

            string[] accessData = line.Split(';');
            return accessData[posicion];
        }
    }
}