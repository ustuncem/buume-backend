using BUUME.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = BUUME.Domain.Files.File;

namespace BUUME.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(500)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));
        
        builder.Property(x => x.LastName).HasMaxLength(500)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));

        builder.Property(x => x.Email).HasMaxLength(500)
            .HasConversion(email => email != null ? email.Value : "", value => new Email(value));
        
        builder.Property(x => x.SwitchedToBusiness)
            .HasConversion(switchedToBusiness => switchedToBusiness.Value , value => new SwitchedToBusiness(value));
        
        builder.Property(x => x.PhoneNumber).HasMaxLength(500)
            .HasConversion(phoneNumber => phoneNumber.Value, value => new PhoneNumber(value))
            .IsRequired();

        builder.Property(x => x.IsPhoneNumberVerified).
            HasConversion(isPhoneNumberVerified => isPhoneNumberVerified != null && isPhoneNumberVerified.Value, 
                value => new IsPhoneNumberVerified(value))
            .HasDefaultValue(new IsPhoneNumberVerified(false));
        
        builder.HasOne<File>().WithMany().HasForeignKey(city => city.ProfilePhotoId).OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.Gender).HasConversion<int>();
        
        builder.HasIndex(x => x.PhoneNumber).IsUnique();
        
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}