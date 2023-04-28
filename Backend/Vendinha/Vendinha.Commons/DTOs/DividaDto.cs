using System.ComponentModel.DataAnnotations;
using Vendinha.Commons.Enums;

namespace Vendinha.Commons.DTOs
{
    public class DividaDto : BaseDto<int>
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public float Valor { get; set; }
        [Required]
        public EnumSituacaoDivida Situacao { get; set; }
        public DateTime? DataPagamento { get; set; }

        public ClienteDto Cliente { get; set; }
    }
}
