using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Helper;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.Services.Implementations
{
	public class WeaponService : HelperMethods, IWeaponService
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;

		public WeaponService(IHttpContextAccessor contextAccessor, ApplicationDbContext db, IMapper mapper) : base(contextAccessor)
		{
			_db = db;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDTO newWeapon)
		{
			var response = new ServiceResponse<GetCharacterDto>();
			try
			{
				var character = await _db.characters
					.Include(c => c.weapon)
					.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId
					&& c.user.Id == GetUserId());
				if (character == null)
				{
					response.Success = false;
					response.Message = "Character not found";
					return response;
				}
				var weapon = new Weapon()
				{
					Name = newWeapon.Name,
					Damage = newWeapon.Damage,
					Character = character
				};
				await _db.weapons.AddAsync(weapon);
				await _db.SaveChangesAsync();
				response.Data = _mapper.Map<GetCharacterDto>(character);
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}
			return response;
		}
	}
}
