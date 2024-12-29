using BUUME.Domain.Cities;
using BUUME.Domain.Districts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Code = BUUME.Domain.Districts.Code;
using Name = BUUME.Domain.Districts.Name;

namespace BUUME.Infrastructure.Configurations;

internal sealed class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable("districts");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        
        builder.Property(x => x.Code).HasMaxLength(50)
            .HasConversion(code => code.Value, value => new Code(value))
            .IsRequired();
        
        builder.HasOne<City>().WithMany().HasForeignKey(district => district.CityId).IsRequired();
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}