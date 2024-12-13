using BUUME.Domain.TaxOffices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUUME.Infrastructure.Configurations;

internal sealed class TaxOfficeConfiguration : IEntityTypeConfiguration<TaxOffice>
{
    public void Configure(EntityTypeBuilder<TaxOffice> builder)
    {
        builder.ToTable("TaxOffices");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}