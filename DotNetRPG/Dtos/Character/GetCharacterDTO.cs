using DotNetRPG.Dtos.Skill;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model.Enums;

namespace DotNetRPG.Dtos
{
	public class GetCharacterDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = "GucciMane";
		public int Hitpoint { get; set; } = 100;
		public int Strength { get; set; } = 10;
		public int Defence { get; set; } = 10;
		public int Intelligence { get; set; } = 10;
		public RpgClass Class { get; set; } = RpgClass.Knight;
		public GetWeaponDto? Weapon { get; set; }
		public List<GetSkillDTO> Skills { get;set; }
        public int Fights { get; set; }
        public int Victorys { get; set; }
        public int Defeats { get; set; }

    }
}
