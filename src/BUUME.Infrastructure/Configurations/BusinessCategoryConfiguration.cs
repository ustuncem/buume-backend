using BUUME.Domain.BusinessCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUUME.Infrastructure.Configurations;

internal sealed class BusinessCategoryConfiguration : IEntityTypeConfiguration<BusinessCategory>
{
    public void Configure(EntityTypeBuilder<BusinessCategory> builder)
    {
        builder.ToTable("business_categories");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}