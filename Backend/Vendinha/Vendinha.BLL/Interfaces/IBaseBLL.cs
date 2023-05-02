using Vendinha.Commons.DTOs;

namespace Vendinha.BLL.Interfaces
{
    public interface IBaseBLL<T, K> where T : BaseDto<K>
    {
        public Task<int> Create(T dto, CancellationToken cancellationToken);
        public Task<T> GetById(K id, CancellationToken cancellationToken);
        public Task<int> Update(T dto, CancellationToken cancellationToken);
        public Task<int> Delete(K id, CancellationToken cancellationToken);
    }
}
