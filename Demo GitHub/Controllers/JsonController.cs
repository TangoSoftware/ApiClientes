using Demo_GitHub.Helpers;
using Demo_GitHub.Models;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class JsonController : Controller
    {        
        public ActionResult Index(long idCliente, long idComprobante)
        {
            MarkAsReaded(idCliente, idComprobante);
            var model = new JsonViewModel(idCliente, idComprobante);
            return View(model);
        }

        public void MarkAsReaded(long idCliente, long idComprobante)
        {
            FileHelper.DeleteLine(@"C:\inetpub\wwwroot\Demo GitHub\IO\Comprobantes.txt", $"{idCliente};{idComprobante}");
        }        
    }
}