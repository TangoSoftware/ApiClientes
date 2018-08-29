using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo_GitHub.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            JsonList = new List<ComprobanteJson>();
            AddComprobanteToList();
        }

        public IList<ComprobanteJson> JsonList { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Token de seguridad")]
        public string Token { get; set; }

        private void AddComprobanteToList()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\inetpub\wwwroot\Demo GitHub\IO\Prueba.txt");

            foreach (string line in lines)
            {
                string[] comprobante = line.Split(';');
                JsonList.Add(new ComprobanteJson() { IdCliente = Convert.ToInt64(comprobante[0]), IdComprobante = Convert.ToInt64(comprobante[1]) });                
            }
        }
    }
}