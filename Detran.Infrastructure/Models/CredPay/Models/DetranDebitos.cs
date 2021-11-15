using Newtonsoft.Json;
using System.Collections.Generic;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class DetranDebitos
    {
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("renavam")]
        public string Renavam { get; set; }

        [JsonProperty("valorTotalNum")]
        public double ValorTotalNum { get; set; }

        [JsonProperty("valorTotal")]
        public string ValorTotal { get; set; }

        [JsonProperty("selecionavel")]
        public bool Selecionavel { get; set; }

        [JsonProperty("debitos")]
        public List<Debito> Debitos { get; set; }
    }
}
