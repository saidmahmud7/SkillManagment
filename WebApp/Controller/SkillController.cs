using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class SkillController(SkillService service) : ControllerBase
{
    [HttpGet]
    public ApiResponse<List<Skill>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("GetAccount/{id}")]
    public ApiResponse<Skill> GetSkillById(int id)
    {
        return service.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> AddSkill(Skill skill)
    {
        return service.Add(skill);
    }

    [HttpPut]
    public ApiResponse<bool> UpdateSkill(Skill skill)
    {
        return service.Update(skill);
    }

    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return service.Delete(id);
    }
}