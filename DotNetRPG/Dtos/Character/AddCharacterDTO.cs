using DotNetRPG.Model.Enums;

namespace DotNetRPG.Dtos
{
	public class AddCharacterDto
	{
		public string Name { get; set; } = "Geralt";
		public int Hitpoint { get; set; } = 100;
		public int Strength { get; set; } = 10;
		public int Defence { get; set; } = 10;
		public int Intelligence { get; set; } = 10;
		public RpgClass Class { get; set; } = RpgClass.Knight;
	}
}
