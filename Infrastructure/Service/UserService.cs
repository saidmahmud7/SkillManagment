using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;
using Infrastructure.Service.Interface;

namespace Infrastructure.Service;

public class UserService(IContext context) : IGenericService<User>
{
    public ApiResponse<List<User>> GetAll()
    {
        using var connection = context.Connection();
        string sql = "select * from users";
        var res = connection.Query<User>(sql).AsQueryable().ToList();
        return new ApiResponse<List<User>>(res);
    }

    public ApiResponse<User> GetById(int id)
    {
        using var connection = context.Connection();
        string sql = "select * from users where userId = @Id";
        var res = connection.QuerySingleOrDefault<User>(sql, new { Id = id });
        if (res == null) return new ApiResponse<User>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<User>(res);
    }

    public ApiResponse<bool> Add(User data)
    {
        using var connection = context.Connection();
        string sql = "insert into users (FullName,Email,Phone,City) values (@FullName,@Email,@Phone,@City)";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(User data)
    {
        using var connection = context.Connection();
        string sql = "update users set FullName=@FullName,Email=@Email,Phone=@Phone,City=@City where userid = @userid";
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection();
        string sql = "delete from users where userid = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<bool>(res != 0);
    }
    
}