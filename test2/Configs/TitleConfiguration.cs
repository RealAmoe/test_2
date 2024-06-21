using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test2.Entities;

namespace test2.Configs;

public class TitleConfiguration : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.ToTable("Title");
    }
}