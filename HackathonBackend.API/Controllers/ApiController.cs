namespace HackathonBackend.API.Controllers;

using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected Guid? GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Guid.TryParse(userId, out var validUserId))
        {
            return validUserId;
        }

        return null;
    }

    protected IActionResult UnauthorizedUserIdProblem()
    {
        return Problem([Error.Unauthorized(description: "Invalid or missing user ID in token")]);
    }
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
            return Problem();
        
        if (errors.All(error => error.Type == ErrorType.Validation))
            return ValidationProblem(errors);
        
        return Problem(errors.First());
    }

    private IActionResult Problem(Error firstError)
    {
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelStateDictionary);
    }
}