using Microsoft.AspNetCore.Mvc;

namespace WebApiProjeto.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]

    public IActionResult Get()
    {
        return Ok("Ok");
    }

}
