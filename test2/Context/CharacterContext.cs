using Microsoft.EntityFrameworkCore;
using test2.Configs;
using test2.Entities;

namespace test2.Context;

public class CharacterContext : DbContext
{
    public CharacterContext(DbContextOptions opts) : base(opts)
    {
        
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Title> Titles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new BackpackConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterTitleConfiguration());
        modelBuilder.ApplyConfiguration(new TitleConfiguration());
    }
}