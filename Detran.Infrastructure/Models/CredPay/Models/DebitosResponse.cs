using Newtonsoft.Json;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class DebitosResponse
    {
        [JsonProperty("detran")]
        public DetranDebitos Detran { get; set; }

        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }
    }
}
