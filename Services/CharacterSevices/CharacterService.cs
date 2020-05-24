using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterSevices
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character{ ID = 1, Name = "Sam"},
        };      
        private readonly IMapper _mapper;  
        public CharacterService(IMapper mapper)
        {
          _mapper = mapper;  
        }
        
        public async Task <ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse <List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.ID = characters.Max(c => c.ID)+1;
            characters.Add(character); 
            serviceResponse.Data = (characters.Select(c=> _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task <ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse <List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = (characters.Select(c=> _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task <ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse <GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character selected = characters.FirstOrDefault(c => c.ID == id); 
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(selected);
            return serviceResponse;
        }
    }
}