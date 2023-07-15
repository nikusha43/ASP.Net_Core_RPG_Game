using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Character;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Model;
using DotNetRPG.Services.Implementations;
using DotNetRPG.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IskillService _iskill;

        public SkillController(IskillService iskill)
        {
            _iskill = iskill;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDTO>>>> GetAll()
        {
            return await _iskill.GetSkill();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSkillDTO>>> GetSingle(int id)
        {
            return await _iskill.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDTO>>>> CreateSkill(AddSkillDTO NewSkill)
        {
            return await _iskill.AddSkill(NewSkill);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetSkillDTO>>> UpdateSkill(UpdateSkillDTO Upskill)
        {
            var response = await _iskill.UpdateSkill(Upskill);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDTO>>>> DeleteSkill(int id)
        {
            var response = await _iskill.DeleteSkill(id);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return response;
        }



    }
}
