using DotNetRPG.Dtos.Skill;
using DotNetRPG.Model;

namespace DotNetRPG.Services.Interfaces
{
    public interface IskillService
    {                                 
        Task<ServiceResponse<List<GetSkillDTO>>> GetSkill();
        Task<ServiceResponse<GetSkillDTO>> GetById(int skillId);
        Task<ServiceResponse<List<GetSkillDTO>>> AddSkill(AddSkillDTO newSkill);
        Task<ServiceResponse<GetSkillDTO>> UpdateSkill(UpdateSkillDTO Updateskill);
        Task<ServiceResponse<List<GetSkillDTO>>>DeleteSkill(int skillId);
    }
}
