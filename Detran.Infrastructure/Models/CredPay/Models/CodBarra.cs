using Newtonsoft.Json;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class CodBarra
    {
        [JsonProperty("codBarras")]
        public string CodBarras { get; set; }

        [JsonProperty("dataVencimento")]
        public string DataVencimento { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("valorNum")]
        public double ValorNum { get; set; }

        [JsonProperty("descricao")]
        public object Descricao { get; set; }
    }
}
