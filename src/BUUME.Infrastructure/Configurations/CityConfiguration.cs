using BUUME.Domain.Cities;
using BUUME.Domain.Regions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Country = BUUME.Domain.Countries.Country;
using Name = BUUME.Domain.Cities.Name;

namespace BUUME.Infrastructure.Configurations;

internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        
        builder.Property(x => x.Code).HasMaxLength(10)
            .HasConversion(code => code.Value, value => new Code(value))
            .IsRequired();
        
        builder.HasOne<Country>().WithMany().HasForeignKey(city => city.CountryId).IsRequired();
        builder.HasOne<Region>().WithMany().HasForeignKey(city => city.RegionId).OnDelete(DeleteBehavior.SetNull);
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}