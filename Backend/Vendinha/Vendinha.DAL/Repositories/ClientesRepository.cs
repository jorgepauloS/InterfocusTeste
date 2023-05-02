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
            sb.Append("SELECT C.*, D.Valor FROM Clientes C LEFT JOIN Dividas D on (D.ClienteId = C.Id)");
            sb.Append(" WHERE");
            if (!string.IsNullOrWhiteSpace(filteredName))
            {
                sb.Append($" (C.Nome LIKE @filteredName) AND ");
            }
            sb.Append(" (D.Situacao = 0 OR D.Situacao IS NULL)");
            sb.Append(" ORDER BY D.Valor DESC, C.Nome ASC");
            sb.Append($" OFFSET {10 * (page - 1)} ROWS");
            sb.Append($" FETCH NEXT 10 ROWS ONLY");

            IQueryable<Cliente> clienteQuery = _context.Clientes.FromSqlRaw(sb.ToString(), param);
            return await clienteQuery.ToListAsync(cancellationToken);
        }
    }
}
