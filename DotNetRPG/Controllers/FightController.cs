using DotNetRPG.Dtos.Fight;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DotNetRPG.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FightController : ControllerBase
    {

        private readonly IFightService _fs;

        public FightController(IFightService fs)
        {
            _fs = fs;
        }
        [HttpPost]
        public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            return await _fs.WeaponAttack(request);
        }
        [HttpPost]
        public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
        {
            return await _fs.SkillAttack(request);
        }

        [HttpPost]
        public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request)
        {
            return await _fs.Fight(request);
        }

        [HttpGet]
        //[SwaggerResponse(200, type:typeof(ICollection<HighScoreDTO>))]
        public async Task<ServiceResponse <List<HighScoreDTO>>> GetHighScore()
        {
            return await _fs.GetHighScore();
        }
    }
}
