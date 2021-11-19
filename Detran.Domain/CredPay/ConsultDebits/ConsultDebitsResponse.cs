using Detran.Infrastructure.Models.CredPay.Models;
using Newtonsoft.Json;

namespace Detran.Domain.CredPay.ConsultDebits
{
    public class ConsultDebitsResponse
    {
        [JsonProperty("detran")]
        public DetranDebitos Detran { get; set; }

        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }
    }
}
