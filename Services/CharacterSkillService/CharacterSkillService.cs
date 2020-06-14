using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Dtos.CharacterSkill;
using DOTNET_RPG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterSkillService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters 
                        .Include(c => c.Weapon)
                        .Include(c =>c.CharacterSkills).ThenInclude(cs => cs.Skill)
                        .FirstOrDefaultAsync(c => c.ID == newCharacterSkill.CharacterId && 
                        c.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

                if(character == null){
                    response.Success = false; 
                    response.Message = "Character not found.";
                    return response;
                }

                Skill skill = await _context.Skills
                      .FirstOrDefaultAsync(s => s.Id ==newCharacterSkill.SkillId);
                if(skill == null){
                    response.Success = false; 
                    response.Message = "Skill not found";
                    return response;
                }
                CharacterSkill characterSkill = new CharacterSkill
                {
                    Character = character, 
                    Skill = skill
                };

                await _context.CharacterSkills.AddAsync(characterSkill); 
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex)
            {
                response.Success = false; 
                response.Message = ex.Message; 
            }
            return response;
        }
    }
}