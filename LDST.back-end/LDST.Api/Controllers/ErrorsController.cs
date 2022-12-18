using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
