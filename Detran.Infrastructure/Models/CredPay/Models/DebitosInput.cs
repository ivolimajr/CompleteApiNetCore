using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public  class DebitosInput
    {
        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("pais")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("uf")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("renavam")]
        public string Renavam { get; set; }
    }
}
