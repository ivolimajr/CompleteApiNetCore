using Newtonsoft.Json;
using System.Collections.Generic;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class DetranGuias
    {
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("renavam")]
        public string Renavam { get; set; }

        [JsonProperty("listaCodBarras")]
        public List<CodBarra> ListaCodBarras { get; set; }

        [JsonProperty("debitos")]
        public List<Debito> Debitos { get; set; }
    }
}
