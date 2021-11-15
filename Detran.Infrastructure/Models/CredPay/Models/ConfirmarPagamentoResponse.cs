using Newtonsoft.Json;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class ConfirmarPagamentoResponse
    {
        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
