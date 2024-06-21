namespace test2.Entities;

public class Title
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<CharacterTitle> CharacterTitles { get; set; }
}