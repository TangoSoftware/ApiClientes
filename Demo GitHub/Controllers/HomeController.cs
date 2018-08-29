using Demo_GitHub.Models;
using System.IO;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Demostración de JSON de comprobantes";

            var model = new HomeViewModel();
            return View(model);
        }

        [HttpGet]
        public void AddComprobante(long id, long idComprobante)
        {
            string comprobante = $"{id};{idComprobante}";
            string path = @"C:\inetpub\wwwroot\Demo GitHub\IO\Prueba.txt";

            if (!System.IO.File.Exists(path))
            {
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine(comprobante);
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLine(comprobante);
                }
            }
        }
    }
}