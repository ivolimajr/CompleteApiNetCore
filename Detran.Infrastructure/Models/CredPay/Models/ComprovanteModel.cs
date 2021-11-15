using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public  class ComprovanteModel
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("autenticacao")]
        public string Autenticacao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("bancoLiquidante")]
        public string BancoLiquidante { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("codBarras")]
        public string CodBarras { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("dataPagamento")]
        public DateTime DataPagamento { get; set; }

    }
}
