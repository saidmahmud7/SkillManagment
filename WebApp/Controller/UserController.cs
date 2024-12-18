using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService service) : ControllerBase
{
    [HttpGet]
    public ApiResponse<List<User>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("GetAccount/{id}")]
    public ApiResponse<User> GetUserById(int id)
    {
        return service.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> AddUser(User user)
    {
        return service.Add(user);
    }

    [HttpPut]
    public ApiResponse<bool> UpdateUser(User user)
    {
        return service.Update(user);
    }

    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return service.Delete(id);
    }
}