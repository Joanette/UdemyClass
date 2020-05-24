using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterSevices
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character{ ID = 1, Name = "Sam"},
        };        
        public async Task <List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public async Task <List<Character>> GetAllCharacters()
        {
            return characters;
        }

        public async Task <Character> GetCharacterById(int id)
        {
            return characters.FirstOrDefault(c => c.ID == id);
        }
    }
}