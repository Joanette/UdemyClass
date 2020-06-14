using System.Threading.Tasks;
using DOTNET_RPG.Dtos.Fight;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FightServices
{
    public interface IFightService
    {
         Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
         Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
         Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
    }
}