using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TaskBoard.Authorization.API.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[EnableCors("ReactPolicy")]
public abstract class ApiBaseController : ControllerBase
{
    
}