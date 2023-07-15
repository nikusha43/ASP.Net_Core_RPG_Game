using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.Dtos.Fight;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Dtos.Weapon;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNetRPG.Services.Implementations
{
    public class FightService : IFightService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FightService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request)
        {
            var response = new ServiceResponse<FightResultDTO>()
            {
                Data= new FightResultDTO() 
            };
            try
            {
                var character = await _db.characters
                    .Include(c => c.weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.characterIds.Contains(c.Id) && (c.weapon!=null || c.Skills.Count !=0)).ToListAsync();
                bool Defeated = false;
                while (!Defeated)
                {
                    foreach(var attacker in character)
                    {
                        var opponents =character.Where(c =>c.Id != attacker.Id).ToList();
                        var oppnent = opponents[new Random().Next(opponents.Count)];
                        int damage = 0;
                        string AttackUsed = string.Empty;

                        bool HasWeapon = attacker.weapon  !=null;
                        bool HasSkill = attacker.Skills.Count >0;
                       
                        if(HasWeapon && HasSkill)
                        {
                            bool useWeapon = new Random().Next(2) == 0;
                            if(useWeapon)
                            {
                               
                            
                                AttackUsed = attacker.weapon.Name;
                                damage = DoWeapnAttack(attacker, oppnent);
                            }
                            else
                            {

                                var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                                AttackUsed = skill.Name;
                                damage = DoSkillAttack(attacker, oppnent, skill);
                            }
                        }
                        else if(HasWeapon)
                        {
                            
                            AttackUsed = attacker.weapon.Name;
                            damage = DoWeapnAttack(attacker, oppnent);
                        }
                        else
                        {

                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            AttackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, oppnent, skill);
                        }
                        response.Data.Log
                            .Add($"{attacker.Name}attacks {oppnent.Name} using {AttackUsed} with {(damage >= 0 ? damage :"no damage")}");
                        if(oppnent.Hitpoint <= 0)
                        {
                            Defeated = true;
                            attacker.Victorys++;
                            oppnent.Defeats++;
                            response.Data.Log.Add($"{oppnent.Name} has been defeated");
                            response.Data.Log.Add($"{attacker.Name} wins {attacker.Hitpoint} Hp left");

                        }
                    }
                }
                character.ForEach(c =>
                {
                    c.Fights++;
                    c.Hitpoint = 150;
                });
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
              
            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();

            try
            {
                var attacker = await _db.characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _db.characters
                   .Include(c => c.weapon)
                   .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't has dat skill";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);
                if (opponent.Hitpoint < 0) response.Message = $"{opponent.Name} has been defeated!";
                await _db.SaveChangesAsync();
                response.Data = new AttackResultDTO
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.Hitpoint,
                    OpponentHP = opponent.Hitpoint,
                    Damage = damage

                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        private static int DoSkillAttack(Character? attacker, Character? opponent, Skill? skill)
        {
            int damage = skill.Damage + new Random().Next(attacker.Intelligence);
            damage -= new Random().Next(opponent.Defence);

            if (damage > 0) opponent.Hitpoint -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
        {
            var response = new ServiceResponse<AttackResultDTO>();

            try
            {
                var attacker = await _db.characters
                    .Include(c => c.weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _db.characters
                   .Include(c => c.weapon)
                   .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = DoWeapnAttack(attacker, opponent);
                if (opponent.Hitpoint < 0) response.Message = $"{opponent.Name} has been defeated!";
                await _db.SaveChangesAsync();
                response.Data = new AttackResultDTO
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.Hitpoint,
                    OpponentHP = opponent.Hitpoint,
                    Damage = damage

                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        private static int DoWeapnAttack(Character? attacker, Character? opponent)
        {
            var damage = attacker.weapon.Damage + new Random().Next(attacker.Strength);
            damage -= new Random().Next(opponent.Defence);
            if (damage > 0) opponent.Hitpoint -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HighScoreDTO>>> GetHighScore()
        {
            var characters = await _db.characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victorys)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            return new (){ Data = characters.Select(c => _mapper.Map<HighScoreDTO>(c)).ToList()};
            

        }
    }
}
