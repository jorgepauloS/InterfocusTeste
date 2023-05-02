using Vendinha.Commons.DTOs;

namespace Vendinha.BLL.Interfaces
{
    public interface IClientesBLL : IBaseBLL<ClienteDto, int>
    {
        public Task<IEnumerable<ClienteDto>> GetAll(int page, string filteredName, CancellationToken cancellationToken);
        public Task<int> CountAll(string filteredName, CancellationToken cancellationToken);
    }
}
