using System.Collections.Generic;
using System.Threading.Tasks;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterSevices
{
    public interface ICharacterService
    {
         Task <List <Character>> GetAllCharacters(); 
         Task <Character> GetCharacterById(int id); 
         Task <List<Character>> AddCharacter(Character newCharacter); 
    }
}