namespace Demo_GitHub.Models
{
    public class JsonViewModel
    {
        public JsonViewModel(long idCliente, long idComprobante, string json)
        {
            IdCliente = idCliente;
            IdComprobante = idComprobante;
            Json = json;
        }
        public long IdCliente { get; set; }

        public long IdComprobante { get; set; }

        public string Json { get; set; }
    }
}