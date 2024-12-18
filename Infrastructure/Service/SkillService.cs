using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;
using Infrastructure.Service.Interface;

namespace Infrastructure.Service;

public class SkillService(IContext context) : IGenericService<Skill>
{
    public ApiResponse<List<Skill>> GetAll()
    {
        using var connection = context.Connection();
        string sql = "select * from skills";
        var res = connection.Query<Skill>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Skill>>(res);
    }

    public ApiResponse<Skill> GetById(int id)
    {
        using var connection = context.Connection();
        string sql = "select * from skills where SkillId = @Id";
        var res = connection.QuerySingleOrDefault<Skill>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Skill>(HttpStatusCode.NotFound, "Skills not found");
        return new ApiResponse<Skill>(res);
    }

    public ApiResponse<bool> Add(Skill data)
    {
        using var connection = context.Connection();
        string sql = "insert into skills (UserId,Title,Description) values (@UserId,@Title,@Description)";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Skill data)
    {
        using var connection = context.Connection();
        string sql = "update skills set UserId=@UserId,Title=@Title,Description=@Description where SkillId = @SkillId";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection();
        string sql = "delete from skills where SkillId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Skill not found");
        return new ApiResponse<bool>(res != 0);
    }
}