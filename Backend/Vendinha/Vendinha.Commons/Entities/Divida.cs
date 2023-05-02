using Vendinha.Commons.Enums;

namespace Vendinha.Commons.Entities
{
    public class Divida : BaseEntity<int>
    {
        public int ClienteId { get; set; }
        public float Valor { get; set; }
        public EnumSituacaoDivida Situacao { get; set; }
        public DateTime? DataPagamento { get; set; }

        public Cliente Cliente { get; set; }
    }
}
