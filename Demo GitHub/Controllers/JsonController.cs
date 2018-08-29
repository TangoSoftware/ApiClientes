using Demo_GitHub.Models;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class JsonController : Controller
    {        
        public ActionResult Index(long idCliente, long idComprobante)
        {
            var model = new JsonViewModel(idCliente, idComprobante);
            return View(model);
        }
    }
}