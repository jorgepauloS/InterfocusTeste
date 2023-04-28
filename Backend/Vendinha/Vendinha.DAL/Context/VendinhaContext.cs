using Microsoft.EntityFrameworkCore;
using Vendinha.Commons.Entities;
using Vendinha.DAL.Context.Configurations;

namespace Vendinha.DAL.Context
{
    public class VendinhaContext : DbContext
    {
        public VendinhaContext(DbContextOptions<VendinhaContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Divida> Dividas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new DividaConfiguration());

            //Código para não gerar colunas como NVARCHAR
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                        .SelectMany(t => t.GetProperties())
                                                        .Where(// Se for do tipo string
                                                               p => p.ClrType == typeof(string)
                                                               // E nenhum tipo for previamente definido
                                                               && p.GetColumnType() == null
                                                               ))
            {
                property.SetIsUnicode(false);
            }

            //Código para gerar colunas padrão de data com default value
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                        .SelectMany(t => t.GetProperties())
                                                        .Where(// Se for do tipo DateTime
                                                               p => p.ClrType == typeof(DateTime)
                                                               // Com nomes CreatedAt e UpdatedAt
                                                               &&
                                                               (p.GetColumnName().Equals("CreatedAt") || p.GetColumnName().Equals("UpdatedAt"))
                                                               ))
            {
                property.SetDefaultValueSql("GETDATE()");
            }

        }
    }
}
