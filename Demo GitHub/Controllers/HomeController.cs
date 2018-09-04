using Demo_GitHub.Helpers;
using Demo_GitHub.Models;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            return View(model);
        }

        public void Notificar(long id, long idComprobante)
        {
            ManagerComprobantesHelper.Add(id, idComprobante);
        }

        public ActionResult ComprobanteJson(long idCliente, long idComprobante)
        {
            var compJson = ApiCompJsonHelper.GetJson(idCliente, idComprobante, AppSettingsHelper.Token);
            var model = new JsonViewModel(idCliente, idComprobante, compJson);
            return View(model);
        }

        public void MarkAsReaded(long idCliente, long idComprobante)
        {
            ManagerComprobantesHelper.Delete(idCliente, idComprobante);
        }
    }
}