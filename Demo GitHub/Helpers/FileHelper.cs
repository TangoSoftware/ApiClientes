using System.IO;
using System.Text;

namespace Demo_GitHub.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Borra las líneas de un archivo, cuyo contenido coincide con el indicado
        /// </summary>
        /// <param name="filename">Ruta del archivo</param>
        /// <param name="lineToDelete">Contenido de las lineas a eliminar</param>
        public static void DeleteLine(string filename, string lineToDelete)
        {
            StringBuilder sb = new StringBuilder();
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                if (line != lineToDelete)
                    sb.Append(line);
            }

            RecreateFile(filename, sb);
        }

        /// <summary>
        /// Reemplaza las líneas de un archivo, cuyo contenido coincide con el indicado.
        /// </summary>
        /// <param name="filename">Ruta del archivo</param>
        /// <param name="lineToReplace">Contenido a reemplazar</param>
        /// <param name="newLineContent">Nuevo contenido de la línea</param>
        public static void ReplaceLine(string filename, string lineToReplace, string newLineContent)
        {
            StringBuilder sb = new StringBuilder();
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string lineToAppend = (line != lineToReplace) ? line : newLineContent;
                sb.AppendLine(lineToAppend);
            }

            RecreateFile(filename, sb);
        }

        /// <summary>
        /// Elimina un archivo de texto y lo vuele a crear
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
    }
}