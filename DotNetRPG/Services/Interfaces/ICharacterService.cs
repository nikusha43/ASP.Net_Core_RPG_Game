using System.Data;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Character;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Model;

namespace DotNetRPG.Services.Interfaces
{
	public interface ICharacterService
	{
		Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
		Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
		Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
		Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
		Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
		Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill);
    }
}
