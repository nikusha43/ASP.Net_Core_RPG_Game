using System.Security.Claims;
using System.Security.Cryptography;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Character;
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
	public class CharacterController : ControllerBase
	{
		private readonly ICharacterService _characterService;
		public CharacterController(ICharacterService characterService)
		{
			_characterService = characterService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
		{
			return await _characterService.GetCharacters();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
		{
			return await _characterService.GetCharacterById(id);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> CreateCharacter(AddCharacterDto newCharacter)
		{
			return await _characterService.AddCharacter(newCharacter);
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
		{
			var response = await _characterService.UpdateCharacter(updatedCharacter);
			if(response.Data == null)
			{
				return NotFound(response);
			}
			return response;
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
		{
			var response = await _characterService.DeleteCharacter(id);
			if (response.Data == null)
			{
				return NotFound(response);
			}
			return response;
		}
		[HttpPost]
		public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDTO newharacterSkill)
		{
			return await _characterService.AddCharacterSkill(newharacterSkill);
		}
	}
}
