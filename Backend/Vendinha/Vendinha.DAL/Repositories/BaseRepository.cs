using Microsoft.EntityFrameworkCore;
using Vendinha.Commons.Entities;
using Vendinha.DAL.Context;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.DAL.Repositories
{
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : BaseEntity<K>
    {
        internal readonly VendinhaContext _context;

        public BaseRepository(VendinhaContext context)
        {
            _context = context;
        }

        public virtual async Task<int> Create(T entity, CancellationToken cancellationToken)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.CreatedAt = DateTime.Now;
            await _context.AddAsync(entity: entity, cancellationToken: cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<int> Delete(K id, CancellationToken cancellationToken)
        {
            await _context.Set<T>().Where(e => e.Id.Equals(id)).ExecuteDeleteAsync(cancellationToken: cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<T> GetById(K id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);
        }

        public virtual async Task<int> Update(T entity, CancellationToken cancellationToken)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}
