using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.Dtos;
using DotNetRPG.Dtos.Skill;
using DotNetRPG.Helper;
using DotNetRPG.Model;
using DotNetRPG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DotNetRPG.Services.Implementations
{
    public class SkillService : IskillService
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public SkillService(IMapper mapper, ApplicationDbContext applicationDb)
        {
                _mapper = mapper;
                _db = applicationDb;
        }
        public async Task<ServiceResponse<List<GetSkillDTO>>> AddSkill(AddSkillDTO newSkill)
        {
            var response = new ServiceResponse<List<GetSkillDTO>>();

            try
            {
                var skill = _mapper.Map<Skill>(newSkill);

                await _db.Skills.AddAsync(skill);
                await _db.SaveChangesAsync();
                response.Data = await _db.Skills
                           .Select(x => _mapper.Map<GetSkillDTO>(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }

            return response;

        }

        public async Task<ServiceResponse<List<GetSkillDTO>>> DeleteSkill(int skillId)
        {
            var response = new ServiceResponse<List<GetSkillDTO>>();
            try
            {
                var skill = await _db.Skills.FirstOrDefaultAsync(s=>s.Id == skillId);
                if(skillId != null)
                {
                    _db.Skills.Remove(_db.Skills.First(k => k.Id == skillId));
                    await _db.SaveChangesAsync();
                    response.Data = await _db.Skills
                        .Select(x => _mapper.Map<GetSkillDTO>(x)).ToListAsync();
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

        public async Task<ServiceResponse<GetSkillDTO>> GetById(int skillId)
        {
            var response = new ServiceResponse<GetSkillDTO>();
            try
            {
                var skill = await _db.Skills
                    .Include(s=>s.Characters)
                    .FirstOrDefaultAsync(x=>x.Id == skillId);

                if(skill != null)
                {
                    response.Data = _mapper.Map<GetSkillDTO>(skill);
                }
                else
                {
                    response.Success = false;
                    response.Message = "akill not found.";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
              
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetSkillDTO>>> GetSkill()
        {
            var response = new ServiceResponse<List<GetSkillDTO>>();

            return new ServiceResponse<List<GetSkillDTO>>()
            {
                Data = _db.Skills
                 .Include(c => c.Characters) 
                 .Select(x => _mapper.Map<GetSkillDTO>(x)).ToList()
            };
        }

        public async Task<ServiceResponse<GetSkillDTO>> UpdateSkill(UpdateSkillDTO Updateskill)
        {
            var response = new ServiceResponse<GetSkillDTO>();

            try
            {
                var skill = await _db.Skills
                    .FirstOrDefaultAsync(x => x.Id == Updateskill.Id);
                if(skill != null)
                {
                    _mapper.Map(Updateskill,skill);
                    await _db.SaveChangesAsync();
                    response.Data = _mapper.Map<GetSkillDTO>(skill);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Skill not found,";
                }
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
