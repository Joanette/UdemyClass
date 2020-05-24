using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DOTNET_RPG.Models;
using System.Linq;
using DOTNET_RPG.Services.CharacterSevices;
using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Character;

namespace DOTNET_RPG.Controllers
{
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

    }
}