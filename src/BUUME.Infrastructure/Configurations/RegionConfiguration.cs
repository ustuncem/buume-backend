using BUUME.Domain.Regions;
using BUUME.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Country = BUUME.Domain.Countries.Country;
using Region = BUUME.Domain.Regions.Region;

namespace BUUME.Infrastructure.Configurations;

internal sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("regions");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        
        builder.HasOne<Country>().WithMany().HasForeignKey(region => region.CountryId);
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}