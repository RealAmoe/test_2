using System.ComponentModel.DataAnnotations;

namespace test2.Models;

public class AddItemsDto
{
    [Required]
    public ICollection<ItemDto> Items { get; set; }
}

public class ItemDto
{
    [Required]
    [Range(1, Int32.MaxValue)]
    public int IdItem { get; set; }
    [Required]
    [Range(1, Int32.MaxValue)]
    public int Amount { get; set; }
}