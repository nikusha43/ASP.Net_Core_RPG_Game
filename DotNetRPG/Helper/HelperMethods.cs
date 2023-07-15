using System.Security.Claims;

namespace DotNetRPG.Helper
{
	public class HelperMethods
	{
		private IHttpContextAccessor _contextAccessor { get; set; }

		public HelperMethods(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public int GetUserId()
		{
			return int.Parse(_contextAccessor.HttpContext.User
			.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}
