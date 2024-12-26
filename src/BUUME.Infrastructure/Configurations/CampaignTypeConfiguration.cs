using BUUME.Domain.CampaignTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUUME.Infrastructure.Configurations;

internal sealed class CampaignTypeConfiguration : IEntityTypeConfiguration<CampaignType>
{
    public void Configure(EntityTypeBuilder<CampaignType> builder)
    {
        builder.ToTable("campaign_types");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500)
            .HasConversion(description => description.Value, value => new Description(value));
        builder.Property(x => x.Code).HasMaxLength(80)
            .HasConversion(code => code.Value, value => Code.GetFromCode(value));
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}