using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test2.Entities;

namespace test2.Configs;

public class BackpackConfiguration : IEntityTypeConfiguration<Backpack>
{
    public void Configure(EntityTypeBuilder<Backpack> builder)
    {
        builder.HasKey(b => new { b.CharacterId, b.ItemId });
        builder.Property(b => b.CharacterId).IsRequired();
        builder.Property(b => b.ItemId).IsRequired();
        builder.Property(b => b.Amount).IsRequired();
        builder.ToTable("Backpack");

        builder.HasOne(b => b.Character)
            .WithMany(c => c.Backpacks)
            .HasForeignKey(b => b.CharacterId);
        
        builder.HasOne(b => b.Item)
            .WithMany(i => i.Backpacks)
            .HasForeignKey(b => b.ItemId);
    }
}