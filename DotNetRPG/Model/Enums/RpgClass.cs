using System.Text.Json.Serialization;

namespace DotNetRPG.Model.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RpgClass
	{
		Knight = 1,
		Mage,
		Marksman
	}
}
