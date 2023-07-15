using DotNetRPG.Data;
using DotNetRPG.Dtos.Fight;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;

namespace DotNetRPG.Services.Interfaces
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
        Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
        Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request);
        Task<ServiceResponse<List<HighScoreDTO>>> GetHighScore();
    }
}
