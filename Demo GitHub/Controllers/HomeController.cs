using Demo_GitHub.Models;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Demo_GitHub.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string ACCESS = @"~/App_Data/Acceso.txt";
        private static readonly string COMPROBANTES = @"~/App_Data/Comprobantes.txt";

        public ActionResult Index()
        {
            ViewBag.Message = "Demostración de JSON de comprobantes";

            var model = new HomeViewModel();
            return View(model);
        }

        public void AddComprobante(long id, long idComprobante)
        {
            string comprobante = $"{id};{idComprobante}";
            string path = HostingEnvironment.MapPath(COMPROBANTES);

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

        public void SetAccessData(long idCliente, string token)
        {
            using (StreamWriter file =  new StreamWriter(HostingEnvironment.MapPath(ACCESS)))
            {
                file.WriteLine($"{idCliente};{token}");                
            }
        }
    }
}