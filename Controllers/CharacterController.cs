using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DOTNET_RPG.Models;
using System.Linq;
using DOTNET_RPG.Services.CharacterSevices;
using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;

        }
 
        private static List <Character> characters  = new List<Character> {
            new Character(), 
            new Character{ ID = 1, Name = "Sam"}, 
        };

        [HttpGet("GetAll")]
        public async Task <IActionResult> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        public async Task<IActionResult> GetSingle()
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters());
        }
        
        [HttpGet("{id}")]
        public async Task <IActionResult> GetSingle(int ID){ 
            return Ok(await _characterService.GetCharacterById(ID));
        }
        
        [HttpPost]
        public async Task <IActionResult> AddCharacter(AddCharacterDto newCharacter){
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        [HttpPut]
        public async Task <IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter){
            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response.Data==null){
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int ID){ 
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(ID);
            if (response.Data==null){
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}