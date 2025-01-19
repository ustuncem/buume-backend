using BUUME.Domain.BusinessCategories;
using BUUME.Domain.Businesses;
using BUUME.Domain.Cities;
using BUUME.Domain.Countries;
using BUUME.Domain.Districts;
using BUUME.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using File = BUUME.Domain.Files.File;
using Location = BUUME.Domain.Businesses.Location;

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
        
        builder.Property(b => b.IsEnabled)
            .HasConversion(
                isEnabled => isEnabled.Value,
                value => new IsEnabled(value)
            )
            .HasDefaultValue(new IsEnabled(false))
            .IsRequired();

        builder.Property(b => b.Location)
            .HasConversion(
                location => new Point(location.Longitude, location.Latitude),
                point => Location.Create(point.Y, point.X)
            )
            .HasColumnType("geometry(Point, 4326)");

        builder.Property(x => x.Address)
            .HasConversion(e => e.Value, value => new Address(value));
        
        builder.Property(x => x.IsKvkkApproved)
            .HasConversion(e => e.Value, value => new IsKvkkApproved(value))
            .HasDefaultValue(new IsKvkkApproved(false))
            .IsRequired();

        builder.OwnsOne(x => x.WorkingHours);

        builder.HasOne<File>().WithMany().HasForeignKey(b => b.LogoId).OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne<File>().WithMany().HasForeignKey(b => b.TaxDocumentId);
        builder.HasOne<User>().WithMany().HasForeignKey(b => b.ValidatorId).OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne<User>().WithOne().HasForeignKey<Business>(b => b.OwnerId);
        builder.HasOne<Country>().WithMany().HasForeignKey(b => b.CountryId);
        builder.HasOne<City>().WithMany().HasForeignKey(b => b.CityId);
        builder.HasOne<District>().WithMany().HasForeignKey(b => b.DistrictId);
        builder.HasMany<BusinessCategory>().WithMany();
        builder.HasMany<File>().WithMany();

        builder.HasIndex(b => b.Location).HasMethod("gist");
        
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}