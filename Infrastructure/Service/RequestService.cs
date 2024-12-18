using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;
using Infrastructure.Service.Interface;

namespace Infrastructure.Service;

public class RequestService(IContext context) : IGenericService<Request>
{
    public ApiResponse<List<Request>> GetAll()
    {
        using var connection = context.Connection();
        string sql = "select * from requests";
        var res = connection.Query<Request>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Request>>(res);
    }

    public ApiResponse<Request> GetById(int id)
    {
        using var connection = context.Connection();
        string sql = "select * from requests where RequestId = @Id";
        var res = connection.QuerySingleOrDefault<Request>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Request>(HttpStatusCode.NotFound, "Request not found");
        return new ApiResponse<Request>(res);
    }

    public ApiResponse<bool> Add(Request data)
    {
        using var connection = context.Connection();
        string sql =
            "insert into  requests(FromUserId,ToUserId,RequestedSkillId,OfferedSkillId,Status) values (@FromUserId,@ToUserId,@RequestedSkillId,@OfferedSkillId,@Status)";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Request data)
    {
        using var connection = context.Connection();
        string sql = @"update requests set FromUserId=@FromUserId,ToUserId=@ToUserId,RequestedSkillId=@RequestedSkillId,OfferedSkillId=@OfferedSkillId,Status=@Status 
          where RequestId = @RequestId";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection();
        string sql = "delete from requests where RequestId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Request not found");
        return new ApiResponse<bool>(res != 0);    }
}