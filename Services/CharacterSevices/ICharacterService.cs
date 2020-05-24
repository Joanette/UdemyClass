using System.Collections.Generic;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterSevices
{
    public interface ICharacterService
    {
         List <Character> GetAllCharacters(); 
         Character GetCharacterById(int id); 
         List<Character> AddCharacter(Character newCharacter); 
    }
}