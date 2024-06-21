using test2.Entities;
using test2.Models;

namespace test2.Services;

public interface ICharacterService
{
    public Task<GetCharacterDataDto> GetCharacterDataAsync(int idCharacter);

    public Task AddItemsAsync(int idCharacter, AddItemsDto addItemsDto);
}