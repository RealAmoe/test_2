using test2.Context;
using test2.Entities;
using test2.Models;

namespace test2.Repositories;

public interface ICharacterRepository
{
    public Task<bool> CharacterExistsAsync(int idCharacter);
    public Task<GetCharacterDataDto> GetCharacterDataAsync(int idCharacter);

    public Task<bool> ItemsExistsAsync(AddItemsDto addItemsDto);
    public Task<bool> HasEnoughWeightAsync(int idCharacter, AddItemsDto addItemsDto);
    public Task AddNewItemsAsync(int idCharacter, AddItemsDto addItemsDto);
}