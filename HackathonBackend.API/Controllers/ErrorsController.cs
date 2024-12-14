using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HackathonBackend.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        return Problem(title: exception?.Message, statusCode: 400);
    }
}