using Demo_GitHub.Helpers;
using Demo_GitHub.Models;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class JsonController : Controller
    {
        private static readonly string COMPROBANTES = @"~/App_Data/Comprobantes.txt";

        public ActionResult Index(long idCliente, long idComprobante)
        {
            var model = new JsonViewModel(idCliente, idComprobante);
            return View(model);
        }

        public void MarkAsReaded(long idCliente, long idComprobante)
        {            
            FileHelper.DeleteLine(HostingEnvironment.MapPath(COMPROBANTES), $"{idCliente};{idComprobante}");
        }        
    }
}