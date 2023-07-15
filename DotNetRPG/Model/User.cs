namespace DotNetRPG.Model
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public List<Character>? characters { get; set; }
	}
}
