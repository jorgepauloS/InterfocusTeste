using Vendinha.Commons.Entities;

namespace Vendinha.DAL.Repositories.Interfaces
{
    public interface IClientesRepository : IBaseRepository<Cliente, int>
    {
        public Task<IEnumerable<Cliente>> GetAll(int page, string filteredName, CancellationToken cancellationToken);
        public Task<int> CountAll(string filteredName, CancellationToken cancellationToken);
    }
}
