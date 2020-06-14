using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.CharacterSevices
{
    public class CharacterService : ICharacterService
    {
      
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync(); 

            serviceResponse.Data = (_context.Characters.Where(c => c.user.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                Character character = await _context.Characters
                 .FirstOrDefaultAsync(c => c.ID == id && c.user.Id == GetUserId());
                 if(character != null){
                      _context.Characters.Remove(character);
                      await _context.SaveChangesAsync();
                      serviceResponse.Data = (_context.Characters.Where(c => c.user.Id == GetUserId())
                       .Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                 }    
                 else{
                     serviceResponse.Success = false; 
                     serviceResponse.Message = "Character not found";
                 } 
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacter = await _context.Characters.Where( c=> c.user.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbCharacter.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            
            Character selected = await _context.Characters
            .Include(c=>c.Weapon)
            .Include(c=>c.CharacterSkills).ThenInclude(cs=> cs.Skill)
            .FirstOrDefaultAsync(c => c.ID == id && c.user.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(selected);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters.Include(c=> c.user).FirstOrDefaultAsync(c => c.ID == updateCharacter.ID);
                if(character.user.Id == GetUserId())
                {
                    character.Name = updateCharacter.Name;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Defense = updateCharacter.Defense;
                    character.Class = updateCharacter.Class;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;

                    _context.Characters.Update(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else{
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }
    }
}