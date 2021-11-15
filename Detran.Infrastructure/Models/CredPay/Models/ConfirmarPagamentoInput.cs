using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class ConfirmarPagamentoInput
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public List<ComprovanteModel> Comprovantes { get; set; }
    }
}
