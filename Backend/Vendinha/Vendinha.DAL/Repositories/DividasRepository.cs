using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vendinha.Commons.Entities;
using Vendinha.DAL.Context;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.DAL.Repositories
{
    public class DividasRepository : BaseRepository<Divida, int>, IDividasRepository
    {
        public DividasRepository(VendinhaContext context) : base(context) { }

        public override async Task<Divida> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Dividas.AsNoTracking().Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Divida>> GetAllWhere(Expression<Func<Divida, bool>> function, CancellationToken cancellationToken)
        {
            return await _context.Dividas.Where(function).Include(e => e.Cliente).ToListAsync(cancellationToken);
        }
    }
}
