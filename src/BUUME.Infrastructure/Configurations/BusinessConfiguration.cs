using BUUME.Domain.BusinessCategories;
using BUUME.Domain.Businesses;
using BUUME.Domain.Cities;
using BUUME.Domain.Countries;
using BUUME.Domain.Districts;
using BUUME.Domain.TaxOffices;
using BUUME.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = BUUME.Domain.Files.File;

namespace BUUME.Infrastructure.Configurations;

internal sealed class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("businesses");
        
        builder.HasKey(x => x.Id);

        builder.OwnsOne(b => b.BaseInfo, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            navigationBuilder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            navigationBuilder.Property(x => x.PhoneNumber).HasMaxLength(100).IsRequired();
            navigationBuilder.Property(x => x.Description).HasMaxLength(2000);
        });

        builder.OwnsOne(b => b.AddressInfo, navigationBuilder =>
        {
            navigationBuilder.Property(a => a.Latitude).HasMaxLength(100).IsRequired();
            navigationBuilder.Property(a => a.Longitude).HasMaxLength(100).IsRequired();
        });
        
        builder.OwnsOne(t => t.TaxInfo, navigationBuilder =>
        {
            navigationBuilder.Property(ti => ti.TradeName).HasMaxLength(500).IsRequired();
            navigationBuilder.Property(ti => ti.Vkn).IsRequired();
        });
        
        builder.Property(x => x.IsKvkkApproved)
            .HasConversion(e => e.Value, value => new IsKvkkApproved(value))
            .HasDefaultValue(new IsKvkkApproved(false))
            .IsRequired();

        builder.OwnsOne(x => x.WorkingHours);

        builder.HasOne<File>().WithMany().HasForeignKey(b => b.LogoId);
        builder.HasOne<User>().WithOne().HasForeignKey<Business>(b => b.OwnerId);
        builder.HasOne<Country>().WithMany().HasForeignKey(b => b.CountryId);
        builder.HasOne<City>().WithMany().HasForeignKey(b => b.CityId);
        builder.HasOne<District>().WithMany().HasForeignKey(b => b.DistrictId);
        builder.HasOne<TaxOffice>().WithMany().HasForeignKey(b => b.TaxOfficeId);
        builder.HasMany<BusinessCategory>().WithMany();
        builder.HasMany<File>().WithMany();
        
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}