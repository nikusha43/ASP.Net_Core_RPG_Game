using AutoMapper;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Fight;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;

namespace DotNetRPG
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
            CreateMap<Character, UpdateCharacterDto>().ReverseMap();

            CreateMap<Weapon, GetWeaponDto>().ReverseMap();

            CreateMap<Skill,GetSkillDTO>().ReverseMap();
            CreateMap<Skill,AddSkillDTO>().ReverseMap();    
            CreateMap<Skill,UpdateSkillDTO>().ReverseMap();
            CreateMap<Character,HighScoreDTO>().ReverseMap();
        }
    }
}
