using Demo_GitHub.Models;
using System.IO;
using System.Text;
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
            DeleteLineWithParams(idCliente, idComprobante);
        }

        //gcozzi@axoft.com: refactorizar, posiblemente extraer clase
        private static void DeleteLineWithParams(long idCliente, long idComprobante)
        {
            string fileName = @"C:\inetpub\wwwroot\Demo GitHub\IO\Comprobantes.txt";
            string lineToDelete = $"{idCliente};{idComprobante}";

            StringBuilder sb = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (line != lineToDelete)
                {
                    sb.Append(line);
                }
            }

            if (System.IO.File.Exists(fileName))
                System.IO.File.Delete(fileName);

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
        }
    }
}