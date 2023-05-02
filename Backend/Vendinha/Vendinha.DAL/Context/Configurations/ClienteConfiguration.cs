using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendinha.Commons.Entities;

namespace Vendinha.DAL.Context.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(x => x.Id);
            builder.HasMany(e => e.Dividas).WithOne(e => e.Cliente).HasForeignKey(e => e.ClienteId).IsRequired();
        }
    }
}
