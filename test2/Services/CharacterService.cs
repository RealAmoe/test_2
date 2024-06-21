using test2.Entities;
using test2.Exceptions;
using test2.Models;
using test2.Repositories;

namespace test2.Services;

public class CharacterService : ICharacterService
{
    private ICharacterRepository _characterRepository;
    private IUnitOfWork _unitOfWork;

    public CharacterService(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCharacterDataDto> GetCharacterDataAsync(int idCharacter)
    {
        var characterExists = await _characterRepository.CharacterExistsAsync(idCharacter);
        if (!characterExists)
        {
            throw new DoesntExistException("Character does not exits");
        }

        var data = await _characterRepository.GetCharacterDataAsync(idCharacter);

        return data;
    }

    public async Task AddItemsAsync(int idCharacter, AddItemsDto addItemsDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var characterExists = await _characterRepository.CharacterExistsAsync(idCharacter);

            if (!characterExists)
            {
                throw new BadRequestException("Character does not exist");
            }

            var checkItemsExists = await _characterRepository.ItemsExistsAsync(addItemsDto);

            if (!checkItemsExists)
            {
                throw new BadRequestException("One or many items do not exist");
            }

            var hasEnoughWeight = await _characterRepository.HasEnoughWeightAsync(idCharacter, addItemsDto);

            if (!hasEnoughWeight)
            {
                throw new BadRequestException("Character doesnt have enough capacity");
            }

            await _characterRepository.AddNewItemsAsync(idCharacter, addItemsDto);

            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();

            throw e;
        }
    }
}