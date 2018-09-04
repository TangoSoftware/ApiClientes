using Demo_GitHub.Helpers;
using System;
using System.Collections.Generic;

namespace Demo_GitHub.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            IdCliente = AppSettingsHelper.IdCliente;
            Token = AppSettingsHelper.Token;
            JsonList = ManagerComprobantesHelper.GetComprobantes(Convert.ToInt64(IdCliente));
        }

        public string IdCliente { get; set; }

        public string Token { get; set; }

        public IList<ComprobanteJson> JsonList { get; set; }
    }
}