using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using test2.Context;
using test2.Entities;
using test2.Models;

namespace test2.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private CharacterContext _characterContext;

    public CharacterRepository(CharacterContext characterContext)
    {
        _characterContext = characterContext;
    }

    public async Task<bool> CharacterExistsAsync(int idCharacter)
    {
        var characterExists = await _characterContext.Characters.FindAsync(idCharacter);

        return characterExists != null;
    }

    public async Task<GetCharacterDataDto> GetCharacterDataAsync(int idCharacter)
    {
        var character = await _characterContext.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .Where(c => c.Id == idCharacter)
            .Select(c => new GetCharacterDataDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b => new BackpackDto
                {
                    ItemName = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount = b.Amount
                }).ToList(),
                Titles = c.CharacterTitles.Select(ct => new TitleDto
                {
                    Title = ct.Title.Name,
                    AcquiredAt = ct.AcquiredAt
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return character;
    }

    public async Task<bool> ItemsExistsAsync(AddItemsDto addItemsDto)
    {
        foreach (var item in addItemsDto.Items)
        {
            var check = await _characterContext.Items.FindAsync(item.IdItem);

            if (check == null)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<bool> HasEnoughWeightAsync(int idCharacter, AddItemsDto addItemsDto)
    {
        var weight = await _characterContext.Characters.FindAsync(idCharacter);
        int totalItemsWeight = 0;
        
        foreach (var item in addItemsDto.Items)
        {
            var getTtem = await _characterContext.Items.FindAsync(item.IdItem);
            totalItemsWeight += getTtem.Weight;
        }

        int remainCapacity = weight.MaxWeight - weight.CurrentWeight;

        if (totalItemsWeight > remainCapacity)
        {
            return false;
        }

        return true;
    }

   private async Task<int> GetTotalItemsWeightAsync(AddItemsDto addItemsDto)
    {
        int totalItemsWeight = 0;
        
        foreach (var item in addItemsDto.Items)
        {
            var getTtem = await _characterContext.Items.FindAsync(item.IdItem);
            totalItemsWeight += getTtem.Weight;
        }

        return totalItemsWeight;
    }

    public async Task AddNewItemsAsync(int idCharacter, AddItemsDto addItemsDto)
    {
        var character = await _characterContext.Characters.FindAsync(idCharacter);
        
        foreach (var item in addItemsDto.Items)
        {
            var hasItem = hasItemAsync(item.IdItem);
            if (hasItem != null)
            {
                var getFromBackpack = await _characterContext.Backpacks.FindAsync(idCharacter, item.IdItem);

                getFromBackpack.Amount = item.Amount;
            }
            else
            {
                var newBackpackItem = new Backpack()
                {
                    CharacterId = idCharacter,
                    ItemId = item.IdItem,
                    Amount = item.Amount
                };

                _characterContext.Backpacks.Add(newBackpackItem);
            }
        }

        int currentWeight = character.CurrentWeight;
        
        character.CurrentWeight = currentWeight + GetTotalItemsWeightAsync(addItemsDto).Result;
        
        await _characterContext.SaveChangesAsync();
    }

    private async Task<Item?> hasItemAsync(int id)
    {
        return await _characterContext.Items.FindAsync(id);
    }
}