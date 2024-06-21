using Microsoft.AspNetCore.Mvc;
using test2.Exceptions;
using test2.Models;
using test2.Services;

namespace test2.Controllers;
[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("{idCharacter}")]
    public async Task<IActionResult> GetCharacterData(int idCharacter)
    {
        GetCharacterDataDto getCharacterDataDto;
        try
        {
            getCharacterDataDto = await _characterService.GetCharacterDataAsync(idCharacter);
        }
        catch (DoesntExistException doesntExistException)
        {
            return NotFound(doesntExistException.Message);
        }

        return Ok(getCharacterDataDto);
    }

    [HttpPost("{idCharacter}/backpacks")]
    public async Task<IActionResult> AddNewItem(int idCharacter, AddItemsDto addItemsDto)
    {
        try
        {
            await _characterService.AddItemsAsync(idCharacter, addItemsDto);
        }
        catch (BadRequestException badRequestException)
        {
            return BadRequest(badRequestException.Message);
        }

        return NoContent();
    }
    
}