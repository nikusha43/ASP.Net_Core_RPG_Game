using DotNetRPG.Model.Enums;

namespace DotNetRPG.Model
{
	public class Character
	{
		public int Id { get; set; }
		public string Name { get; set; } = "Geralt";
		public int Hitpoint { get; set; } = 100;
		public int Strength { get; set; } = 10;
		public int Defence { get; set; } = 10;
		public int Intelligence { get; set; } = 10;
		public RpgClass Class { get; set; } = RpgClass.Knight;
		public User? user { get; set; }
		public Weapon weapon { get; set; }
        public List<Skill> Skills { get; set; }
        public int Fights { get; set; }
        public int Victorys { get; set; }
        public int Defeats { get; set; }


    }
}
