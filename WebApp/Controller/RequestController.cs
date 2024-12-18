using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;




[ApiController]
[Route("api/[controller]")]
public class RequestController(RequestService service):ControllerBase
{
    [HttpGet]
    public ApiResponse<List<Request>> GetAll()
    {
        return service.GetAll();
    }

    [HttpPost]
    public ApiResponse<bool> Add(Request request)
    {
        return service.Add(request);
    }

    [HttpPut]
    public ApiResponse<bool> Update(Request request)
    {
        return service.Update(request);
    }

    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return service.Delete(id);
    }
}