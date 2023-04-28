using System.Linq.Expressions;
using Vendinha.Commons.Entities;

namespace Vendinha.DAL.Repositories.Interfaces
{
    public interface IDividasRepository : IBaseRepository<Divida, int>
    {
        public Task<IEnumerable<Divida>> GetAllWhere(Expression<Func<Divida, bool>> function, CancellationToken cancellationToken);
    }
}
