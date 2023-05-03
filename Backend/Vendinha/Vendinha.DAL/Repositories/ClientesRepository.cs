using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Vendinha.Commons.Entities;
using Vendinha.DAL.Context;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.DAL.Repositories
{
    public class ClientesRepository : BaseRepository<Cliente, int>, IClientesRepository
    {
        public ClientesRepository(VendinhaContext context) : base(context) { }

        public async Task<int> CountAll(string filteredName, CancellationToken cancellationToken)
        {
            IQueryable<Cliente> clienteQuery = _context.Clientes;

            if (!string.IsNullOrWhiteSpace(filteredName))
            {
                clienteQuery = clienteQuery.Where(e => e.Nome.Contains(filteredName));
            }

            return await clienteQuery.CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Cliente>> GetAll(int page, string filteredName, CancellationToken cancellationToken)
        {
            var param = new SqlParameter("filteredName", $"%{filteredName}%");

            StringBuilder sb = new();
            sb.Append("SELECT C.*, ISNULL((SELECT TOP 1 B.Valor FROM Dividas B WHERE C.Id = B.ClienteId), 0) AS Valor FROM Clientes C");
            if (!string.IsNullOrWhiteSpace(filteredName))
            {
                sb.Append($" WHERE (C.Nome LIKE @filteredName)");
            }
            sb.Append(" ORDER BY Valor DESC, C.Nome ASC");
            sb.Append($" OFFSET {10 * (page - 1)} ROWS");
            sb.Append($" FETCH NEXT 10 ROWS ONLY");

            IQueryable<Cliente> clienteQuery = _context.Clientes.FromSqlRaw(sb.ToString(), param);
            return await clienteQuery.ToListAsync(cancellationToken);
        }
    }
}
