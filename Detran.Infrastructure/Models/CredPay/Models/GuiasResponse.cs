using Newtonsoft.Json;

namespace Detran.Infrastructure.Models.CredPay.Models
{

    public class GuiasResponse
    {
        [JsonProperty("detran")]
        public DetranGuias Detran { get; set; }

        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [JsonProperty("idExterno")]
        public string IdExterno { get; set; }
    }
}
