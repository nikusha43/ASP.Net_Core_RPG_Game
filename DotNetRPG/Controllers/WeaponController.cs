using System.Runtime.CompilerServices;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WeaponController : ControllerBase
	{
		private readonly IWeaponService _weaponService;

		public WeaponController(IWeaponService weaponService)
		{
			_weaponService = weaponService;
		}

		[HttpPost]
		public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDTO newWeapon)
		{
			return await _weaponService.AddWeapon(newWeapon);
		}
	}
}
