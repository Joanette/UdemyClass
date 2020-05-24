using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DOTNET_RPG.Models;
using System.Linq;
using DOTNET_RPG.Services.CharacterSevices;

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
        public IActionResult Get()
        {
            return Ok(_characterService.GetAllCharacters());
        }

        public IActionResult GetSingle()
        {
            return Ok(_characterService.GetAllCharacters());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetSingle(int ID){ 
            return Ok(_characterService.GetCharacterById(ID));
        }
        
        [HttpPost]
        public IActionResult AddCharacter(Character newCharacter){
            return Ok(_characterService.AddCharacter(newCharacter));
        }

    }
}