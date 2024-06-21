namespace test2.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }

    public virtual ICollection<Backpack> Backpacks { get; set; }
}