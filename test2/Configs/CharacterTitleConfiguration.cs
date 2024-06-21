using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test2.Entities;

namespace test2.Configs;

public class CharacterTitleConfiguration : IEntityTypeConfiguration<CharacterTitle>
{
    public void Configure(EntityTypeBuilder<CharacterTitle> builder)
    {
        builder.HasKey(ct => new { ct.CharacterId, ct.TitleId });
        builder.Property(ct => ct.CharacterId).IsRequired();
        builder.Property(ct => ct.TitleId).IsRequired();
        builder.Property(ct => ct.AcquiredAt).IsRequired();
        builder.ToTable("CharacterTitle");

        builder.HasOne(ct => ct.Character)
            .WithMany(c => c.CharacterTitles)
            .HasForeignKey(ct => ct.CharacterId);
        
        builder.HasOne(ct => ct.Title)
            .WithMany(t => t.CharacterTitles)
            .HasForeignKey(ct => ct.TitleId);
    }
}