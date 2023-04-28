using Vendinha.Commons.Entities;

namespace Vendinha.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T, K> where T : BaseEntity<K>
    {
        public Task<int> Create(T entity, CancellationToken cancellationToken);
        public Task<T> GetById(K id, CancellationToken cancellationToken);
        public Task<int> Update(T entity, CancellationToken cancellationToken);
        public Task<int> Delete(K id, CancellationToken cancellationToken);
    }
}
