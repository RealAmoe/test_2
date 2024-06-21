using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test2.Entities;

namespace test2.Configs;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(c => c.LastName).HasMaxLength(120).IsRequired();
        builder.Property(c => c.CurrentWeight).IsRequired();
        builder.Property(c => c.MaxWeight).IsRequired();
        builder.ToTable("Character");
    }
}