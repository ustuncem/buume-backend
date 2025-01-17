using BUUME.Domain.PairDevice;
using BUUME.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUUME.Infrastructure.Configurations;

internal sealed class PairDeviceConfiguration : IEntityTypeConfiguration<PairDevice>
{
    public void Configure(EntityTypeBuilder<PairDevice> builder)
    {
        builder.ToTable("pair_device");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.DeviceName).HasMaxLength(500)
            .HasConversion(deviceName => deviceName.Value, value => new DeviceName(value))
            .IsRequired();
        
        builder.Property(x => x.FcmToken).HasMaxLength(300)
            .HasConversion(fcmToken => fcmToken.Value, value => new FcmToken(value))
            .IsRequired();
        
        builder.Property(x => x.OperatingSystem).HasConversion<int>().IsRequired();
        
        builder.Property(x => x.IsActive)
            .HasConversion(isActive  => isActive.Value, value => new IsActive(value))
            .IsRequired();
        
        builder.HasOne<User>().WithOne().HasForeignKey<PairDevice>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}