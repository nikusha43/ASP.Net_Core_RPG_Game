using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;

namespace DotNetRPG.Services.Interfaces
{
	public interface IWeaponService
	{
	 Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDTO newWeapon);
		
	}
}
