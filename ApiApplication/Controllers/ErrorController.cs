using System;
using System.Net;
using ApiApplication.Abstractions.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/error")]
public class ErrorController : ControllerBase
{
    [HttpGet(Name = "Error")]
    public IActionResult Error()
    {
        Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IApiException serviceException => (serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };

        return Problem(
            detail: exception?.StackTrace,
            title: exception?.Message
        );
    }
}
