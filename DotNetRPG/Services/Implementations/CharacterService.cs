using System.Runtime.CompilerServices;
using System.Security.Claims;
using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Character;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Helper;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.Services.Implementations
{
	public class CharacterService : HelperMethods, ICharacterService
	{
        #region Listcoment
        //public static List<Character> characters = new List<Character>()
        //{
        //	new Character(),
        //	new Character()
        //	{
        //		Id = 1,
        //		Name = "Frodo",
        //		Class = Model.Enums.RpgClass.Marksman
        //	}
        //};
        #endregion

        private readonly HelperMethods _Hm;
        private readonly IMapper _mapper;
		private readonly ApplicationDbContext _db;

        public CharacterService(IHttpContextAccessor contextAccessor, IMapper mapper, ApplicationDbContext db, HelperMethods hm) : base(contextAccessor)
        {
            _mapper = mapper;
            _db = db;
            _Hm = hm;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
		{
			var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
			var character = _mapper.Map<Character>(newCharacter);
			//character.Id = _db.characters.Max(x => x.Id) + 1;

			character.user = await _db.users.FirstOrDefaultAsync(u => u.Id == GetUserId());

			await _db.characters.AddAsync(character);
			await _db.SaveChangesAsync();
			serviceResponse.Data = _db.characters
				.Where(c => c.user.Id == GetUserId())
				.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
		{
			var serviceResponse = new ServiceResponse<GetCharacterDto>();
			try
			{
				var character = await _db.characters
					.Include(c => c.weapon)
					.FirstOrDefaultAsync(x => x.Id == id && x.user.Id == GetUserId());
				if (character != null)
				{
					serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
				}
				else
				{
					serviceResponse.Success = false;
					serviceResponse.Message = "Character not found.";
				}
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = "Character not found " + ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
		{
			return new ServiceResponse<List<GetCharacterDto>>()
			{
				Data = _db.characters
				.Include(c => c.weapon)
				.Where(x => x.user.Id == GetUserId())
				.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList()
			};
		}

		public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
		{
			var response = new ServiceResponse<GetCharacterDto>();

			try
			{
				var character = await _db.characters
					.Include(c => c.user)
					.FirstOrDefaultAsync(x => x.Id == updatedCharacter.Id);

				#region MapperComment
				//character.Name = updatedCharacter.Name;
				//character.Hitpoint = updatedCharacter.Hitpoint;
				//character.Strength = updatedCharacter.Strength;
				//character.Defence = updatedCharacter.Defence;
				//character.Intelligence = updatedCharacter.Intelligence;
				//character.Class = updatedCharacter.Class;
				#endregion

				if (character.user.Id == GetUserId())
				{
					_mapper.Map(updatedCharacter, character);
					await _db.SaveChangesAsync();
					response.Data = _mapper.Map<GetCharacterDto>(character);
				}
				else
				{
					response.Success = false;
					response.Message = "Character not found,";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}
			
			return response;
		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
		{
			var response = new ServiceResponse<List<GetCharacterDto>>();
			try
			{
				var character = await _db.characters.FirstOrDefaultAsync(c => c.Id == id && c.user.Id == GetUserId());
				if (character != null)
				{
					_db.characters.Remove(_db.characters.First(x => x.Id == id));
					await _db.SaveChangesAsync();
					response.Data = await _db.characters
						.Where(c => c.user.Id == GetUserId())
						.Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
				}
				else
				{
					response.Success = false;
					response.Message = "Character not found.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}
			return response;
		}

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
			var response = new ServiceResponse<GetCharacterDto>()
			{
				Data = new GetCharacterDto()
			};

			try
			{
				int id = _Hm.GetUserId();
				var character = await _db.characters 
				   .Include(c => c.Skills)
				   .Include(c => c.weapon)
				   .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.user.Id == _Hm.GetUserId());
				if(character == null)
				{
                    response.Success = false;
                    response.Message = "Character Notfound";
					return response;
                }
				var skill = await _db.Skills.FirstOrDefaultAsync(s =>s.Id == newCharacterSkill.SkillId);
				if(skill == null)
				{

					response.Success = false;
					response.Message = "SkillNotfound";
                    return response;
                }
				character.Skills.Add(skill);
				response.Data=_mapper.Map<GetCharacterDto>(character);
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
