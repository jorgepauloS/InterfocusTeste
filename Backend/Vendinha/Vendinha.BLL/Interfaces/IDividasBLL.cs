using Vendinha.Commons.DTOs;
using Vendinha.Commons.Enums;

namespace Vendinha.BLL.Interfaces
{
    public interface IDividasBLL : IBaseBLL<DividaDto, int>
    {
        public Task<IEnumerable<DividaDto>> GetFromCliente(int clienteId, CancellationToken cancellationToken);
        public Task<IEnumerable<DividaDto>> GetFromSituacao(EnumSituacaoDivida situacao, CancellationToken cancellationToken);
        public Task<int> PagarDivida(int id, CancellationToken cancellationToken);
    }
}
