using Newtonsoft.Json;
using System.Collections.Generic;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class Debito
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("valorNum")]
        public double ValorNum { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("selecionavel")]
        public bool Selecionavel { get; set; }

        [JsonProperty("dependencias")]
        public List<int> Dependencias { get; set; }
    }
}
