namespace test2.Entities;

public class Character
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public virtual ICollection<Backpack> Backpacks { get; set; }
    public virtual ICollection<CharacterTitle> CharacterTitles { get; set; }
}