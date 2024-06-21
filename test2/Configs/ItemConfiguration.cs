using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test2.Entities;

namespace test2.Configs;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
        builder.Property(i => i.Weight).IsRequired();
        builder.ToTable("Item");
        
    }
}