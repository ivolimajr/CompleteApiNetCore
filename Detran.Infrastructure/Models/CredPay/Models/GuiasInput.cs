using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Detran.Infrastructure.Models.CredPay.Models
{
    public class GuiasInput
    {
        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("uf")]
        public string Uf { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("renavam")]
        public string Renavam { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("pais")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [JsonProperty("debitosSelecionados")]
        public List<int> DebitosSelecionados { get; set; }

        [JsonProperty("nsu")]
        public string Nsu { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("valorTotal")]
        public double ValorTotal { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("formaPagamento")]
        public string FormaPagamento { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("numeroParcelas")]
        public short? NumeroParcelas { get; set; }

        [Required(ErrorMessage ="{0} é obrigatório!")]
        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [JsonProperty("idExterno")]
        public string IdExterno { get; set; }
    }
}
