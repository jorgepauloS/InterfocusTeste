using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vendinha.Commons.Entities;

namespace Vendinha.DAL.Context.Configurations
{
    public class DividaConfiguration : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.ToTable("Dividas");
            builder.HasKey(x => x.Id);
        }
    }
}
