using BUUME.Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUUME.Infrastructure.Configurations;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        builder.Property(x => x.Code).HasMaxLength(10)
            .HasConversion(code => code.Value, value => new Code(value))
            .IsRequired();
        builder.Property(x => x.HasRegion).HasMaxLength(500)
            .HasDefaultValue(new HasRegion(true))
            .HasConversion(hasRegion => hasRegion.Value, value => new HasRegion(value));
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}