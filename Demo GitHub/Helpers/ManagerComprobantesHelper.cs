using Demo_GitHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Hosting;

namespace Demo_GitHub.Helpers
{
    /// <summary>
    /// Clase encargada de administrar el acceso al storage (archivo txt en disco)
    /// donde se almacenarán los identificadores de comprobantes notificados por Tango Clientes.
    /// </summary>
    public static class ManagerComprobantesHelper
    {
        /// <summary>
        /// Ubicación del storage de comprobantes notificados
        /// </summary>
        private static readonly string Storage = @"~/App_Data/Comprobantes.txt";

        /// <summary>
        /// Agrega un registro al storage
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="idComprobante">Identificador del comprobante</param>
        public static void Add(long idCliente, long idComprobante)
        {
            var path = HostingEnvironment.MapPath(Storage);
            var comprobante = GetFormattedComprobanteLine(idCliente, idComprobante);

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                    sw.WriteLine(comprobante);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                    sw.WriteLine(comprobante);
            }
        }

        /// <summary>
        /// Elimina un registro del storage
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="idComprobante">Identificador del comprobante</param>
        public static void Delete(long idCliente, long idComprobante)
        {
            var path = HostingEnvironment.MapPath(Storage);
            var comprobante = GetFormattedComprobanteLine(idCliente, idComprobante);

            StringBuilder sb = new StringBuilder();
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (line != comprobante)
                    sb.AppendLine(line);
            }

            RecreateFile(path, sb);
        }

        /// <summary>
        /// Obtiene todos los registros (comprobantes) del storage según el cliente
        /// </summary>
        /// <returns>Lista de JSONs de comprobantes del cliente</returns>
        public static List<ComprobanteJson> GetComprobantes(long idCliente)
        {
            var list = new List<ComprobanteJson>();

            string[] lines = File.ReadAllLines(HostingEnvironment.MapPath(Storage));

            long idCl, idComp;
            foreach (string line in lines)
            {
                string[] comprobante = line.Split(';');
                idCl = Convert.ToInt64(comprobante[0]);
                idComp = Convert.ToInt64(comprobante[1]);

                if (idCl == idCliente)
                    list.Add(new ComprobanteJson() { IdCliente = idCl, IdComprobante = idComp });
            }

            return list;
        }

        /// <summary>
        /// Elimina un archivo de texto y lo vuele a crear con el contenido indicado
        /// </summary>
        /// <param name="filename">Ruta del archivo de texto a recrear</param>
        /// <param name="sb">Contenido del archivo de texto</param>
        private static void RecreateFile(string filename, StringBuilder sb)
        {
            if (File.Exists(filename))
                File.Delete(filename);

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Formato de datos por línea (idCliente;idComprobante) para el storage
        /// </summary>
        private static string GetFormattedComprobanteLine(long idCliente, long idComprobante)
        {
            return $"{idCliente};{idComprobante}";
        }
    }
}