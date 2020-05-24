using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DOTNET_RPG.Models;
using System.Linq;

namespace DOTNET_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        private static List <Character> characters  = new List<Character> {
            new Character(), 
            new Character{ ID = 1, Name = "Sam"}, 
        };
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(characters);
        }

        public IActionResult GetSingle()
        {
            return Ok(characters[0]);
        }
        [HttpGet("{id}")]
        public IActionResult GetSingle(int ID){ 
            return Ok(characters.FirstOrDefault(c=>c.ID == ID));
        }
    }
}