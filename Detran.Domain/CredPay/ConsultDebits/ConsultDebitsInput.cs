using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Detran.Domain.CredPay.ConsultDebits
{
    public class ConsultDebitsInput : IRequest<ConsultDebitsResponse>
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
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
